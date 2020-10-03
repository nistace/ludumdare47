using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace LD47 {
	public class PlayerController : MonoBehaviour {
		[SerializeField] protected Humanoid            _humanoid;
		[SerializeField] protected PlayerInputRecorder _recorder;

		public  Humanoid            humanoid      => _humanoid;
		private PickUpableChecker   pickablesArea => humanoid.pickablesArea;
		public  PlayerInputRecorder recorder      => _recorder;

		public UnityEvent       onInput            { get; } = new UnityEvent();
		public PickUpable.Event onObjectPickable   { get; } = new PickUpable.Event();
		public UnityEvent       onNoObjectPickable { get; } = new UnityEvent();
		public PickUpable.Event onObjectPickedUp   { get; } = new PickUpable.Event();
		public UnityEvent       onObjectDropped    { get; } = new UnityEvent();

		private void Start() => SetListeners(true);
		private void OnDestroy() => SetListeners(false);

		private void OnEnable() {
			if (_humanoid.waitForZeroMovement && Inputs.controls.Player.Movement.ReadValue<Vector2>() == Vector2.zero) _humanoid.waitForZeroMovement = false;
			SetListenersEnabled(true);
		}

		private void OnDisable() => SetListenersEnabled(false);

		private void SetListeners(bool enabled) {
			pickablesArea.onPickableInArea.AddListenerOnce(HandleObjectPickable);
			pickablesArea.onPickableExitArea.AddListenerOnce(HandleObjectNotPickable);
			Inputs.controls.Player.Movement.SetAnyListenerOnce(HandleMovement, enabled);
			Inputs.controls.Player.Jump.SetPerformListenerOnce(HandleJump, enabled);
			Inputs.controls.Player.PickUp.SetPerformListenerOnce(HandlePickUp, enabled);
			Inputs.controls.Player.Throw.SetPerformListenerOnce(HandleThrow, enabled);
		}

		private void HandleObjectPickable(PickUpable pickable) {
			if (_humanoid.pickedObject) return;
			onObjectPickable.Invoke(pickable);
		}

		private void HandleObjectNotPickable(PickUpable _) {
			if (_humanoid.pickedObject) return;
			var pickableObject = pickablesArea.any;
			if (pickableObject) onObjectPickable.Invoke(pickableObject);
			else onNoObjectPickable.Invoke();
		}

		private static void SetListenersEnabled(bool enabled) {
			Inputs.controls.Player.Movement.SetEnabled(enabled);
			Inputs.controls.Player.Jump.SetEnabled(enabled);
			Inputs.controls.Player.PickUp.SetEnabled(enabled);
			Inputs.controls.Player.Throw.SetEnabled(enabled);
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

		private void HandlePickUp(InputAction.CallbackContext obj) {
			if (!_humanoid.PickUpOrDrop()) return;
			onInput.Invoke();
			if (humanoid.pickedObject) {
				onObjectPickedUp.Invoke(humanoid.pickedObject);
				onNoObjectPickable.Invoke();
			}
			else {
				onObjectDropped.Invoke();
				if (pickablesArea.anyPickable) onObjectPickable.Invoke(pickablesArea.any);
			}
		}

		private void HandleThrow(InputAction.CallbackContext obj) {
			if (!_humanoid.Throw()) return;
			onInput.Invoke();
			onObjectDropped.Invoke();
			if (pickablesArea.anyPickable) onObjectPickable.Invoke(pickablesArea.any);
		}

		public void Reinitialize() {
			_humanoid.Reinitialize();
		}
	}
}