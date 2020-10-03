using UnityEngine;

public class FollowTarget : MonoBehaviour {
	[SerializeField] protected Transform _target;
	[SerializeField] protected float     _smooth = 1;
	[SerializeField] protected Vector3   _currentVelocity;

	private void FixedUpdate() {
		if (!_target) return;
		transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref _currentVelocity, _smooth);
	}
}