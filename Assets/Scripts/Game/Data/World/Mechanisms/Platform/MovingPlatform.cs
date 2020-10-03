using System.Linq;
using UnityEngine;

namespace LD47 {
	[RequireComponent(typeof(RequirePowerMechanism))]
	public class MovingPlatform : MonoBehaviour {
		[SerializeField] protected Transform   _platform;
		[SerializeField] protected float       _speed = 5;
		[SerializeField] protected Transform[] _steps;
		[SerializeField] protected int         _initialStep;
		[SerializeField] protected float       _initialLerpToNextStep;
		[SerializeField] protected float       _waitAtSteps = .5f;

		private float[]               distancesBetweenSteps { get; set; }
		private int                   currentStep           { get; set; }
		private float                 currentLerp           { get; set; }
		private float                 waitAtCurrentStep     { get; set; }
		private RequirePowerMechanism requirePowerMechanism { get; set; }

		private void Awake() {
			requirePowerMechanism = GetComponent<RequirePowerMechanism>();
			distancesBetweenSteps = _steps.Select((t, i) => Vector3.Distance(_steps[i].position, _steps[(i + 1) % _steps.Length].position)).ToArray();
			ResetToInitialPosition();
			TimeManager.onLoopEnded.AddListenerOnce(ResetToInitialPosition);
		}

		private void Update() {
			if (!TimeManager.playing) return;
			if (!requirePowerMechanism.isPowerProvided) return;
			if (_steps.Length < 2) return;
			if (waitAtCurrentStep > 0) {
				waitAtCurrentStep -= Time.deltaTime;
				return;
			}
			currentLerp += _speed * Time.deltaTime / distancesBetweenSteps[currentStep];
			if (currentLerp >= 1) {
				currentLerp = 0;
				currentStep = (currentStep + 1) % _steps.Length;
				waitAtCurrentStep = _waitAtSteps;
			}
			UpdatePosition();
		}

		private void UpdatePosition() {
			_platform.position = Vector3.Lerp(_steps[currentStep].position, _steps[(currentStep + 1) % _steps.Length].position, currentLerp);
		}

		[ContextMenu("Set to initial position")]
		private void ResetToInitialPosition() {
			currentStep = _initialStep;
			currentLerp = _initialLerpToNextStep;
			UpdatePosition();
		}
	}
}