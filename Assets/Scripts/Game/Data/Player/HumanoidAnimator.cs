﻿using UnityEngine;
using Utils.Audio;

namespace LD47 {
	public class HumanoidAnimator : MonoBehaviour {
		[SerializeField] protected Animator _animator;

		private static readonly int runningAnimParam       = Animator.StringToHash("Running");
		private static readonly int inTheAirAnimParam      = Animator.StringToHash("InTheAir");
		private static readonly int jumpAnimParam          = Animator.StringToHash("Jump");
		private static readonly int pickUpAnimParam        = Animator.StringToHash("PickUp");
		private static readonly int throwAnimParam         = Animator.StringToHash("Throw");
		private static readonly int carriesObjectAnimParam = Animator.StringToHash("CarriesObject");
		private static readonly int danceAnimParam         = Animator.StringToHash("Dance");
		private static readonly int dancingAnimParam       = Animator.StringToHash("Dancing");

		[SerializeField] protected AudioClip[] _footStepClips;

		public bool running  => _animator.GetBool(runningAnimParam);
		public bool inTheAir => _animator.GetBool(inTheAirAnimParam);

		public void SetRunning(bool running) => _animator.SetBool(runningAnimParam, running);
		public void SetInTheAir(bool inTheAir) => _animator.SetBool(inTheAirAnimParam, inTheAir);
		public void SetJump() => _animator.SetTrigger(jumpAnimParam);
		public void SetPickUp() => _animator.SetTrigger(pickUpAnimParam);
		public void SetThrow() => _animator.SetTrigger(throwAnimParam);
		public void SetCarriesObject(bool carry) => _animator.SetBool(carriesObjectAnimParam, carry);

		public void Dance() {
			_animator.SetBool(dancingAnimParam, true);
			_animator.SetTrigger(danceAnimParam);
		}

		public void StopDancing() => _animator.SetBool(dancingAnimParam, false);

		public void OnFootStep() {
			if (_footStepClips.Length == 0) return;
			AudioManager.Sfx.Play(_footStepClips.Random());
		}
	}
}