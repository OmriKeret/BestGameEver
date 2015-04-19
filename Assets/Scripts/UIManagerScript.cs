using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {
    public AudioClip clickSound;
    public AudioClip startSound;
    public AudioSource audioSource;
    void Start() 
    {
        startSound = Sound.sound.getStartButtonSound();
        clickSound = Sound.sound.getButtonPushSound();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
	public void StartGame()
	{
        audioSource.clip = startSound;
        audioSource.PlayOneShot(startSound);
        StartCoroutine(playSoundThenLoad("Prototype"));
		//Application.LoadLevel("Prototype");
	}
	public void ToMainMenu()
	{
        audioSource.clip = clickSound;
        audioSource.PlayOneShot(clickSound);
       StartCoroutine(playSoundThenLoad("Main Menu"));
	//	Application.LoadLevel("Main Menu");
	}
	public void ToStore()
	{
        audioSource.clip = clickSound;
        audioSource.PlayOneShot(clickSound);
        StartCoroutine(playSoundThenLoad("Store"));
		//Application.LoadLevel("Store");
	}

    IEnumerator playSoundThenLoad(string levelName)
    {

        yield return new WaitForSeconds(audioSource.clip.length);
        Application.LoadLevel(levelName);
    }
	
}
