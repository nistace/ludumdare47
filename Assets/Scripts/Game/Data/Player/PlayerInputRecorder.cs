using UnityEngine;
using UnityEngine.InputSystem;

namespace LD47 {
	public class PlayerInputRecorder : MonoBehaviour {
		[SerializeField] protected GhostRecord _record;

		private void Start() => SetListeners(true);
		private void OnDestroy() => SetListeners(false);
		private void OnEnable() => SetListenersEnabled(true);
		private void OnDisable() => SetListenersEnabled(false);

		private void SetListeners(bool enabled) {
			Inputs.controls.Player.Movement.SetAnyListenerOnce(HandleMovement, enabled);
			Inputs.controls.Player.Jump.SetPerformListenerOnce(HandleJump, enabled);
		}

		private static void SetListenersEnabled(bool enabled) {
			Inputs.controls.Player.Movement.SetEnabled(enabled);
			Inputs.controls.Player.Jump.SetEnabled(enabled);
		}

		private void HandleMovement(InputAction.CallbackContext obj) {
			_record.AddMovementRecord(new GhostRecordVector2(TimeManager.currentLoopTime, obj.ReadValue<Vector2>()));
		}

		private void HandleJump(InputAction.CallbackContext obj) {
			_record.AddJumpRecord(TimeManager.currentLoopTime);
		}

		public GhostRecord Reinitialize() => _record = new GhostRecord();
	}
}