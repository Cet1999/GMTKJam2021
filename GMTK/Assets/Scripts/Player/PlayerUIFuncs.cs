using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIFuncs : MonoBehaviour {
    [SerializeField] GameObject inGameUI;

    public void onMenu(InputAction.CallbackContext value) {
        if (gameObject.name == "SmallPlayer") return;

        if (value.started) {
            inGameUI.SetActive(true);
            inGameUI.GetComponent<InGameUI>().PauseGame();
		}
    } 

    public void onExitMenu(InputAction.CallbackContext value) {
        if (gameObject.name == "SmallPlayer") return;

        if (value.started) {
            inGameUI.GetComponent<InGameUI>().ResumeGame();
		}
    } 


}
