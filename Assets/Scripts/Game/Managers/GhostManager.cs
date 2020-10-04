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

		public static Ghost Instantiate() => Instantiate(instance._prefab); /*{
			if (instance.pool.Count <= 0) return Instantiate(instance._prefab);
			var pooled = instance.pool.Dequeue();
			pooled.gameObject.SetActive(true);
			pooled.transform.SetParent(null);
			return pooled;
		}*/

		public static void Pool(Ghost ghost) => Destroy(ghost.gameObject); /*{
			if (!instance._poolContainer) instance._poolContainer = new GameObject("GhostPool").ParentedTo(instance.transform).transform;
			ghost.Reinitialize();
			var ghostTransform = ghost.transform;
			ghostTransform.SetParent(instance._poolContainer);
			ghostTransform.position = Vector3.zero;
			ghost.gameObject.SetActive(false);
			instance.pool.Enqueue(ghost);
		}*/
	}
}