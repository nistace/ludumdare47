using LD47;
using UnityEngine;

public class ButtonMechanism : MonoBehaviour {
	[SerializeField] protected Transform             _pressPart;
	[SerializeField] protected CheckTriggerEnter     _pressCheck;
	[SerializeField] protected Vector3               _onPosition;
	[SerializeField] protected Vector3               _offPosition;
	[SerializeField] protected RequirePowerMechanism _requirePowerMechanism;
	[SerializeField] protected ProvidePowerMechanism _providePowerMechanism;
	[SerializeField] protected Light                 _buttonLight;

	private void Start() {
		_requirePowerMechanism.onMechanismPowerChanged.AddListenerOnce(RefreshProvider);
		_pressCheck.onSomethingEntered.AddListenerOnce(RefreshProvider);
		_pressCheck.onSomethingExited.AddListenerOnce(RefreshProvider);
		RefreshProvider();
	}

	private void RefreshProvider(bool _) => RefreshProvider();

	private void RefreshProvider() {
		var active = _pressCheck.isSomethingThere && _requirePowerMechanism.isPowerProvided;
		_buttonLight.enabled = active;
		_pressPart.localPosition = active ? _onPosition : _offPosition;
		_providePowerMechanism.SetProvidingPower(active);
	}
}