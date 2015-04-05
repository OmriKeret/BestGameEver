using UnityEngine;
using System.Collections;

public class EnemyGeneratorController : MonoBehaviour {

	GameObject stupidEnemy ;
	GenerateWaveLogic generateWaveLogic;
	public float timeBetweenWaves = 5f;
	private float fixedTimeStart;
	// Use this for initialization
	void Start () {
		generateWaveLogic = GameObject.Find("Logic").GetComponent<GenerateWaveLogic>();
		//_leftBodyPart = Instantiate (_leftBodyPartResouce, i_location, Quaternion.identity) as GameObject;
	}
	void FixedUpdate(){
		if (Time.fixedTime - fixedTimeStart > timeBetweenWaves) {
			GenerateWave();
			fixedTimeStart = Time.fixedTime;
		}
	}
	void GenerateWave(){
		generateWaveLogic.generateWave ();
	}
}
