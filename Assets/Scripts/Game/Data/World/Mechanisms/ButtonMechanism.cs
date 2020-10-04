using LD47;
using UnityEngine;
using Utils.Audio;

public class ButtonMechanism : MonoBehaviour {
	[SerializeField] protected Transform             _pressPart;
	[SerializeField] protected CheckTriggerEnter     _pressCheck;
	[SerializeField] protected Vector3               _onPosition;
	[SerializeField] protected Vector3               _offPosition;
	[SerializeField] protected RequirePowerMechanism _requirePowerMechanism;
	[SerializeField] protected ProvidePowerMechanism _providePowerMechanism;
	[SerializeField] protected Light                 _buttonLight;
	[SerializeField] protected AudioClip             _activateAudioClip;
	[SerializeField] protected AudioClip             _deactivateAudioClip;

	private bool active { get; set; }

	private void Start() {
		_requirePowerMechanism.onMechanismPowerChanged.AddListenerOnce(RefreshProvider);
		_pressCheck.onSomethingEntered.AddListenerOnce(RefreshProvider);
		_pressCheck.onSomethingExited.AddListenerOnce(RefreshProvider);
		RefreshProvider();
	}

	private void RefreshProvider(bool _) => RefreshProvider();

	private void RefreshProvider() {
		var newActive = _pressCheck.isSomethingThere && _requirePowerMechanism.isPowerProvided;
		_buttonLight.enabled = newActive;
		_pressPart.localPosition = newActive ? _onPosition : _offPosition;
		_providePowerMechanism.SetProvidingPower(newActive);
		if (active != newActive) AudioManager.Sfx.Play(newActive ? _activateAudioClip : _deactivateAudioClip);
		active = newActive;
	}
}