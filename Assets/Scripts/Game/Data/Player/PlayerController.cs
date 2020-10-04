using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils.Audio;

namespace LD47 {
	public class PlayerController : MonoBehaviour {
		[SerializeField] protected Humanoid _humanoid;

		[Header("Audio")] [SerializeField] protected AudioClip[] _victoryClips;
		[SerializeField]                   protected AudioClip[] _jumpClips;
		[SerializeField]                   protected AudioClip[] _pickUpClips;
		[SerializeField]                   protected AudioClip[] _throwClips;

		public     Humanoid          humanoid      => _humanoid;
		private    PickUpableChecker pickablesArea => humanoid.pickablesArea;
		public new Transform         transform     { get; private set; }
		public     GhostRecord       record        { get; private set; }

		public UnityEvent       onInput            { get; } = new UnityEvent();
		public PickUpable.Event onObjectPickable   { get; } = new PickUpable.Event();
		public UnityEvent       onNoObjectPickable { get; } = new UnityEvent();
		public PickUpable.Event onObjectPickedUp   { get; } = new PickUpable.Event();
		public UnityEvent       onObjectDropped    { get; } = new UnityEvent();

		private void Awake() => transform = base.transform;

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

		private static void SetListenersEnabled(bool enabled) {
			Inputs.controls.Player.Movement.SetEnabled(enabled);
			Inputs.controls.Player.Jump.SetEnabled(enabled);
			Inputs.controls.Player.PickUp.SetEnabled(enabled);
			Inputs.controls.Player.Throw.SetEnabled(enabled);
		}

		private void FixedUpdate() {
			if (!TimeManager.playing) return;
			_humanoid.ManualUpdate();
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

		private void HandleJump(InputAction.CallbackContext obj) {
			record.AddJumpRecord(TimeManager.currentLoopTime);
			if (!_humanoid.Jump()) return;
			PlayRandomClip(_jumpClips);
			onInput.Invoke();
		}

		private void HandleMovement(InputAction.CallbackContext obj) {
			var movementInput = obj.ReadValue<Vector2>();
			if (_humanoid.waitForZeroMovement) {
				if (movementInput == Vector2.zero) _humanoid.waitForZeroMovement = false;
				return;
			}
			record.AddMovementRecord(new GhostRecordVector2(TimeManager.currentLoopTime, movementInput));
			_humanoid.SetMovement(movementInput);
			if (movementInput != Vector2.zero) onInput.Invoke();
		}

		private void HandlePickUp(InputAction.CallbackContext obj) {
			record.AddPickUpOrDropRecord(TimeManager.currentLoopTime);
			if (!_humanoid.PickUpOrDrop()) return;
			onInput.Invoke();
			if (humanoid.pickedObject) {
				PlayRandomClip(_pickUpClips);
				onObjectPickedUp.Invoke(humanoid.pickedObject);
				onNoObjectPickable.Invoke();
			}
			else {
				onObjectDropped.Invoke();
				if (pickablesArea.anyPickable) onObjectPickable.Invoke(pickablesArea.any);
			}
		}

		private void HandleThrow(InputAction.CallbackContext obj) {
			record.AddThrowRecord(TimeManager.currentLoopTime);
			if (!_humanoid.Throw()) return;
			PlayRandomClip(_throwClips);
			onInput.Invoke();
			onObjectDropped.Invoke();
			if (pickablesArea.anyPickable) onObjectPickable.Invoke(pickablesArea.any);
		}

		public void Reinitialize() {
			record = new GhostRecord();
			_humanoid.Reinitialize();
			onObjectDropped.Invoke();
		}

		public void HoorayVictory() => PlayRandomClip(_victoryClips);

		private static void PlayRandomClip(IReadOnlyCollection<AudioClip> clips) {
			if (clips != null && clips.Count > 0) AudioManager.Sfx.Play(clips.Random());
		}
	}
}