using System.Linq;
using UnityEngine;

namespace LD47 {
	[RequireComponent(typeof(RequirePowerMechanism))]
	public class MovingPlatform : MonoBehaviour {
		[SerializeField] protected Transform _platform;
		[SerializeField] protected float     _speed = 5;
		[SerializeField] protected Vector3[] _steps;
		[SerializeField] protected int       _initialStep;
		[SerializeField] protected float     _initialLerpToNextStep;
		[SerializeField] protected float     _waitAtSteps = .5f;

		private float[]               distancesBetweenSteps { get; set; }
		private int                   currentStep           { get; set; }
		private float                 currentLerp           { get; set; }
		private float                 waitAtCurrentStep     { get; set; }
		private RequirePowerMechanism requirePowerMechanism { get; set; }

		private void Awake() {
			requirePowerMechanism = GetComponent<RequirePowerMechanism>();
			distancesBetweenSteps = _steps.Select((t, i) => Vector3.Distance(_steps[i], _steps[(i + 1) % _steps.Length])).ToArray();
			ResetToInitialPosition();
			TimeManager.onLoopEnded.AddListenerOnce(ResetToInitialPosition);
		}

		private void FixedUpdate() {
			if (!TimeManager.playing) return;
			if (!requirePowerMechanism.isPowerProvided) return;
			if (_steps.Length < 2) return;
			if (waitAtCurrentStep > 0) {
				waitAtCurrentStep -= Time.deltaTime;
				return;
			}
			currentLerp += _speed * Time.fixedDeltaTime / distancesBetweenSteps[currentStep];
			if (currentLerp >= 1) {
				currentLerp = 0;
				currentStep = (currentStep + 1) % _steps.Length;
				waitAtCurrentStep = _waitAtSteps;
			}
			UpdatePosition();
		}

		private void UpdatePosition() {
			_platform.localPosition = Vector3.Lerp(_steps[currentStep], _steps[(currentStep + 1) % _steps.Length], currentLerp);
		}

		[ContextMenu("Set to initial position")]
		private void ResetToInitialPosition() {
			currentStep = _initialStep;
			currentLerp = _initialLerpToNextStep;
			UpdatePosition();
		}

		[ContextMenu("Add current platform position as a step")]
		private void AddCurrentPlatformPositionAsAStep() {
			_steps = _steps.WithAppended(_platform.localPosition);
		}
	}
}