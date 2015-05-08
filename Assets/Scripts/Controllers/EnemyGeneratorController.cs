using UnityEngine;
using System.Collections;

public class EnemyGeneratorController : MonoBehaviour {

	public GameObject stupidEnemy ;
	private GenerateWaveLogic generateWaveLogic;
	private int _waveNumber = 0;
    private int counter = 0;
    public int TimeBetweenWaves = 150;
    bool startCounter = false;
	// Use this for initialization

	void Start () {
		generateWaveLogic = GameObject.Find("Logic").GetComponent<GenerateWaveLogic>();
		//_leftBodyPart = Instantiate (_leftBodyPartResouce, i_location, Quaternion.identity) as GameObject;
        GenerateWave();
        counter = 0;
        startCounter = false;
	}

	void FixedUpdate(){
        if (generateWaveLogic.waveEnded)
        {
            generateWaveLogic.waveEnded = false;
            startCounter = true;
		}
        if (startCounter)
        {
            counter++;
            Debug.Log(counter);
        }
        if (counter == TimeBetweenWaves)
        {
            startCounter = false;
            counter = 0;
            GenerateWave();
        }
	}

	void GenerateWave(){
        WaveGenerateModel wm = new WaveGenerateModel { waveNumber = _waveNumber };
        generateWaveLogic.generateWave(wm);
        _waveNumber++;
	}
}
