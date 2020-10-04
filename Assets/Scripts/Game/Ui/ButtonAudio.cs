using System;
using UnityEngine;
using UnityEngine.UI;
using Utils.Audio;

namespace Game.Ui {
	public class ButtonAudio : MonoBehaviour {
		[SerializeField] protected Button    _button;
		[SerializeField] protected AudioClip _clip;

		private void Reset() {
			_button = GetComponent<Button>();
		}

		private void Start() {
			_button.onClick.AddListenerOnce(PlayClip);
		}

		private void PlayClip() => AudioManager.Sfx.Play(_clip);
	}
}