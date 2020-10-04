using TMPro;
using UnityEngine;

namespace LD47.Ui {
	public class IntroScreenUi : MonoBehaviour {
		[SerializeField] protected TMP_Text _levelNameText;
		[SerializeField] protected TMP_Text _hintText;

		public void Show(string levelName, string hint) {
			_levelNameText.text = levelName;
			_hintText.text = $"Hint : {hint}";
			gameObject.SetActive(true);
		}

		public void Hide() => gameObject.SetActive(false);
	}
}