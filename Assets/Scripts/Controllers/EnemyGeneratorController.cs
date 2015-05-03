using UnityEngine;
using System.Collections;

public class EnemyGeneratorController : MonoBehaviour {

	GameObject stupidEnemy ;
	GenerateWaveLogic generateWaveLogic;
	public float timeBetweenWaves = 9f;
	private float fixedTimeStart = -10;
	private int _waveNumber = 0;
    bool b;
	// Use this for initialization

	void Start () {
		generateWaveLogic = GameObject.Find("Logic").GetComponent<GenerateWaveLogic>();
		//_leftBodyPart = Instantiate (_leftBodyPartResouce, i_location, Quaternion.identity) as GameObject;
        GenerateWave();
        b = false;
	}

	void FixedUpdate(){
		if (b) {
            generateWaveLogic.waveEnded = false;
			GenerateWave();
            b = false;
		}
	}

	void GenerateWave(){
		generateWaveLogic.generateWave (new WaveGenerateModel{waveNumber = _waveNumber});
        //_waveNumber++;
	}
}
