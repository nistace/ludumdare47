using LD47.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD47 {
	public class MenuController : MonoBehaviour {
		[SerializeField] protected MenuUi _menuUi;

		private void OnEnable() => _menuUi.onStartClicked.AddListenerOnce(StartGame);
		private void OnDisable() => _menuUi.onStartClicked.RemoveListener(StartGame);

		private static void StartGame() => SceneManager.LoadScene("Game");
	}
}