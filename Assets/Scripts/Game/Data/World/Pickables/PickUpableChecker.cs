using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD47 {
	public class PickUpableChecker : MonoBehaviour {
		private HashSet<PickUpable> pickUpablesInZone { get; } = new HashSet<PickUpable>();
		public  bool                empty             => pickUpablesInZone.Count == 0;
		public  PickUpable          any               => empty ? null : pickUpablesInZone.First();

		public PickUpable.Event onPickableInArea   { get; } = new PickUpable.Event();
		public PickUpable.Event onPickableExitArea { get; } = new PickUpable.Event();
		public object           count              => pickUpablesInZone.Count;

		private void OnTriggerEnter(Collider other) {
			var pickable = other.GetComponentInParent<PickUpable>();
			if (!pickable) return;
			if (pickUpablesInZone.Contains(pickable)) return;
			pickUpablesInZone.Add(pickable);
			onPickableInArea.Invoke(pickable);
		}

		private void OnTriggerExit(Collider other) {
			var pickable = other.GetComponentInParent<PickUpable>();
			if (!pickable) return;
			if (!pickUpablesInZone.Contains(pickable)) return;
			pickUpablesInZone.Remove(pickable);
			onPickableExitArea.Invoke(pickable);
		}
	}
}