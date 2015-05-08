using UnityEngine;
using System.Collections;

public class StaminaAnimationHelper : MonoBehaviour {

    StaminaBarLogic staminaBar;
	// Use this for initialization
	void Start () {
        staminaBar = GameObject.Find("Logic").GetComponent<StaminaBarLogic>();
	}

    public void finishedAnimation()
    {
        staminaBar.setShouldChange();
    }
}
