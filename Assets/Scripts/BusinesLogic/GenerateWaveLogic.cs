using UnityEngine;
using System.Collections;

public class GenerateWaveLogic : MonoBehaviour {

	GameObject stupidEnemy ;
	public Vector2 enemy1Loc  ;
	public Vector2 enemy2Loc;
	public Vector2 enemy3Loc;

	void Start () {
		stupidEnemy = Resources.Load ("stupidEnemy") as GameObject;
		//_leftBodyPart = Instantiate (_leftBodyPartResouce, i_location, Quaternion.identity) as GameObject;
	}
	// Use this for initialization
	public void generateWave() {
		var instatiateLocation = new Vector2 (-10.5f, 14.9f);
		var enemy1 = Instantiate (stupidEnemy, instatiateLocation, Quaternion.identity) as GameObject;
	}
}
