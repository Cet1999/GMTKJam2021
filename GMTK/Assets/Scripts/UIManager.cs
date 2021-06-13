using UnityEngine;

public class UIManager : MonoBehaviour {
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    static GameObject inGameUI;

    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;

            inGameUI = GameObject.Find("InGameUI");
        }
    }

}