using UnityEngine;
using Utils.Events;

namespace LD47 {
	public class RequirePowerMechanism : MonoBehaviour {
		[SerializeField] protected ProvidePowerMechanism[] _providers;

		public int  countActiveProviders { get; private set; }
		public bool isPowerProvided      => countActiveProviders > 0;

		public BoolEvent onMechanismPowerChanged { get; } = new BoolEvent();

		private void Start() {
			foreach (var provider in _providers) {
				if (provider.isProvidingPower) countActiveProviders++;
				provider.onProvidingPowerChanged.AddListenerOnce(HandleProviderChangedStatus);
			}
			onMechanismPowerChanged.Invoke(isPowerProvided);
		}

		private void HandleProviderChangedStatus(bool providing) {
			if (providing) {
				countActiveProviders++;
				if (countActiveProviders == 1) onMechanismPowerChanged.Invoke(isPowerProvided);
			}
			else {
				countActiveProviders--;
				if (countActiveProviders == 0) onMechanismPowerChanged.Invoke(isPowerProvided);
			}
		}
	}
}