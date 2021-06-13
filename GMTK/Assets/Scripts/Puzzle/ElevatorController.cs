using UnityEngine;
using UnityEngine.Experimental.AI;

public class ElevatorController : MonoBehaviour {
	public enum ElevatorState { INACTIVE, ASCENDING, DESCENDING };
	public ElevatorState state = ElevatorState.INACTIVE;

	[SerializeField] float maxHeight;
	[SerializeField] float minHeight;

	float lerpTimer = 0f;

	[System.Serializable] struct SwitchStruct { public GameObject lever; public bool shouldBeOn; }
	[SerializeField] SwitchStruct[] switches;

	float checkInterval = 0.25f;
	float checkTimer = 0f;

	bool ascensionComplete = false, descensionComplete = false;

	void Start() {
		state = ElevatorState.INACTIVE;
	}

	void Update() {
		checkTimer += Time.deltaTime;

		if (checkTimer > checkInterval) {
			checkTimer = 0f;

			for (int i=0; i<switches.Length; i++) {
				if (AllOn()) {
					if (!ascensionComplete) state = ElevatorState.ASCENDING;
					descensionComplete = false;
				} else if (AllOff()) {
					if (!descensionComplete) state = ElevatorState.DESCENDING;
					ascensionComplete = false;
				} else {
					state = ElevatorState.INACTIVE;
					descensionComplete = false;
					ascensionComplete = false;
				}
			}
		}

		if (state != ElevatorState.INACTIVE) {
			if (state == ElevatorState.ASCENDING) {
				transform.position = new Vector3(transform.position.x, Mathf.Lerp(minHeight, maxHeight, lerpTimer), transform.position.z);
			} else if (state == ElevatorState.DESCENDING) {
				transform.position = new Vector3(transform.position.x, Mathf.Lerp(maxHeight, minHeight, lerpTimer), transform.position.z);
			}

			lerpTimer += 0.4f * Time.deltaTime;

			float targetHeight = state == ElevatorState.ASCENDING ? maxHeight : minHeight;
			if (Mathf.Abs(targetHeight - transform.position.y) < 0.01f) {
				float newY = state == ElevatorState.ASCENDING ? maxHeight : minHeight;

				if (state == ElevatorState.ASCENDING) {
					ascensionComplete = true;
				} else {
					descensionComplete = true;
				}

				transform.position = new Vector3(transform.position.x, newY, transform.position.z);

				state = ElevatorState.INACTIVE;
				lerpTimer = 0f;
			}

			lerpTimer = Mathf.Clamp(lerpTimer, 0f, 1f);
		}
	}

	bool AllOn() {
		for (int i = 0; i < switches.Length; i++) {
			if (switches[i].lever.GetComponent<Switch>().On != switches[i].shouldBeOn) {
				return false;
			}
		} return true;
	}

	bool AllOff() {
		for (int i = 0; i < switches.Length; i++) {
			if (switches[i].lever.GetComponent<Switch>().On == switches[i].shouldBeOn) {
				return false;
			}
		} return true;
	}
}
