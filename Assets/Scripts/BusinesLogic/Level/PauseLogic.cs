using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PauseLogic : MonoBehaviour {
    public float timeToMenuToOpenAndClose = 1f;
    GameObject PauseMenu;
    public Vector3 menuOrigPos;
    public Vector3 menuEndPos;
    bool isMenuOpen = false;
    Sprite imageOpen;
    Sprite imageClose;
    Image buttonImage;
    AudioSource audioSource;
    AudioClip pauseSoundIn;
    AudioClip pauseSoundOut;
    TouchInterpeter touch;
    bool touchDisabled;

    Animator darkScreen;
	// Use this for initialization
	void Start () {
        PauseMenu = GameObject.Find("PauseMenu");
        audioSource = GameObject.Find("Camera").GetComponent<AudioSource>();
        menuOrigPos = PauseMenu.transform.position; ;//new Vector3(0, 30, 0);
        menuEndPos = new Vector3(menuOrigPos.x, 35, menuOrigPos.z);
        imageClose = Resources.Load<Sprite>("pause1");
        imageOpen = Resources.Load<Sprite>("resume1");
        buttonImage = GameObject.Find("Pause").GetComponent<Image>();
        pauseSoundIn = Sound.sound.getPauseBtnIn();
        pauseSoundOut = Sound.sound.getPauseBtnOut();
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
        darkScreen = GameObject.Find("Canvas/DarkScreen").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onPause(){

        if (isMenuOpen)
        {
            if (!touchDisabled)
            {
                touch.UnsetDisableMovment();
            }
            closeMenu();
        }
        else
        {
            touchDisabled = touch.isMovmentDisabled;
            touch.SetDisableMovment();
            openMenu();
        }
    }

    private void openMenu()
    {
        Time.timeScale = 0;
        darkScreen.SetBool("darkScreen",true);
        LeanTween.move(PauseMenu, menuEndPos, timeToMenuToOpenAndClose).setIgnoreTimeScale(true);
        isMenuOpen = true;
        buttonImage.sprite = imageOpen;
        audioSource.PlayOneShot(pauseSoundIn);
    }

    private void closeMenu()
    {
        Time.timeScale = 1;
         darkScreen.SetBool("darkScreen",false);
        LeanTween.move(PauseMenu, menuOrigPos, timeToMenuToOpenAndClose).setIgnoreTimeScale(true);
        isMenuOpen = false;
        buttonImage.sprite = imageClose;
        audioSource.PlayOneShot(pauseSoundOut);
        
    }

}
