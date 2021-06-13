using UnityEngine;

public class PressurePlateController : MonoBehaviour {
	public bool isOn = false;

	[SerializeField] LayerMask playerMask;

	void Update() {
		// detect if the player is above
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity, playerMask);

		bool pressed = false;

		for (int i=0; i<hitColliders.Length; i++) {
			if (hitColliders[i].name == "BigPlayer") {
				pressed = true;
			}
		}

		if (pressed) isOn = true;
		else isOn = false;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, transform.localScale);
	}
}
