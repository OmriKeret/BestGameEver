using UnityEngine;
using System.Collections;

public class SoundLogic : MonoBehaviour {

    AudioClip currentlyPlaying;
    AudioSource audioSource1;
    AudioSource audioSource2;
    AudioSource audioSource3;
    public float sliceDelay = 0.1f;
    public float fallDelay = 0.3f;
	// Use this for initialization
	void Start () {
        audioSource1 = GameObject.Find("SoundSourcePlayer1").GetComponent<AudioSource>();
        audioSource2 = GameObject.Find("SoundSourcePlayer2").GetComponent<AudioSource>();
        audioSource3 = GameObject.Find("Camera").GetComponent<AudioSource>();
	}
    public void playFallSound() 
    {   
        currentlyPlaying = Sound.sound.playerGetFallSound();
        audioSource2.clip = currentlyPlaying;
        audioSource2.PlayDelayed(fallDelay);
    }
    public void playSliceSound()
    {
  //      Debug.Log("play slicingSound");
        currentlyPlaying = Sound.sound.playerGetRandomSliceSound();
        audioSource1.Stop();
        audioSource2.Stop();
        audioSource2.PlayOneShot(currentlyPlaying);
    }
    public void playSpinningeSound()
    {
        currentlyPlaying = Sound.sound.playerGetRandomSpinSound();
        audioSource1.Stop();
        audioSource1.clip = currentlyPlaying;
        audioSource1.PlayDelayed(sliceDelay);
        //audioSource2.Stop();
    }
    public void playJumpSound()
    {
        currentlyPlaying = Sound.sound.playerGetRandomJumpSound();
        audioSource1.Stop();
        audioSource2.Stop();
        audioSource1.PlayOneShot(currentlyPlaying);
    }
    public void playLandingSound()
    {
        currentlyPlaying = Sound.sound.playerGetLandingSound();
        audioSource1.Stop();
        audioSource2.Stop();
        audioSource1.PlayOneShot(currentlyPlaying);
    }
    public void playHittedSound()
    {
    //    Debug.Log("play slicingSound");
        currentlyPlaying = Sound.sound.playerGetHittedSound();
        audioSource1.Stop();
        audioSource2.Stop();
        audioSource2.PlayOneShot(currentlyPlaying);
    }
    public void playDeathSound()
    {
//        Debug.Log("play slicingSound");
        currentlyPlaying = Sound.sound.playerGetDeathSound();
        audioSource1.Stop();
        audioSource2.Stop();
        audioSource2.PlayOneShot(currentlyPlaying);
    }

    public void playChangeMoneySound()
    {
        if(currentlyPlaying != Sound.sound.scoreGetScoreToCashSound()) {
            currentlyPlaying = Sound.sound.scoreGetScoreToCashSound();
            Debug.Log("playing change sound");
            audioSource3.clip = currentlyPlaying;
            audioSource3.loop = true;
            audioSource3.Play();
        }
        
    }



    internal void playchangeMoneyEndSound()
    {
        if (currentlyPlaying != Sound.sound.scoreGetScoreToCashEndSound())
        {
            currentlyPlaying = Sound.sound.scoreGetScoreToCashEndSound();
            audioSource3.clip = currentlyPlaying;
            audioSource3.loop = false;
            audioSource3.Play();
        }
    }
}
