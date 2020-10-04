using System.Collections.Generic;
using UnityEngine;

namespace LD47 {
	public class GhostRecord {
		private List<GhostRecordVector2> movementRecords             { get; } = new List<GhostRecordVector2>();
		private List<float>              jumpRecords                 { get; } = new List<float>();
		private List<float>              pickUpOrDropRecords         { get; } = new List<float>();
		private List<float>              throwRecords                { get; } = new List<float>();
		private int                      lastMovementRecordIndex     { get; set; }
		private int                      lastPickUpOrDropRecordIndex { get; set; }
		private int                      lastThrowRecordIndex        { get; set; }
		private int                      lastJumpRecordIndex         { get; set; }
		public  bool                     jumpTriggered               { get; private set; }
		public  bool                     pickUpOrDropTriggered       { get; private set; }
		public  bool                     throwTriggered              { get; private set; }
		public  Vector2                  movementInput               { get; private set; }
		public  bool                     movementUpdated             { get; private set; }

		public void AddMovementRecord(GhostRecordVector2 record) {
			if (movementRecords.Count > 0 && movementRecords.Last().time >= record.time) movementRecords.RemoveAt(movementRecords.Count - 1);
			movementRecords.Add(record);
		}

		public void AddJumpRecord(float record) {
			if (jumpRecords.Count > 0 && jumpRecords.Last() == record) return;
			jumpRecords.Add(record);
		}

		public void AddPickUpOrDropRecord(float record) {
			if (pickUpOrDropRecords.Count > 0 && pickUpOrDropRecords.Last() == record) return;
			pickUpOrDropRecords.Add(record);
		}

		public void AddThrowRecord(float record) {
			if (throwRecords.Count > 0 && throwRecords.Last() == record) return;
			throwRecords.Add(record);
		}

		public void Reinitialize() {
			lastMovementRecordIndex = -1;
			lastJumpRecordIndex = -1;
			lastPickUpOrDropRecordIndex = -1;
			lastThrowRecordIndex = -1;
			lastJumpRecordIndex = -1;
			jumpTriggered = false;
			pickUpOrDropTriggered = false;
			throwTriggered = false;
			movementInput = Vector2.zero;
			movementUpdated = false;
		}

		public void Progress(float newTime) {
			movementUpdated = false;
			while (lastMovementRecordIndex + 1 < movementRecords.Count && movementRecords[lastMovementRecordIndex + 1].time <= newTime) {
				lastMovementRecordIndex++;
				movementInput = movementRecords[lastMovementRecordIndex].input;
				movementUpdated = true;
			}

			jumpTriggered = false;
			while (lastJumpRecordIndex + 1 < jumpRecords.Count && jumpRecords[lastJumpRecordIndex + 1] <= newTime) {
				lastJumpRecordIndex++;
				jumpTriggered = true;
			}

			pickUpOrDropTriggered = false;
			while (lastPickUpOrDropRecordIndex + 1 < pickUpOrDropRecords.Count && pickUpOrDropRecords[lastPickUpOrDropRecordIndex + 1] <= newTime) {
				lastPickUpOrDropRecordIndex++;
				pickUpOrDropTriggered = true;
			}

			throwTriggered = false;
			while (lastThrowRecordIndex + 1 < throwRecords.Count && throwRecords[lastThrowRecordIndex + 1] <= newTime) {
				lastThrowRecordIndex++;
				throwTriggered = true;
			}
		}
	}
}