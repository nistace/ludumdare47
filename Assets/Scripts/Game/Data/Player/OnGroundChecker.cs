using System.Collections.Generic;
using UnityEngine;

namespace LD47 {
	public class OnGroundChecker : MonoBehaviour {
		private HashSet<GameObject> collidingObjects { get; } = new HashSet<GameObject>();
		public  bool                check            => collidingObjects.Count > 0;

		private void OnTriggerEnter(Collider other) => collidingObjects.Add(other.gameObject);

		private void OnTriggerExit(Collider other) => collidingObjects.Remove(other.gameObject);
	}
}