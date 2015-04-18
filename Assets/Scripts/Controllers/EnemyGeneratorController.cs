using UnityEngine;
using System.Collections;

public class EnemyGeneratorController : MonoBehaviour {

	GameObject stupidEnemy ;
	GenerateWaveLogic generateWaveLogic;
	public float timeBetweenWaves = 9f;
	private float fixedTimeStart = -10;
	private int waveNumber = 1;
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
        waveNumber++;
	}
}
