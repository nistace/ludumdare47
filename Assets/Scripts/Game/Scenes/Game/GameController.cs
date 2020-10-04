using System.Collections.Generic;
using LD47.Camera;
using LD47.Ui;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace LD47 {
	public class GameController : MonoBehaviour {
		private enum Status {
			Intro = 0,
			Game  = 1,
			Outro = 2
		}

		[SerializeField] protected string            _levelName;
		[SerializeField] protected string            _levelHint;
		[SerializeField] protected PlayerController  _player;
		[SerializeField] protected Transform         _spawn;
		[SerializeField] protected CheckTriggerEnter _exit;
		[SerializeField] protected string            _nextScene;
		[SerializeField] protected CameraStates      _camera;
		[SerializeField] protected GameUi            _ui;
		[SerializeField] protected GameObject        _world;

		private List<Ghost>                          ghosts           { get; } = new List<Ghost>();
		private IReadOnlyList<IWorldReinitializable> reinitializables { get; set; }
		private GhostRecord                          currentRecord    { get; set; }
		private Status                               status           { get; set; }

		private void Start() {
			reinitializables = _world.GetComponentsInChildren<IWorldReinitializable>();
			_ui.gameScreen.SetPlayer(_player);
			_ui.outroScreen.onTryAgainClicked.AddListenerOnce(RestartLevel);
			_ui.outroScreen.onNextLevelClicked.AddListenerOnce(HandleGoToNextLevel);
			Start(true);
		}

		private void Start(bool first) {
			_player.enabled = false;
			_camera.MoveToAerialView(first);
			Reset();
			TimeManager.ResetAllRunsTime();
			_ui.ShowIntroScreen(_levelName, _levelHint);
			status = Status.Intro;
			Inputs.controls.Game.StartPlaying.AddPerformListenerOnce(StartPlayingLevel);
			Inputs.controls.Game.StartPlaying.Enable();
		}

		private void RestartLevel(InputAction.CallbackContext obj) => RestartLevel();

		private void RestartLevel() {
			TimeManager.StopLoop(true);
			ghosts.ForEach(GhostManager.Pool);
			ghosts.Clear();
			Start(false);
		}

		private void StartPlayingLevel(InputAction.CallbackContext obj) {
			status = Status.Game;
			Inputs.controls.Game.StartPlaying.RemovePerformListener(StartPlayingLevel);
			Inputs.controls.Game.StartPlaying.Disable();
			_camera.MoveToPlayView(false);
			_player.enabled = true;
			SetPlayingListeners(true);
			_ui.ShowGameScreen();
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
				ghost.transform.forward = Vector3.forward;
				ghost.Reinitialize();
			}
			_player.transform.position = _spawn.position;
			_player.transform.forward = Vector3.forward;
			_player.Reinitialize();
			_player.humanoid.StopDancing();
			_player.onInput.AddListenerOnce(StartLoop);
			reinitializables.ForEach(t => t.Reinitialize());
			currentRecord = _player.recorder.Reinitialize();
			Inputs.controls.Game.EndLoop.SetEnabled(false);
		}

		private void StartLoop() {
			TimeManager.StartLoop();
			_player.onInput.RemoveListener(StartLoop);
			Inputs.controls.Game.EndLoop.SetEnabled(enabled);
		}

		private void HandleExitReached() {
			if (status == Status.Outro) return;
			status = Status.Outro;
			TimeManager.StopLoop(false);
			_ui.ShowOutroScreen(TimeManager.allRunsTime, ghosts.Count + 1, !string.IsNullOrEmpty(_nextScene));
			_player.HoorayVictory();
			var playerTransform = _player.transform;
			playerTransform.position = _exit.transform.position;
			playerTransform.forward = Vector3.back;
			_player.humanoid.Dance();
			_player.enabled = false;
			_camera.MoveToLevelCompleteView(false);
			ghosts.ForEach(GhostManager.Pool);
			ghosts.Clear();
		}

		private void HandleGoToNextLevel() {
			SceneManager.LoadSceneAsync(string.IsNullOrEmpty(_nextScene) ? "Thanks" : _nextScene);
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