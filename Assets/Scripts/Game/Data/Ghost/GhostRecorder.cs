using UnityEngine;

namespace LD47 {
	public class GhostRecorder : MonoBehaviour {
		[SerializeField] protected HumanoidAnimator _animator;
		[SerializeField] protected float            _recordFrequency = .05f;
		[SerializeField] protected GhostRecord      _record;

		private float delayBeforeNextRecordStep { get; set; }

		public new Transform transform { get; private set; }

		private void Awake() {
			transform = base.transform;
		}

		public GhostRecord Reinitialize() {
			_record = new GhostRecord();
			delayBeforeNextRecordStep = 0;
			return _record;
		}

		private void Update() {
			if (!TimeManager.playing) return;
			delayBeforeNextRecordStep -= Time.deltaTime;
			if (delayBeforeNextRecordStep > 0) return;
			_record.AddStep(new GhostRecordStep(TimeManager.currentLoopTime, transform.position, transform.rotation, _animator.running, _animator.inTheAir));
			delayBeforeNextRecordStep = _recordFrequency;
		}
	}
}