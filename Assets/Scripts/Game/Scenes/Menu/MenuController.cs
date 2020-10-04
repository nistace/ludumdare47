using LD47.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Audio;

namespace LD47 {
	public class MenuController : MonoBehaviour {
		[SerializeField] protected MenuUi             _menuUi;
		[SerializeField] protected string             _firstSceneName;
		[SerializeField] protected HumanoidAnimator[] _dancers;

		private void Start() {
			_dancers.ForEach(t => t.Dance());
			_menuUi.audioParameters.SetValues(AudioManager.Music.volume, AudioManager.Sfx.volume);
		}

		private void OnEnable() => SetListeners(true);
		private void OnDisable() => SetListeners(false);

		private void SetListeners(bool enable) {
			_menuUi.onStartClicked.SetListenerActive(StartGame, enable);
			_menuUi.audioParameters.onMusicValueChanged.SetListenerActive(SetMusicVolume, enable);
			_menuUi.audioParameters.onSfxValueChanged.SetListenerActive(SetSfxVolume, enable);
		}

		private static void SetMusicVolume(float value) => AudioManager.Music.volume = value;
		private static void SetSfxVolume(float value) => AudioManager.Sfx.volume = value;

		private void StartGame() => SceneManager.LoadScene(_firstSceneName);

		[ContextMenu("Find all dancers")] private void FindAllAnimators() => _dancers = FindObjectsOfType<HumanoidAnimator>();
	}
}