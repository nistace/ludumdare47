using UnityEngine;

namespace LD47 {
	public class PlatformContent : MonoBehaviour {
		private void OnTriggerEnter(Collider other) {
			if (other.gameObject.layer == LayerMask.NameToLayer("GroundChecker")) return;
			other.transform.SetParent(transform);
		}

		private void OnTriggerExit(Collider other) {
			if (other.gameObject.layer == LayerMask.NameToLayer("GroundChecker")) return;
			other.transform.SetParent(null);
		}
	}
}