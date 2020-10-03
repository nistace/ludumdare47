using TMPro;
using UnityEngine;

namespace LD47.Ui {
	public class GameUi : MonoBehaviour {
		[SerializeField] protected TMP_Text   _countdown;
		[SerializeField] protected TMP_Text   _totalLevelTime;
		[SerializeField] protected GameObject _startLevelInfo;

		private void Update() {
			_countdown.text = DisplayTime(TimeManager.currentLoopRemainingTime, TimeManager.playing);
			_totalLevelTime.text = DisplayTime(TimeManager.allRunsTime, true);
		}

		private static string DisplayTime(float time, bool active) {
			if (!active) return "-";
			if (time > 60) return $"{(int) (time / 60)}:{(int) (time % 60):00}.{(int) (time % 1 * 1000):000}";
			return $"{(int) (time % 60):00}.{(int) ((time % 1) * 1000):000}";
		}

		public void SetStartLevelInfoVisible(bool visible) => _startLevelInfo.SetActive(visible);
	}
}