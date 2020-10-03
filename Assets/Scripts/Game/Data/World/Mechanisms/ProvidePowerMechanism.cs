using System;
using UnityEngine;
using Utils.Events;

namespace LD47 {
	public class ProvidePowerMechanism : MonoBehaviour {
		[SerializeField] protected bool _providesPowerOnStart = true;

		public bool isProvidingPower { get; private set; }

		public BoolEvent onProvidingPowerChanged { get; } = new BoolEvent();

		private void Start() => SetProvidingPower(_providesPowerOnStart);

		public void SetProvidingPower(bool providingPower) {
			if (isProvidingPower == providingPower) return;
			isProvidingPower = providingPower;
			onProvidingPowerChanged.Invoke(isProvidingPower);
		}
	}
}