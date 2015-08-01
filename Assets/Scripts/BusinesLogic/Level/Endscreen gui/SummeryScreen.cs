using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SummeryScreen : MonoBehaviour, PhaseEventHandler {

	Button missionCompleteBtn;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void handleEvent()
    {
        //Brings up next button
    }

    public void next()
    {
        throw new System.NotImplementedException();
    }

    public void setNext(PhaseEventHandler next)
    {
        throw new System.NotImplementedException();
    }
}
