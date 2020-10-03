using UnityEngine;

namespace LD47 {
	public class FlagLight : MonoBehaviour {
		[SerializeField] protected RequirePowerMechanism _flagMechanism;
		[SerializeField] protected Light                 _light;

		private void Start() {
			_flagMechanism.onMechanismPowerChanged.AddListenerOnce(SetLightOn);
			SetLightOn(_flagMechanism.isPowerProvided);
		}

		private void SetLightOn(bool enabled) => _light.enabled = enabled;
	}
}