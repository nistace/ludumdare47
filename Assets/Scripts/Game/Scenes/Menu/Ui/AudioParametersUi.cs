using UnityEngine;
using UnityEngine.UI;

public class AudioParametersUi : MonoBehaviour {
	[SerializeField] protected Slider _musicSlider;
	[SerializeField] protected Slider _sfxSlider;

	public Slider.SliderEvent onMusicValueChanged => _musicSlider.onValueChanged;
	public Slider.SliderEvent onSfxValueChanged   => _sfxSlider.onValueChanged;

	public void SetValues(float music, float sfx) {
		_musicSlider.SetValueWithoutNotify(music);
		_sfxSlider.SetValueWithoutNotify(sfx);
	}
}