using UnityEngine;

public class SideElevatorController : MonoBehaviour {
	public enum SideElevatorState { INACTIVE, ASCENDING, DESCENDING };
	public SideElevatorState state = SideElevatorState.INACTIVE;

	[SerializeField] float maxHeight;
	[SerializeField] float minHeight;

	float lerpTimer = 0f;

	void Update() {
		if (state != SideElevatorState.INACTIVE) {
			if (state == SideElevatorState.ASCENDING) {
				transform.position = new Vector3(Mathf.Lerp(minHeight, maxHeight, lerpTimer), transform.position.y, transform.position.z);
			} else if (state == SideElevatorState.DESCENDING) {
				transform.position = new Vector3(Mathf.Lerp(maxHeight, minHeight, lerpTimer), transform.position.y, transform.position.z);
			}

			lerpTimer += 0.4f * Time.deltaTime;

			float targetHeight = state == SideElevatorState.ASCENDING ? maxHeight : minHeight;
			if (Mathf.Abs(targetHeight - transform.position.x) < 0.01f) {
				float newX = state == SideElevatorState.ASCENDING ? maxHeight : minHeight;
				transform.position = new Vector3(newX, transform.position.y, transform.position.z);

				state = SideElevatorState.INACTIVE;
				lerpTimer = 0f;
			}
		}
	}
}
