using UnityEngine;
using System.Collections;

public class EnemySoundLogic : MonoBehaviour {

    public EnemyType type;
    AudioClip currentlyPlaying;
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = this.GetComponent<AudioSource>();
	}

    public void playSpawnSound()
    {
        currentlyPlaying = Sound.sound.EnemyGetSpawnSound(type);
        audioSource.PlayOneShot(currentlyPlaying);
    }
    public void playDeathSound()
    {
        currentlyPlaying = Sound.sound.EnemyGetDeathSound(type);
        audioSource.PlayOneShot(currentlyPlaying);
    }
}
