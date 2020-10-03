using UnityEngine;

public class LightOscillator : MonoBehaviour {
	[SerializeField] protected Light _light;
	[SerializeField] protected float _intensityMin = .5f;
	[SerializeField] protected float _intensityMax = 1f;
	[SerializeField] protected float _speed        = 1;

	private void Update() {
		if (!_light.enabled) return;
		_light.intensity = Mathf.Lerp(_intensityMin, _intensityMax, (Mathf.Sin(Time.time * _speed) + 1) / 2);
	}
}