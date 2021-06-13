using UnityEngine;
using UnityEngine.InputSystem;

public class InGameUI : MonoBehaviour {
	[SerializeField] PlayerInput bigPlayerInput;
	[SerializeField] PlayerInput smallPlayerInput;

	public void ResumeGame() {
		bigPlayerInput.SwitchCurrentActionMap("WASD");
		smallPlayerInput.SwitchCurrentActionMap("ArrowKeys");

		gameObject.SetActive(false);
	}

	public void PauseGame() { 
		bigPlayerInput.SwitchCurrentActionMap("InMenu");
		smallPlayerInput.SwitchCurrentActionMap("InMenu");
	}

	public void ExitGame() => Application.Quit();
}
