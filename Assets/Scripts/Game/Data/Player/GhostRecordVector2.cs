using System;
using UnityEngine;

namespace LD47 {
	[Serializable]
	public class GhostRecordVector2 {
		[SerializeField] protected float   _time;
		[SerializeField] protected Vector2 _input;

		public float   time  => _time;
		public Vector2 input => _input;

		public GhostRecordVector2(float time, Vector2 movementInput) {
			_time = time;
			_input = movementInput;
		}
	}
}