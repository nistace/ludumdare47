using UnityEngine;
using UnityEngine.Events;

namespace LD47 {
	public class Humanoid : MonoBehaviour {
		[SerializeField] protected Rigidbody         _rigidbody;
		[SerializeField] protected float             _jumpForce = 50;
		[SerializeField] protected OnGroundChecker   _onGroundChecker;
		[SerializeField] protected HumanoidAnimator  _animator;
		[SerializeField] protected float             _movementSpeed;
		[SerializeField] protected float             _minTransformY           = -5;
		[SerializeField] protected float             _speedSmoothnessOnGround = .2f;
		[SerializeField] protected float             _speedSmoothnessInAir    = 1f;
		[SerializeField] protected Vector3           _acceleration;
		[SerializeField] protected PickUpableChecker _pickablesArea;
		[SerializeField] protected PickUpable        _pickedObject;
		[SerializeField] protected Transform         _pickUpAnchor;
		[SerializeField] protected float             _throwForce = 2000;
		[SerializeField] protected float             _throwAngle = 1;

		public  PickUpable        pickedObject        => _pickedObject;
		public  PickUpableChecker pickablesArea       => _pickablesArea;
		public  bool              waitForZeroMovement { get; set; }
		public  Vector3           movementInput       { get; set; }
		private bool              dead                { get; set; }

		public UnityEvent onDie { get; } = new UnityEvent();

		public void ManualUpdate() {
			if (dead || !CheckAlive()) return;
			if (waitForZeroMovement) {
				_animator.SetInTheAir(false);
				return;
			}
			_animator.SetInTheAir(!_onGroundChecker.check);
			_animator.SetRunning(_onGroundChecker.check && movementInput != Vector3.zero);
			var currentVelocity = _rigidbody.velocity;
			var expectedMovement = Vector3.SmoothDamp(currentVelocity, movementInput.With(y: currentVelocity.y), ref _acceleration,
				_onGroundChecker.check ? _speedSmoothnessOnGround : _speedSmoothnessInAir);
			_rigidbody.velocity = CheckImminentCollisionWithWalls(expectedMovement);
			if (movementInput != Vector3.zero) transform.forward = movementInput;
		}

		private Vector3 CheckImminentCollisionWithWalls(Vector3 expectedMovement) {
			if (expectedMovement.With(y: 0) == Vector3.zero) return expectedMovement;
			if (_onGroundChecker.check) return expectedMovement;
			if (!_rigidbody.SweepTest(expectedMovement.With(y: 0), out var hit, expectedMovement.magnitude * Time.fixedDeltaTime)) return expectedMovement;
			if (hit.collider.gameObject.layer != LayerMask.GetMask("Ground")) return expectedMovement;
			return new Vector3(0, _rigidbody.velocity.y, 0);
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

		public bool PickUpOrDrop() {
			if (_pickedObject) {
				ThrowPickedObject(Vector3.zero);
				return true;
			}
			if (!pickablesArea.anyPickable) return false;
			_animator.SetPickUp();
			_pickedObject = pickablesArea.any;
			_pickedObject.Pickup(_pickUpAnchor);
			_animator.SetCarriesObject(true);
			return true;
		}

		public bool Throw() {
			if (!_pickedObject) return false;
			ThrowPickedObject((transform.forward + _throwAngle * Vector3.up).normalized * _throwForce);
			return true;
		}

		private void ThrowPickedObject(Vector3 force) {
			_pickedObject.Throw(force);
			_animator.SetThrow();
			_animator.SetCarriesObject(false);
			_pickedObject = null;
		}

		private bool CheckAlive() {
			if (transform.position.y >= _minTransformY) return true;
			dead = true;
			onDie.Invoke();
			return false;
		}

		public void Reinitialize() {
			dead = false;
			movementInput = Vector3.zero;
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
			if (_pickedObject) _pickedObject.Throw(Vector3.zero);
			_pickedObject = null;
			_animator.SetInTheAir(false);
			_animator.SetRunning(false);
			_animator.SetCarriesObject(false);
			_pickablesArea.Reinitialize();
		}

		public void Dance() => _animator.Dance();
		public void StopDancing() => _animator.StopDancing();
	}
}