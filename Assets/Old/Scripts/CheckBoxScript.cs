using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckBoxScript : MonoBehaviour {
	GameObject vSprite,vButton;
	public Text highScore,highStreak;
	// Use this for initialization
	void Start () {
		vSprite = Resources.Load ("vButton") as GameObject;
		GeneralPhysics.isTutorialMode = false;
		click ();
		highScore.text = "High Score: "+GeneralPhysics.highScore.ToString ();
		highStreak.text = "High Streak: "+GeneralPhysics.highStreak.ToString ();
	}

	public void click(){
		if (GeneralPhysics.isTutorialMode) {
			Destroy(vButton);
			GeneralPhysics.isTutorialMode=false;
				} else {
			vButton = Instantiate(vSprite,transform.position,Quaternion.identity) as GameObject;
			GeneralPhysics.isTutorialMode = true;
				}
		}
	
	// Update is called once per frame
	void Update () {
	
	}
}
