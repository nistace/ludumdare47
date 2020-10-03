using LD47.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD47 {
	public class MenuController : MonoBehaviour {
		[SerializeField] protected MenuUi _menuUi;
		[SerializeField] protected string _firstSceneName;

		private void OnEnable() => _menuUi.onStartClicked.AddListenerOnce(StartGame);
		private void OnDisable() => _menuUi.onStartClicked.RemoveListener(StartGame);

		private void StartGame() => SceneManager.LoadScene(_firstSceneName);
	}
}