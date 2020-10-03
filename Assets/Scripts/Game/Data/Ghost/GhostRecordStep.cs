using System;
using UnityEngine;

namespace LD47 {
	[Serializable]
	public struct GhostRecordStep {
		[SerializeField] private float      _timeInLoop;
		[SerializeField] private Vector3    _position;
		[SerializeField] private Quaternion _rotation;
		[SerializeField] private bool       _running;
		[SerializeField] private bool       _inTheAir;

		public float      timeInLoop => _timeInLoop;
		public Vector3    position   => _position;
		public Quaternion rotation   => _rotation;
		public bool       running    => _running;
		public bool       inTheAir   => _inTheAir;

		public GhostRecordStep(float timeInLoop, Vector3 position, Quaternion rotation, bool running, bool inTheAir) {
			_timeInLoop = timeInLoop;
			_position = position;
			_rotation = rotation;
			_running = running;
			_inTheAir = inTheAir;
		}
	}
}