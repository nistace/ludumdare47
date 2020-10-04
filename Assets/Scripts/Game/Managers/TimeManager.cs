using UnityEngine;
using UnityEngine.Events;

namespace LD47 {
	public class TimeManager : MonoBehaviour {
		[SerializeField] protected float       _loopTime = 60;
		private static             TimeManager instance { get; set; }

		private static float previousRunsTime         { get; set; }
		public static  float allRunsTime              => previousRunsTime + currentLoopTime;
		private static float startTime                { get; set; }
		public static  float currentLoopTime          { get; private set; }
		public static  float currentLoopRemainingTime => instance._loopTime - currentLoopTime;
		public static  bool  playing                  { get; private set; }

		public static UnityEvent onLoopEnded { get; } = new UnityEvent();

		private void Awake() {
			if (!instance) instance = this;
			if (instance == this) DontDestroyOnLoad(gameObject);
			else Destroy(gameObject);
		}

		public static void StartLoop() {
			currentLoopTime = 0;
			playing = true;
			startTime = Time.fixedTime;
		}

		public static void StopLoop(bool notify) {
			if (!playing) return;
			playing = false;
			previousRunsTime += currentLoopTime;
			currentLoopTime = 0;
			if (notify) onLoopEnded.Invoke();
		}

		private void Update() {
			if (!playing) return;
			currentLoopTime = Time.fixedTime - startTime;
			if (currentLoopTime >= _loopTime) StopLoop(true);
		}

		public static void ResetAllRunsTime() => previousRunsTime = 0;

		public static string DisplayTime(float time) => $"{(time > 60 ? $"{(int) (time / 60)}:" : "")}{(int) (time % 60):00}.{(int) (time % 1 * 1000):000}";
	}
}