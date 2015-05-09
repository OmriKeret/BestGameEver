using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {
    public AudioClip clickSound;
    public AudioClip startSound;
    public AudioSource audioSource;
    public Animator screenSlash;
    public bool isAnimating;
    void Start() 
    {
        var screenSlh = GameObject.Find("ScreenSlash");
        if(screenSlh != null ) {
             screenSlash = screenSlh.GetComponent<Animator>();
        }
        startSound = Sound.sound.getStartButtonSound();
        clickSound = Sound.sound.getButtonPushSound();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

	public void StartGame()
	{
		Time.timeScale = 1;
        AudioClip sound = startSound;
        if (Application.loadedLevelName.Equals("Prototype"))
        {
            sound = clickSound;
        }
        audioSource.clip = sound;
        audioSource.PlayOneShot(sound); 
        StartCoroutine(playSoundThenLoad("Prototype"));
		//Application.LoadLevel("Prototype");
	}

   
	public void ToMainMenu()
	{
		Time.timeScale = 1;
       // audioSource.clip = clickSound;
        AudioClip sound = startSound;
        if (Application.loadedLevelName.Equals("Prototype"))
        {
            sound = clickSound;
        }
        audioSource.clip = sound;
        audioSource.PlayOneShot(sound);    
       StartCoroutine(playSoundThenLoad("Main Menu"));
	}

	public void ToStore()
	{
		Time.timeScale = 1;
        AudioClip sound = startSound;
        if (Application.loadedLevelName.Equals("Prototype"))
        {
            sound = clickSound;
        }
        audioSource.clip = sound;
        audioSource.PlayOneShot(sound);  
        StartCoroutine(playSoundThenLoad("Store"));
		//Application.LoadLevel("Store");
	}

    IEnumerator playSoundThenLoad(string levelName)
    {
        if (!Application.loadedLevelName.Equals("Prototype"))
        {
            screenSlash.SetTrigger("change");
            isAnimating = true;
        }
        
        yield return new WaitForSeconds(audioSource.clip.length);
        while (isAnimating)
        {
            yield return new WaitForSeconds(0.1f);
        }
       
        AutoFade.LoadLevel(levelName, 0, 1, Color.white);
       // Application.LoadLevel(levelName);
    }

    internal void finishedCutSceneAnimation()
    {
        isAnimating = false;
    }
}
