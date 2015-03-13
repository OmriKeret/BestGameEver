using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MissionController : MonoBehaviour {
	public Toggle mission1;
	public Toggle mission2;
	public Text MissionCompleteAnounce;
	public Animator missionComplete; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Fire1"))
			this.GetComponent<Animator> ().SetTrigger ("FinishedMission");
	}
	public void completedMission(){

		missionComplete.SetTrigger ("FinishedMission");
	}
}
