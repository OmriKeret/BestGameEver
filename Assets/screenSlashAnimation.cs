using UnityEngine;
using System.Collections;

public class screenSlashAnimation : MonoBehaviour {

    UIManagerScript uiManager;
	// Use this for initialization
	void Start () {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
	}

    public void finishedAnimation()
    {
        uiManager.finishedCutSceneAnimation();
    }
}
