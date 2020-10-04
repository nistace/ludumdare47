using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD47 {
	public class ThanksController : MonoBehaviour {
		[SerializeField] protected float              _gotoMenuDelay = 8;
		[SerializeField] protected HumanoidAnimator[] _dancers;

		private void Start() {
			_dancers.ForEach(t => t.Dance());
		}

		private void Update() {
			if (_gotoMenuDelay <= 0) return;
			_gotoMenuDelay -= Time.deltaTime;
			if (_gotoMenuDelay <= 0) SceneManager.LoadSceneAsync("Menu");
		}

		[ContextMenu("Find all dancers")] private void FindAllAnimators() => _dancers = FindObjectsOfType<HumanoidAnimator>();
	}
}