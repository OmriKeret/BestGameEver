using UnityEngine;
using System.Collections;

public class EnemyGeneratorController : MonoBehaviour {

	GameObject stupidEnemy ;
	GenerateWaveLogic generateWaveLogic;
	public float timeBetweenWaves = 9f;
	private float fixedTimeStart = -10;
	private int _waveNumber = 0;
	// Use this for initialization

	void Start () {
		generateWaveLogic = GameObject.Find("Logic").GetComponent<GenerateWaveLogic>();
		//_leftBodyPart = Instantiate (_leftBodyPartResouce, i_location, Quaternion.identity) as GameObject;
        //GenerateWave();   
        Debug.Log("Begin the debug");
	}

	void FixedUpdate(){
        if (generateWaveLogic.waveEnded)
        {
            Debug.Log("generate from controller");
            generateWaveLogic.waveEnded = false;
            GenerateWave();
		}
	}

	void GenerateWave(){
        WaveGenerateModel wm = new WaveGenerateModel { waveNumber = _waveNumber };
        generateWaveLogic.generateWave(wm);
        _waveNumber++;
	}
}
