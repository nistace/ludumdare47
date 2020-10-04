using UnityEngine;

namespace LD47 {
	public class GhostRecordVector2 {
		public float   time  { get; }
		public Vector2 input { get; }

		public GhostRecordVector2(float time, Vector2 movementInput) {
			this.time = time;
			input = movementInput;
		}
	}
}