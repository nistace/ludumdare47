using TMPro;
using UnityEngine;

namespace LD47.Ui {
	public class GameScreenUi : MonoBehaviour {
		[SerializeField] protected TMP_Text _countdown;
		[SerializeField] protected TMP_Text _totalLevelTime;
		[SerializeField] protected TMP_Text _infoPickUp;
		[SerializeField] protected TMP_Text _infoDrop;
		[SerializeField] protected TMP_Text _infoThrow;

		private void Update() {
			_countdown.text = TimeManager.playing ? TimeManager.DisplayTime(TimeManager.currentLoopRemainingTime) : "-";
			_totalLevelTime.text = TimeManager.DisplayTime(TimeManager.allRunsTime);
		}

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
			_infoThrow.text = "Press F to throw the " + item.cleanName;
			_infoThrow.gameObject.SetActive(true);
		}

		public void ShowAndHideAllInfos() {
			gameObject.SetActive(true);
			_infoPickUp.gameObject.SetActive(false);
			_infoDrop.gameObject.SetActive(false);
			_infoThrow.gameObject.SetActive(false);
		}

		public void Hide() => gameObject.SetActive(false);
	}
}