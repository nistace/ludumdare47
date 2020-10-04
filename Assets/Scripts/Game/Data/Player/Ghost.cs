using UnityEngine;

namespace LD47 {
	public class Ghost : MonoBehaviour {
		[SerializeField] protected Humanoid    _humanoid;
		[SerializeField] protected GhostRecord _record;

		public new Transform transform { get; private set; }

		private void Awake() => transform = base.transform;

		private void OnEnable() => TimeManager.onLoopStarted.AddListenerOnce(HandleLoopStarted);
		private void OnDisable() => TimeManager.onLoopStarted.RemoveListener(HandleLoopStarted);

		private void FixedUpdate() {
			if (TimeManager.playing) Progress();
		}

		private void HandleLoopStarted() => Progress();

		private void Progress() {
			_record.Progress(TimeManager.currentLoopTime);
			if (_record.movementUpdated) _humanoid.SetMovement(_record.movementInput);
			if (_record.jumpTriggered) _humanoid.Jump();
			if (_record.pickUpOrDropTriggered) _humanoid.PickUpOrDrop();
			if (_record.throwTriggered) _humanoid.Throw();
		}

		public void Initialise(GhostRecord record) {
			_record = record;
			Reinitialize();
		}

		public void Reinitialize() {
			_humanoid.Reinitialize();
			_record.Reinitialize();
		}
	}
}