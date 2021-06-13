using UnityEngine;

public class Collectible : MonoBehaviour {
    bool collected = false;

    GameObject soundManager;

    private void Awake() {
        soundManager = GameObject.Find("GameManager");
    }

    void Update() {
        if (!collected) {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }
    }

    public void Collect() {
        collected = true;

        soundManager.GetComponent<SoundManager>().UnlockCollectibleSound();
    }

    public void Drop() => collected = false;

    public bool IsCollected() => collected;
}
