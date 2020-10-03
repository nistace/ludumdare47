using UnityEngine;
using UnityEngine.Events;

namespace LD47 {
	public class Humanoid : MonoBehaviour {
		[SerializeField] protected Rigidbody        _rigidbody;
		[SerializeField] protected float            _jumpForce = 50;
		[SerializeField] protected OnGroundChecker  _onGroundChecker;
		[SerializeField] protected HumanoidAnimator _animator;
		[SerializeField] protected float            _movementSpeed;
		[SerializeField] protected float            _minTransformY           = -5;
		[SerializeField] protected float            _speedSmoothnessOnGround = .2f;
		[SerializeField] protected float            _speedSmoothnessInAir    = 1f;
		[SerializeField] protected Vector3          _acceleration;

		public  bool    waitForZeroMovement { get; set; }
		public  Vector3 movementInput       { get; set; }
		private bool    dead                { get; set; }

		public UnityEvent onDie { get; } = new UnityEvent();

		private void FixedUpdate() {
			if (dead) return;
			CheckAlive();
			if (dead) return;
			if (waitForZeroMovement) {
				_animator.SetInTheAir(false);
				return;
			}
			_animator.SetInTheAir(!_onGroundChecker.check);
			_animator.SetRunning(_onGroundChecker.check && movementInput != Vector3.zero);
			var currentVelocity = _rigidbody.velocity;
			var expectedMovement = Vector3.SmoothDamp(currentVelocity, movementInput.With(y: currentVelocity.y), ref _acceleration,
				_onGroundChecker.check ? _speedSmoothnessOnGround : _speedSmoothnessInAir);
			_rigidbody.velocity = !_onGroundChecker.check && _rigidbody.SweepTest(expectedMovement.With(y: 0), out _, expectedMovement.magnitude * Time.fixedDeltaTime)
				? new Vector3(0, _rigidbody.velocity.y, 0)
				: expectedMovement;
			if (movementInput != Vector3.zero) transform.forward = movementInput;
		}

		public void SetMovement(Vector2 movementInput) {
			this.movementInput = _movementSpeed * movementInput.FromGround();
		}

		public bool Jump() {
			if (!_onGroundChecker.check) return false;
			_rigidbody.AddForce(_jumpForce * Vector3.up);
			_animator.SetJump();
			return true;
		}

		private void CheckAlive() {
			if (transform.position.y >= _minTransformY) return;
			dead = true;
			onDie.Invoke();
		}

		public void Reinitialize() {
			dead = false;
			movementInput = Vector3.zero;
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
		}
	}
}