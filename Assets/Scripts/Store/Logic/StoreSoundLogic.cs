using UnityEngine;
using System.Collections;

public class StoreSoundLogic : MonoBehaviour {

    AudioSource audioSource;
    AudioClip currentClip;

	// Use this for initialization
	void Start () {
        audioSource = this.GetComponent<AudioSource>();
	}

    public void buyRealCurrency()
    {
        AudioClip clip = Sound.sound.getFallingCoinsSound();
        currentClip = clip;
        audioSource.PlayOneShot(currentClip);
    }
	
    
}
