using UnityEngine;

namespace LD47 {
	public class Ghost : MonoBehaviour {
		[SerializeField] protected Humanoid    _humanoid;
		[SerializeField] protected GhostRecord _record;

		private void FixedUpdate() {
			if (!TimeManager.playing) return;
			_record.Progress(TimeManager.currentLoopTime);
			if (_record.movementUpdated) _humanoid.SetMovement(_record.movementInput);
			if (_record.jumpTriggered) _humanoid.Jump();
		}

		public void Initialise(GhostRecord record) {
			_record = record;
			ResetForNextLoop();
		}

		public void ResetForNextLoop() {
			_humanoid.Reinitialize();
			_record.Reinitialize();
		}
	}
}