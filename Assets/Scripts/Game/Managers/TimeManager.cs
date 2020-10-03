using UnityEngine;
using UnityEngine.Events;

namespace LD47 {
	public class TimeManager : MonoBehaviour {
		[SerializeField] protected float       _loopTime = 60;
		private static             TimeManager instance { get; set; }

		private static float startTime                { get; set; }
		public static  float currentLoopTime          { get; private set; }
		public static  float currentLoopRemainingTime => instance._loopTime - currentLoopTime;
		public static  bool  playing                  { get; private set; }

		public static UnityEvent onLoopEnded { get; } = new UnityEvent();

		private void Awake() {
			instance = this;
			startTime = 0;
			currentLoopTime = 0;
			playing = false;
		}

		public static void StartLoop() {
			playing = true;
			startTime = Time.time;
			currentLoopTime = 0;
		}

		public static void StopLoop() {
			if (!playing) return;
			playing = false;
			onLoopEnded.Invoke();
		}

		private void Update() {
			if (!playing) return;
			currentLoopTime = Time.time - startTime;
			if (currentLoopTime >= _loopTime) StopLoop();
		}
	}
}