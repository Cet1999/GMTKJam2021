using UnityEngine;

public class GateController : MonoBehaviour {
	[SerializeField] Vector3 openPosition;
	[SerializeField] Vector3 closedPosition;

	[System.Serializable] struct SwitchStruct { public GameObject lever; public bool shouldBeOn; }
	[SerializeField] SwitchStruct[] switches;

	float checkInterval = 1f;
	float checkTimer = 0f;

	bool isOpen = false;

	void Update() {
		checkTimer += Time.deltaTime;

		if (checkTimer > checkInterval) {
			print("wyatt inner update");
			checkTimer = 0f;

			bool shouldOpen = true;

			for (int i=0; i<switches.Length; i++) {
				if (switches[i].lever.GetComponent<Switch>().On != switches[i].shouldBeOn) {
					shouldOpen = false;
				}
			}

			print("wyatt " + shouldOpen);

			print("wyatt " + closedPosition);

			if (shouldOpen) {
				OpenGate();
			} else {
				CloseGate();
			}
		}
	}

	void OpenGate() {
		if (isOpen) return;
		isOpen = true;

		transform.position = openPosition;
	}

	void CloseGate() {
		if (!isOpen) return;
		isOpen = false;

		transform.position = closedPosition;
	}
}
