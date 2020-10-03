using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD47 {
	[Serializable]
	public class GhostRecord {
		[SerializeField] protected List<GhostRecordVector2> _movementRecords     = new List<GhostRecordVector2>();
		[SerializeField] protected List<float>              _jumpRecords         = new List<float>();
		[SerializeField] protected List<float>              _pickUpOrDropRecords = new List<float>();
		[SerializeField] protected List<float>              _throwRecords        = new List<float>();

		private int     lastMovementRecordIndex     { get; set; }
		private int     lastPickUpOrDropRecordIndex { get; set; }
		private int     lastThrowRecordIndex        { get; set; }
		private int     lastJumpRecordIndex         { get; set; }
		public  bool    jumpTriggered               { get; private set; }
		public  bool    pickUpOrDropTriggered       { get; private set; }
		public  bool    throwTriggered              { get; private set; }
		public  Vector2 movementInput               { get; private set; }
		public  bool    movementUpdated             { get; private set; }

		public void AddMovementRecord(GhostRecordVector2 record) {
			if (_movementRecords.Count > 0 && _movementRecords.Last().time == record.time) _movementRecords.RemoveAt(_movementRecords.Count - 1);
			_movementRecords.Add(record);
		}

		public void AddJumpRecord(float record) {
			if (_jumpRecords.Count > 0 && _jumpRecords.Last() == record) return;
			_jumpRecords.Add(record);
		}

		public void AddPickUpOrDropRecord(float record) {
			if (_pickUpOrDropRecords.Count > 0 && _pickUpOrDropRecords.Last() == record) return;
			_pickUpOrDropRecords.Add(record);
		}

		public void AddThrowRecord(float record) {
			if (_throwRecords.Count > 0 && _throwRecords.Last() == record) return;
			_throwRecords.Add(record);
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
			while (lastMovementRecordIndex + 1 < _movementRecords.Count && _movementRecords[lastMovementRecordIndex + 1].time <= newTime) {
				lastMovementRecordIndex++;
				movementInput = _movementRecords[lastMovementRecordIndex].input;
				movementUpdated = true;
			}

			jumpTriggered = false;
			while (lastJumpRecordIndex + 1 < _jumpRecords.Count && _jumpRecords[lastJumpRecordIndex + 1] <= newTime) {
				lastJumpRecordIndex++;
				jumpTriggered = true;
			}

			pickUpOrDropTriggered = false;
			while (lastPickUpOrDropRecordIndex + 1 < _pickUpOrDropRecords.Count && _pickUpOrDropRecords[lastPickUpOrDropRecordIndex + 1] <= newTime) {
				lastPickUpOrDropRecordIndex++;
				pickUpOrDropTriggered = true;
			}

			throwTriggered = false;
			while (lastThrowRecordIndex + 1 < _throwRecords.Count && _throwRecords[lastThrowRecordIndex + 1] <= newTime) {
				lastThrowRecordIndex++;
				throwTriggered = true;
			}
		}
	}
}