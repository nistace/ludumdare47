using UnityEngine;

namespace LD47 {
	public class LoopStartFixedPosition : MonoBehaviour {
		[SerializeField] protected Vector3    _position;
		[SerializeField] protected Quaternion _rotation;
		[SerializeField] protected Rigidbody  _rigidbody;
		private new                Transform  transform { get; set; }

		private void Awake() => transform = base.transform;

		private void Start() {
			TimeManager.onLoopEnded.AddListenerOnce(Reinitialize);
			Reinitialize();
		}

		private void Reinitialize() {
			transform.position = _position;
			transform.rotation = _rotation;
			if (!_rigidbody) return;
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
		}

		[ContextMenu("Get values from transform")]
		private void GetValuesFromTransform() {
			_position = base.transform.position;
			_rotation = base.transform.rotation;
		}
	}
}