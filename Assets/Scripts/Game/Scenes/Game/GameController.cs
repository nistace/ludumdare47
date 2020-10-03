using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace LD47 {
	public class GameController : MonoBehaviour {
		[SerializeField] protected PlayerController  _playerController;
		[SerializeField] protected GhostRecorder     _player;
		[SerializeField] protected Ghost             _ghostPrefab;
		[SerializeField] protected Transform         _spawn;
		[SerializeField] protected CheckTriggerEnter _exit;
		[SerializeField] protected string            _nextScene;

		private List<Ghost> ghosts        { get; } = new List<Ghost>();
		private GhostRecord currentRecord { get; set; }

		private void Awake() => SetListeners(true);

		private void OnDestroy() => SetListeners(false);

		private void SetListeners(bool enabled) {
			if (_exit) _exit.onSomethingEntered.SetListenerActive(HandleExitReached, enabled);
			if (_playerController) _playerController.onDie.SetListenerActive(EndLoop, enabled);
			Inputs.controls.Game.EndLoop.SetPerformListenerOnce(EndLoop, enabled);
			TimeManager.onLoopEnded.SetListenerActive(EndLoop, enabled);
		}

		private void Start() => Reset();

		private void Reset() {
			TimeManager.StopLoop();
			_player.transform.position = _spawn.position;
			ghosts.ForEach(t => t.ResetForNextLoop());
			currentRecord = _player.Reinitialize();
			_playerController.Reinitialize();
			_playerController.onInput.AddListenerOnce(StartLoop);
			Inputs.controls.Game.EndLoop.SetEnabled(false);
		}

		private void StartLoop() {
			TimeManager.StartLoop();
			_playerController.onInput.RemoveListener(StartLoop);
			Inputs.controls.Game.EndLoop.SetEnabled(enabled);
		}

		private void HandleExitReached() => SceneManager.LoadScene(_nextScene);

		private void EndLoop(InputAction.CallbackContext obj) => EndLoop();

		private void EndLoop() {
			var newGhost = Instantiate(_ghostPrefab);
			newGhost.Initialise(currentRecord);
			ghosts.Add(newGhost);
			Reset();
		}
	}
}