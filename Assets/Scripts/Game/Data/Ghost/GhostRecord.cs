using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD47 {
	[Serializable]
	public class GhostRecord {
		[SerializeField] protected List<GhostRecordStep> _stepsPerTimeInLoop = new List<GhostRecordStep>();

		public void AddStep(GhostRecordStep step) => _stepsPerTimeInLoop.Add(step);

		private (GhostRecordStep previous, GhostRecordStep next, float lerp) GetCurrentStep(float timeInLoop, ref int stepIndex) {
			while (_stepsPerTimeInLoop.Count > stepIndex + 1 && _stepsPerTimeInLoop[stepIndex + 1].timeInLoop < timeInLoop) stepIndex++;
			if (_stepsPerTimeInLoop.Count == stepIndex + 1 || _stepsPerTimeInLoop[stepIndex].timeInLoop == timeInLoop) return (_stepsPerTimeInLoop[stepIndex], _stepsPerTimeInLoop[stepIndex], 0);
			var previous = _stepsPerTimeInLoop[stepIndex];
			var next = _stepsPerTimeInLoop[stepIndex + 1];
			return (previous, next, (timeInLoop - previous.timeInLoop) / (next.timeInLoop - previous.timeInLoop));
		}

		public Vector3 GetStepPosition(float timeInLoop, ref int stepIndex) {
			(var previous, var next, var lerp) = GetCurrentStep(timeInLoop, ref stepIndex);
			return Vector3.Lerp(previous.position, next.position, lerp);
		}

		public Quaternion GetStepRotation(float timeInLoop, ref int stepIndex) {
			(var previous, var next, var lerp) = GetCurrentStep(timeInLoop, ref stepIndex);
			return Quaternion.Lerp(previous.rotation, next.rotation, lerp);
		}

		public bool GetStepRunning(float timeInLoop, ref int stepIndex) {
			(var previous, var next, var lerp) = GetCurrentStep(timeInLoop, ref stepIndex);
			return lerp < .5f ? previous.running : next.running;
		}

		public bool GetStepInTheAir(float timeInLoop, ref int stepIndex) {
			(var previous, var next, var lerp) = GetCurrentStep(timeInLoop, ref stepIndex);
			return lerp < .5f ? previous.inTheAir : next.inTheAir;
		}
	}
}