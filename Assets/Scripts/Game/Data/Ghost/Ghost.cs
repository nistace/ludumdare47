using UnityEngine;

namespace LD47 {
	public class Ghost : MonoBehaviour {
		[SerializeField] protected HumanoidAnimator _animator;
		[SerializeField] protected GhostRecord      _record;
		[SerializeField] protected int              _recordStep;

		private new Transform transform { get; set; }

		private void Awake() => transform = base.transform;

		private void FixedUpdate() {
			if (!TimeManager.playing) return;
			ApplyRecordData(TimeManager.currentLoopTime);
		}

		public void Initialise(GhostRecord record) {
			_record = record;
			ResetForNextLoop();
		}

		public void ResetForNextLoop() {
			_recordStep = 0;
			ApplyRecordData(0);
		}

		private void ApplyRecordData(float timeInStep) {
			transform.position = _record.GetStepPosition(timeInStep, ref _recordStep);
			transform.rotation = _record.GetStepRotation(timeInStep, ref _recordStep);
			_animator.SetRunning(_record.GetStepRunning(timeInStep, ref _recordStep));
			_animator.SetInTheAir(_record.GetStepInTheAir(timeInStep, ref _recordStep));
		}
	}
}