using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PauseLogic : MonoBehaviour {
    public float timeToMenuToOpenAndClose = 1f;
    GameObject PauseMenu;
    public Vector3 menuOrigPos;
    public Vector3 menuEndPos;
    bool isMenuOpen = false;
	// Use this for initialization
	void Start () {
        PauseMenu = GameObject.Find("PauseMenu");
        menuOrigPos = new Vector3(0, 30, 0);
        menuEndPos = new Vector3(0,6,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onPause(){
        if (isMenuOpen)
        {
            closeMenu();
        }
        else
        {
            openMenu();
        }
    }

    private void openMenu()
    {
        Time.timeScale = 0;
        LeanTween.move(PauseMenu, menuEndPos, timeToMenuToOpenAndClose).setIgnoreTimeScale(true);
        isMenuOpen = true;
    }

    private void closeMenu()
    {
        Time.timeScale = 1;
        LeanTween.move(PauseMenu, menuOrigPos, timeToMenuToOpenAndClose).setIgnoreTimeScale(true);
        isMenuOpen = false;
    }

}
