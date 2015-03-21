using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

	public Text Force;
	// Use this for initialization
	void Start () {
	
	}

	public void ForceUp(){
		GeneralPhysics.DASH_FORCE += 50;
		}
	public void ForceDown(){
		GeneralPhysics.DASH_FORCE -= 50;
	}

	public void TimeUp(){
		GeneralPhysics.DASH_TIME ++;
	}
	public void TimeDown(){
		GeneralPhysics.DASH_TIME --;
	}

	public void back(){
		Application.LoadLevel (0);
		}
	// Update is called once per frame
	void Update () {
		Force.text = "Force: "+GeneralPhysics.DASH_FORCE;
		GetComponent<Text>().text = "Time: "+GeneralPhysics.DASH_TIME;
	}
}
