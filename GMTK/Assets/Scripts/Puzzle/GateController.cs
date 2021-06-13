using UnityEngine;

public class GateController : MonoBehaviour {
	[SerializeField] float maxHeight;
	[SerializeField] float minHeight;

	[System.Serializable] struct SwitchStruct { public GameObject lever; public bool shouldBeOn; }
	[SerializeField] SwitchStruct[] switches;

	float checkInterval = 1f;
	float checkTimer = 0f;

	public enum GateState { INACTIVE, OPENING, CLOSING };
	public GateState state = GateState.INACTIVE;

	float lerpTimer = 0f;

	void Awake() {
		transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
	}

	void Update() {
		checkTimer += Time.deltaTime;

		if (checkTimer > checkInterval) {
			checkTimer = 0f;

			bool shouldOpen = true;

			for (int i=0; i<switches.Length; i++) {
				if (switches[i].lever.GetComponent<Switch>().On != switches[i].shouldBeOn) {
					shouldOpen = false;
				}
			}

			if (shouldOpen) {
				if (transform.position.y != maxHeight) state = GateState.OPENING;
			} else {
				if (transform.position.y != minHeight) state = GateState.CLOSING;
			}
		}

		if (state == GateState.OPENING) {
			transform.position = new Vector3(transform.position.x, Mathf.Lerp(minHeight, maxHeight, lerpTimer), transform.position.z);

			if (Mathf.Abs(maxHeight - transform.position.y) < 0.01f) {
				transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);

				state = GateState.INACTIVE;
			} else { lerpTimer += 0.4f * Time.deltaTime; }
		} else if (state == GateState.CLOSING) {
			transform.position = new Vector3(transform.position.x, Mathf.Lerp(minHeight, maxHeight, lerpTimer), transform.position.z);

			if (Mathf.Abs(minHeight - transform.position.y) < 0.01f) {
				transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);

				state = GateState.INACTIVE;
			} else { lerpTimer -= 0.4f * Time.deltaTime; }
		}

		Mathf.Clamp(lerpTimer, 0f, 1f);
	}
}
