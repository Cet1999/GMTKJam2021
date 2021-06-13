using UnityEngine;
using UnityEngine.InputSystem;

public class InGameUI : MonoBehaviour {
	[SerializeField] PlayerInput bigPlayerInput;
	[SerializeField] PlayerInput smallPlayerInput;

	GameObject soundManager;

	[SerializeField] Animator anim;

	void Awake() {
		soundManager = GameObject.Find("Managers");
	}

	public void ResumeGame() {
		bigPlayerInput.SwitchCurrentActionMap("WASD");
		smallPlayerInput.SwitchCurrentActionMap("ArrowKeys");

		//Time.timeScale = 1f;

		anim.SetBool("DropDown", false);

		//gameObject.SetActive(false);
	}

	public void PauseGame() { 
		bigPlayerInput.SwitchCurrentActionMap("InMenu");
		smallPlayerInput.SwitchCurrentActionMap("InMenu");

		anim.SetBool("DropDown", true);

		//Time.timeScale = 0f;
	}

	public void ToggleVolume() {
		soundManager.GetComponent<SoundManager>().volume = 1f - soundManager.GetComponent<SoundManager>().volume;
	}

	public void ExitGame() => Application.Quit();
}
