using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace LD47 {
	public class PlayerController : MonoBehaviour {
		[SerializeField] protected Humanoid            _humanoid;
		[SerializeField] protected PlayerInputRecorder _recorder;

		public Humanoid            humanoid => _humanoid;
		public PlayerInputRecorder recorder => _recorder;

		public UnityEvent onInput { get; } = new UnityEvent();

		private void Start() => SetListeners(true);
		private void OnDestroy() => SetListeners(false);

		private void OnEnable() {
			if (_humanoid.waitForZeroMovement && Inputs.controls.Player.Movement.ReadValue<Vector2>() == Vector2.zero) _humanoid.waitForZeroMovement = false;
			SetListenersEnabled(true);
		}

		private void OnDisable() => SetListenersEnabled(false);

		private void SetListeners(bool enabled) {
			Inputs.controls.Player.Movement.SetAnyListenerOnce(HandleMovement, enabled);
			Inputs.controls.Player.Jump.SetPerformListenerOnce(HandleJump, enabled);
		}

		private static void SetListenersEnabled(bool enabled) {
			Inputs.controls.Player.Movement.SetEnabled(enabled);
			Inputs.controls.Player.Jump.SetEnabled(enabled);
		}

		private void HandleJump(InputAction.CallbackContext obj) {
			if (_humanoid.Jump()) onInput.Invoke();
		}

		private void HandleMovement(InputAction.CallbackContext obj) {
			var movementInput = obj.ReadValue<Vector2>();
			if (_humanoid.waitForZeroMovement) {
				if (movementInput == Vector2.zero) _humanoid.waitForZeroMovement = false;
				return;
			}
			_humanoid.SetMovement(movementInput);
			if (movementInput != Vector2.zero) onInput.Invoke();
		}

		public void Reinitialize() {
			_humanoid.Reinitialize();
		}
	}
}