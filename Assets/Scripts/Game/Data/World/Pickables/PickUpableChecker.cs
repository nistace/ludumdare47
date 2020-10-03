using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD47 {
	public class PickUpableChecker : MonoBehaviour {
		private HashSet<PickUpable> pickUpablesInZone { get; } = new HashSet<PickUpable>();
		public  bool                anyPickable       => pickUpablesInZone.Any(t => !t.pickedUp);
		public  PickUpable          any               => pickUpablesInZone.FirstOrDefault(t => !t.pickedUp);

		public PickUpable.Event onPickableInArea   { get; } = new PickUpable.Event();
		public PickUpable.Event onPickableExitArea { get; } = new PickUpable.Event();
		public object           count              => pickUpablesInZone.Count;

		private void OnTriggerEnter(Collider other) {
			var pickable = other.GetComponentInParent<PickUpable>();
			if (!pickable) return;
			if (pickUpablesInZone.Contains(pickable)) return;
			pickUpablesInZone.Add(pickable);
			pickable.onPickedUp.AddListenerOnce(RemovePickable);
			onPickableInArea.Invoke(pickable);
		}

		private void OnTriggerExit(Collider other) => RemovePickable(other.GetComponentInParent<PickUpable>());

		private void RemovePickable(PickUpable pickable) {
			if (!pickable) return;
			if (!pickUpablesInZone.Contains(pickable)) return;
			pickable.onPickedUp.RemoveListener(RemovePickable);
			pickUpablesInZone.Remove(pickable);
			onPickableExitArea.Invoke(pickable);
		}

		public void Reinitialize() => pickUpablesInZone.Clear();
	}
}