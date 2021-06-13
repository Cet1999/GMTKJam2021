using UnityEngine;

public class SoundManager : MonoBehaviour {
	[SerializeField] AudioSource[] CollectibleSounds;
	int collectibleSoundsUnlocked = 0;

	public float volume = 0.225f;

	void Awake() {
		for (int i=0; i<CollectibleSounds.Length; i++) {
			CollectibleSounds[i].volume = 0f;
		}
	}

	public void UnlockCollectibleSound() {
		if (collectibleSoundsUnlocked < CollectibleSounds.Length) {
			CollectibleSounds[collectibleSoundsUnlocked++].volume = volume;
		}
	}
}
