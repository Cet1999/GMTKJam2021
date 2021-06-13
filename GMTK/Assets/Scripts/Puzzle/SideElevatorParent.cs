using UnityEngine;

public class SideElevatorParent : MonoBehaviour {
	bool parented = false;

	[SerializeField] LayerMask elevatorMask;

	void Update() {
		RaycastHit hit;
		Ray ray = new Ray(transform.position, -Vector3.up);

		if (Physics.Raycast(ray, out hit, elevatorMask)) {
		    if (hit.collider != null) {
				if (hit.collider.CompareTag("Elevator")) {
					parented = true;

					transform.parent = hit.collider.transform;
				} else {
					if (parented) {
						parented = false;

						transform.parent = null;
					}
				}
			}
		}
	}
}
