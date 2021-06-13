using UnityEngine;
using UnityEngine.InputSystem;

public class InGameUI : MonoBehaviour {
	[SerializeField] PlayerInput bigPlayerInput;
	[SerializeField] PlayerInput smallPlayerInput;

	public void ResumeGame() {
		bigPlayerInput.SwitchCurrentActionMap("WASD");
		smallPlayerInput.SwitchCurrentActionMap("ArrowKeys");

		//Time.timeScale = 1f;

		gameObject.SetActive(false);
	}

	public void PauseGame() { 
		bigPlayerInput.SwitchCurrentActionMap("InMenu");
		smallPlayerInput.SwitchCurrentActionMap("InMenu");

		//Time.timeScale = 0f;
	}

	public void ExitGame() => Application.Quit();
}
