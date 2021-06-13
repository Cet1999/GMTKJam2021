using UnityEngine;

public class Collectible : MonoBehaviour {
    bool collected = false;

    void Update() {
        if (!collected) {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }
    }

    public void Collect() => collected = true;
    public void Drop() => collected = false;

    public bool IsCollected() => collected;
}
