using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD47 {
	public class CheckTriggerEnter : MonoBehaviour {
		private HashSet<GameObject> objectsInTrigger { get; } = new HashSet<GameObject>();
		public  bool                isSomethingThere => objectsInTrigger.Count > 0;

		public UnityEvent onSomethingEntered { get; } = new UnityEvent();
		public UnityEvent onSomethingExited  { get; } = new UnityEvent();

		private void OnTriggerEnter(Collider other) {
			if (objectsInTrigger.Contains(other.gameObject)) return;
			objectsInTrigger.Add(other.gameObject);
			onSomethingEntered.Invoke();
		}

		private void OnTriggerExit(Collider other) {
			if (!objectsInTrigger.Contains(other.gameObject)) return;
			objectsInTrigger.Remove(other.gameObject);
			onSomethingExited.Invoke();
		}

		public void Reinitialize() {
			if (objectsInTrigger.Count <= 0) return;
			objectsInTrigger.Clear();
			onSomethingExited.Invoke();
		}
	}
}