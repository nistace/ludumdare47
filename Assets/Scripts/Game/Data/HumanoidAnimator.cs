using UnityEngine;

namespace LD47 {
	public class HumanoidAnimator : MonoBehaviour {
		[SerializeField] protected Animator _animator;

		private static readonly int runningAnimParam  = Animator.StringToHash("Running");
		private static readonly int inTheAirAnimParam = Animator.StringToHash("InTheAir");
		private static readonly int jumpAnimParam     = Animator.StringToHash("Jump");

		public bool running  => _animator.GetBool(runningAnimParam);
		public bool inTheAir => _animator.GetBool(inTheAirAnimParam);

		public void SetRunning(bool running) => _animator.SetBool(runningAnimParam, running);
		public void SetInTheAir(bool inTheAir) => _animator.SetBool(inTheAirAnimParam, inTheAir);
		public void SetJump() => _animator.SetTrigger(jumpAnimParam);
	}
}