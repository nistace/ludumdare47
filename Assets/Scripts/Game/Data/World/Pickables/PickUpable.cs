using UnityEngine;
using UnityEngine.Events;

namespace LD47 {
	public class PickUpable : MonoBehaviour {
		public class Event : UnityEvent<PickUpable> { }

		[SerializeField] protected string     _cleanName;
		[SerializeField] protected Vector3    _pickedOffset;
		[SerializeField] protected Quaternion _pickedRotation;
		[SerializeField] protected Rigidbody  _rigidbody;

		private new Transform transform { get; set; }
		public      bool      pickedUp  { get; private set; }
		public      string    cleanName => _cleanName;

		private void Awake() => transform = base.transform;

		public void Pickup(Transform newParent) {
			transform.SetParent(newParent);
			_rigidbody.isKinematic = true;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			pickedUp = true;
		}

		public void Throw(Vector3 force) {
			transform.SetParent(null);
			_rigidbody.AddForce(force);
			_rigidbody.isKinematic = false;
			pickedUp = false;
		}

		[ContextMenu("Get picked data from current transform values")]
		private void ApplyPickedData() {
			_pickedOffset = base.transform.localPosition;
			_pickedRotation = base.transform.localRotation;
		}
	}
}