using UnityEngine;
using UnityEngine.Experimental.AI;

public class ElevatorController : MonoBehaviour {
	public enum ElevatorState { INACTIVE, ASCENDING, DESCENDING };
	public ElevatorState state = ElevatorState.INACTIVE;

	[SerializeField] float maxHeight;
	[SerializeField] float minHeight;

	float lerpTimer = 0f;

	void Update() {
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
				transform.position = new Vector3(transform.position.x, newY, transform.position.z);

				state = ElevatorState.INACTIVE;
				lerpTimer = 0f;
			}
		}
	}
}
