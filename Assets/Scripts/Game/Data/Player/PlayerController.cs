using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace LD47 {
	public class PlayerController : MonoBehaviour {
		[SerializeField] protected Rigidbody        _rigidbody;
		[SerializeField] protected HumanoidAnimator _animator;
		[SerializeField] protected float            _speed;
		[SerializeField] protected float            _jumpForce = 50;
		[SerializeField] protected OnGroundChecker  _onGroundChecker;
		[SerializeField] protected float            _minTransformY = -5;

		private Vector3 movementInput       { get; set; }
		private bool    waitForZeroMovement { get; set; } = true;

		public UnityEvent onInput { get; } = new UnityEvent();
		public UnityEvent onDie   { get; } = new UnityEvent();

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

		private void HandleJump(InputAction.CallbackContext obj) {
			if (!_onGroundChecker.check) return;
			_rigidbody.AddForce(_jumpForce * Vector3.up);
			_animator.SetJump();
			onInput.Invoke();
		}

		private void HandleMovement(InputAction.CallbackContext obj) {
			if (waitForZeroMovement) {
				if (obj.ReadValue<Vector2>() == Vector2.zero) waitForZeroMovement = false;
				return;
			}
			movementInput = obj.ReadValue<Vector2>().FromGround() * _speed;
			if (movementInput != Vector3.zero) onInput.Invoke();
		}

		private void FixedUpdate() {
			if (!CheckAlive()) return;
			if (waitForZeroMovement) return;
			if (_onGroundChecker.check) {
				_animator.SetInTheAir(false);
				_rigidbody.velocity = movementInput.With(y: _rigidbody.velocity.y);
				if (movementInput != Vector3.zero) transform.forward = movementInput;
				_animator.SetRunning(movementInput != Vector3.zero);
			}
			else {
				_animator.SetInTheAir(true);
				_animator.SetRunning(false);
			}
		}

		private bool CheckAlive() {
			if (transform.position.y >= _minTransformY) return true;
			onDie.Invoke();
			return false;
		}

		public void Reinitialize() {
			movementInput = Vector3.zero;
			_rigidbody.velocity = Vector3.zero;
		}
	}
}