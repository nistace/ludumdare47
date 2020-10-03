using System.Collections.Generic;
using UnityEngine;

namespace LD47 {
	public class GhostManager : MonoBehaviour {
		private static GhostManager instance { get; set; }

		[SerializeField] protected Ghost     _prefab;
		[SerializeField] protected Transform _poolContainer;

		private Queue<Ghost> pool { get; } = new Queue<Ghost>();

		private void Awake() {
			if (!instance) instance = this;
			if (instance == this) DontDestroyOnLoad(gameObject);
			else Destroy(gameObject);
		}

		public static Ghost Instantiate() {
			if (instance.pool.Count <= 0) return Instantiate(instance._prefab);
			var pooled = instance.pool.Dequeue();
			pooled.gameObject.SetActive(true);
			return pooled;
		}

		public static void Pool(Ghost ghost) {
			if (!instance._poolContainer) instance._poolContainer = new GameObject("GhostPool").ParentedTo(instance.transform).transform;
			ghost.transform.SetParent(instance._poolContainer);
			ghost.gameObject.SetActive(false);
			instance.pool.Enqueue(ghost);
		}
	}
}