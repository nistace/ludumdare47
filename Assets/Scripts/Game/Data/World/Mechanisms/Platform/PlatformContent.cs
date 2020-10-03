using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD47 {
	public class PlatformContent : MonoBehaviour {
		private void OnTriggerEnter(Collider other) => other.transform.SetParent(transform);

		private void OnTriggerExit(Collider other) => other.transform.SetParent(null);
	}
}