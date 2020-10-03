using System.Collections.Generic;
using LD47.Camera;
using LD47.Ui;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace LD47 {
	public class GameController : MonoBehaviour {
		[SerializeField] protected PlayerController  _player;
		[SerializeField] protected Transform         _spawn;
		[SerializeField] protected CheckTriggerEnter _exit;
		[SerializeField] protected string            _nextScene;
		[SerializeField] protected CameraStates      _camera;
		[SerializeField] protected GameUi            _ui;

		private List<Ghost> ghosts        { get; } = new List<Ghost>();
		private GhostRecord currentRecord { get; set; }

		private void Start() {
			Start(true);
			_ui.SetPlayer(_player);
		}

		private void Start(bool first) {
			_player.enabled = false;
			_camera.MoveToAerialView(first);
			Reset();
			TimeManager.ResetAllRunsTime();
			_ui.SetStartLevelInfoVisible(true);
			Inputs.controls.Game.StartPlaying.AddPerformListenerOnce(StartPlayingLevel);
			Inputs.controls.Game.StartPlaying.Enable();
		}

		private void RestartLevel(InputAction.CallbackContext obj) {
			TimeManager.StopLoop(true);
			ghosts.ForEach(GhostManager.Pool);
			ghosts.Clear();
			Start(false);
		}

		private void StartPlayingLevel(InputAction.CallbackContext obj) {
			Inputs.controls.Game.StartPlaying.RemovePerformListener(StartPlayingLevel);
			Inputs.controls.Game.StartPlaying.Disable();
			_camera.MoveToPlayView(false);
			_player.enabled = true;
			SetPlayingListeners(true);
			_ui.SetStartLevelInfoVisible(false);
		}

		private void OnDestroy() => SetPlayingListeners(false);

		private void SetPlayingListeners(bool enabled) {
			if (_exit) _exit.onSomethingEntered.SetListenerActive(HandleExitReached, enabled);
			if (_player) _player.humanoid.onDie.SetListenerActive(IntentionalStopLoop, enabled);
			Inputs.controls.Game.EndLoop.SetPerformListenerOnce(IntentionalStopLoop, enabled);
			Inputs.controls.Game.RestartLevel.SetPerformListenerOnce(RestartLevel, enabled);
			Inputs.controls.Game.RestartLevel.SetEnabled(enabled);
			TimeManager.onLoopEnded.SetListenerActive(EndLoop, enabled);
		}

		private void Reset() {
			TimeManager.StopLoop(false);
			foreach (var ghost in ghosts) {
				ghost.transform.position = _spawn.position;
				ghost.Reinitialize();
			}
			_player.transform.position = _spawn.position;
			_player.Reinitialize();
			_player.onInput.AddListenerOnce(StartLoop);
			FindObjectsOfType<CheckTriggerEnter>().ForEach(t => t.Reinitialize());
			currentRecord = _player.recorder.Reinitialize();
			Inputs.controls.Game.EndLoop.SetEnabled(false);
		}

		private void StartLoop() {
			TimeManager.StartLoop();
			_player.onInput.RemoveListener(StartLoop);
			Inputs.controls.Game.EndLoop.SetEnabled(enabled);
		}

		private void HandleExitReached() {
			TimeManager.StopLoop(false);
			ghosts.ForEach(GhostManager.Pool);
			ghosts.Clear();
			SceneManager.LoadScene(_nextScene);
		}

		private static void IntentionalStopLoop(InputAction.CallbackContext obj) => TimeManager.StopLoop(true);
		private static void IntentionalStopLoop() => TimeManager.StopLoop(true);

		private void EndLoop() {
			var newGhost = GhostManager.Instantiate();
			newGhost.Initialise(currentRecord);
			ghosts.Add(newGhost);
			Reset();
		}
	}
}