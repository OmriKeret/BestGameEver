using UnityEngine;
using System.Collections;

public class finishedAllMissionAnimation : MonoBehaviour {
    DeathLogic deathLogic;
	// Use this for initialization
	void Start () {
        deathLogic = GameObject.Find("Logic").GetComponent<DeathLogic>();
	}
	
    public void setFinishedAnimation() {
    
    }
}
