using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LD47.Ui {
	public class OutroScreenUi : MonoBehaviour {
		[SerializeField] protected TMP_Text _finalTimeText;
		[SerializeField] protected TMP_Text _loopsText;
		[SerializeField] protected Button   _nextLevel;
		[SerializeField] protected TMP_Text _nextLevelButtonText;
		[SerializeField] protected Button   _tryAgain;

		public UnityEvent onNextLevelClicked => _nextLevel.onClick;
		public UnityEvent onTryAgainClicked  => _tryAgain.onClick;

		public void Show(float finalTime, int loops, bool nextLevel) {
			_finalTimeText.text = TimeManager.DisplayTime(finalTime);
			_loopsText.text = $"{loops}";
			_nextLevelButtonText.text = nextLevel ? "Next level" : "That's it!";
			gameObject.SetActive(true);
		}

		public void Hide() {
			gameObject.SetActive(false);
		}
	}
}