using TMPro;
using UnityEngine;

namespace LD47.Ui {
	public class GameUi : MonoBehaviour {
		[SerializeField] protected TMP_Text _countdown;
		[SerializeField] protected TMP_Text _totalLevelTime;
		[SerializeField] protected TMP_Text _infoStartLevel;
		[SerializeField] protected TMP_Text _infoPickUp;
		[SerializeField] protected TMP_Text _infoDrop;
		[SerializeField] protected TMP_Text _infoThrow;

		private void Update() {
			_countdown.text = DisplayTime(TimeManager.currentLoopRemainingTime, TimeManager.playing);
			_totalLevelTime.text = DisplayTime(TimeManager.allRunsTime, true);
		}

		private static string DisplayTime(float time, bool active) {
			if (!active) return "-";
			if (time > 60) return $"{(int) (time / 60)}:{(int) (time % 60):00}.{(int) (time % 1 * 1000):000}";
			return $"{(int) (time % 60):00}.{(int) ((time % 1) * 1000):000}";
		}

		public void SetStartLevelInfoVisible(bool visible) => _infoStartLevel.gameObject.SetActive(visible);

		public void SetPlayer(PlayerController player) {
			player.onObjectPickable.AddListenerOnce(DisplayInfoObjectPickable);
			player.onObjectPickedUp.AddListenerOnce(DisplayInfoObjectPickedUp);
			player.onNoObjectPickable.AddListenerOnce(HideInfoObjectPickable);
			player.onObjectDropped.AddListenerOnce(HideInfoObjectPickedUp);
		}

		private void HideInfoObjectPickedUp() {
			_infoDrop.gameObject.SetActive(false);
			_infoThrow.gameObject.SetActive(false);
		}

		private void HideInfoObjectPickable() => _infoPickUp.gameObject.SetActive(false);

		private void DisplayInfoObjectPickable(PickUpable item) {
			_infoPickUp.text = "Press E to pick up the " + item.cleanName;
			_infoPickUp.gameObject.SetActive(true);
		}

		private void DisplayInfoObjectPickedUp(PickUpable item) {
			_infoDrop.text = "Press E to drop the " + item.cleanName;
			_infoDrop.gameObject.SetActive(true);
			_infoThrow.text = "Press E to throw the " + item.cleanName;
			_infoThrow.gameObject.SetActive(true);
		}
	}
}