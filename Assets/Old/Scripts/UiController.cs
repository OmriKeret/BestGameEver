using UnityEngine;
using System.Collections;

public class UiController : MonoBehaviour {

	public GameObject panel; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pauseGame() {
		GeneralPhysics.isPaused = true;
		Time.timeScale = 0f;
		panel.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane) );
	}

	public void unPauseGame() {
		GeneralPhysics.isPaused = false;
		Time.timeScale = 1f;
		panel.transform.position = new Vector3(Screen.width * 10, Screen.height * 10, Camera.main.nearClipPlane) ;
	}
}
