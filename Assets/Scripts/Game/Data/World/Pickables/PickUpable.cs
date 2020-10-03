using UnityEngine;
using UnityEngine.Events;

namespace LD47 {
	public class PickUpable : MonoBehaviour {
		public class Event : UnityEvent<PickUpable> { }

		[SerializeField] protected string     _cleanName;
		[SerializeField] protected Vector3    _pickedOffset;
		[SerializeField] protected Quaternion _pickedRotation;
		[SerializeField] protected Rigidbody  _rigidbody;
		[SerializeField] protected Collider   _collider;

		private new Transform transform { get; set; }
		public      bool      pickedUp  { get; private set; }
		public      string    cleanName => _cleanName;

		private void Awake() => transform = base.transform;

		public Event onPickedUp { get; } = new Event();

		public void Pickup(Transform newParent) {
			_collider.enabled = false;
			transform.SetParent(newParent);
			_rigidbody.isKinematic = true;
			transform.localPosition = _pickedOffset;
			transform.localRotation = _pickedRotation;
			pickedUp = true;
			onPickedUp.Invoke(this);
		}

		public void Throw(Vector3 force) {
			transform.SetParent(null);
			_rigidbody.isKinematic = false;
			pickedUp = false;
			_collider.enabled = true;
			_rigidbody.AddForce(force);
		}

		[ContextMenu("Get picked data from current transform values")]
		private void ApplyPickedData() {
			_pickedOffset = base.transform.localPosition;
			_pickedRotation = base.transform.localRotation;
		}
	}
}