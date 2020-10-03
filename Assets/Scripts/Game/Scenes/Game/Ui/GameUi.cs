using TMPro;
using UnityEngine;

namespace LD47.Ui {
	public class GameUi : MonoBehaviour {
		[SerializeField] protected TMP_Text _countdown;

		private void Update() {
			_countdown.text = DisplayTimer();
		}

		private static string DisplayTimer() {
			if (!TimeManager.playing) return "-";
			var time = TimeManager.currentLoopRemainingTime;
			if (time > 60) return time / 60 + ":" + (int) (time % 60) + "." + (int) (time.DecimalPart() * 1000);
			return (int) (time % 60) + "." + (int) (time.DecimalPart() * 1000);
		}
	}
}