using UnityEngine;

namespace LD47.Ui {
	public class GameUi : MonoBehaviour {
		[SerializeField] protected IntroScreenUi _introScreen;
		[SerializeField] protected GameScreenUi  _gameScreen;
		[SerializeField] protected OutroScreenUi _outroScreen;

		public IntroScreenUi introScreen => _introScreen;
		public GameScreenUi  gameScreen  => _gameScreen;
		public OutroScreenUi outroScreen => _outroScreen;

		public void ShowIntroScreen(string levelName, string hint) {
			_introScreen.Show(levelName, hint);
			_gameScreen.Hide();
			_outroScreen.Hide();
		}

		public void ShowGameScreen() {
			_introScreen.Hide();
			_gameScreen.ShowAndHideAllInfos();
			_outroScreen.Hide();
		}

		public void ShowOutroScreen(float finalTime, int loops, bool hasNextLevel) {
			_introScreen.Hide();
			_gameScreen.Hide();
			_outroScreen.Show(finalTime, loops, hasNextLevel);
		}
	}
}