using UnityEngine;

namespace LD47.Camera {
	public class CameraStates : MonoBehaviour {
		[SerializeField] protected Transform _aerialView;
		[SerializeField] protected Transform _playView;
		[SerializeField] protected Transform _levelCompleteView;
		[SerializeField] protected float     _transitionTime = .5f;

		private new Transform transform { get; set; }
		private     Transform target    { get; set; }
		private     float     lerp      { get; set; }

		public void MoveToPlayView(bool jump) => MoveTo(_playView, jump);
		public void MoveToAerialView(bool jump) => MoveTo(_aerialView, jump);
		public void MoveToLevelCompleteView(bool jump) => MoveTo(_levelCompleteView, jump);

		private void Awake() {
			transform = base.transform;
		}

		private void MoveTo(Transform target, bool jump) {
			this.target = target;
			lerp = 0;
			if (jump) SetLerp(1);
		}

		private void Update() {
			if (lerp >= 1) return;
			SetLerp(Time.deltaTime / _transitionTime);
		}

		private void SetLerp(float lerp) {
			this.lerp = lerp;
			transform.position = Vector3.Lerp(transform.position, target.position, lerp);
			transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, lerp);
		}

		[ContextMenu("Set aerial view at current position")]
		private void SetAerialViewAtCurrentPosition() {
			if (!_aerialView) _aerialView = new GameObject("AerialView").ParentedTo(base.transform.parent).transform;
			_aerialView.position = base.transform.position;
			_aerialView.rotation = base.transform.rotation;
		}

		[ContextMenu("Set play view at current position")]
		private void SetPlayViewAtCurrentPosition() {
			if (!_playView) _playView = new GameObject("PlayView").ParentedTo(base.transform.parent).transform;
			_playView.position = base.transform.position;
			_playView.rotation = base.transform.rotation;
		}

		[ContextMenu("Set level complete view at current position")]
		private void SetLevelCompleteViewAtCurrentPosition() {
			if (!_levelCompleteView) _levelCompleteView = new GameObject("LevelCompleteView").ParentedTo(base.transform.parent).transform;
			_levelCompleteView.position = base.transform.position;
			_levelCompleteView.rotation = base.transform.rotation;
		}

		[ContextMenu("Move to aerial view")]
		private void MoveToAerialView() {
			if (!_aerialView) return;
			base.transform.position = _aerialView.position;
			base.transform.rotation = _aerialView.rotation;
		}

		[ContextMenu("Move to play view")]
		private void MoveToPlayView() {
			if (!_playView) return;
			base.transform.position = _playView.position;
			base.transform.rotation = _playView.rotation;
		}

		[ContextMenu("Move to level complete view")]
		private void MoveToLevelCompleteView() {
			if (!_levelCompleteView) return;
			base.transform.position = _levelCompleteView.position;
			base.transform.rotation = _levelCompleteView.rotation;
		}
	}
}