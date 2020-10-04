using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LD47.Ui {
	public class MenuUi : MonoBehaviour {
		[SerializeField] protected Button            _startButton;
		[SerializeField] protected AudioParametersUi _audioParameters;

		public AudioParametersUi audioParameters => _audioParameters;
		public UnityEvent onStartClicked => _startButton.onClick;
	}
}