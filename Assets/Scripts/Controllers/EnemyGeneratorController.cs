using UnityEngine;
using System.Collections;

public class EnemyGeneratorController : MonoBehaviour {

	public GameObject stupidEnemy ;
	private GenerateWaveLogic generateWaveLogic;
	private int _waveNumber = 0;
    private int counter = 0;
    public int TimeBetweenWaves = 30;
    bool startCounter = false;
	// Use this for initialization

    public void testMethon()
    {
        Debug.Log("Delegete works");
    }

	void Start () {
        GameObject.FindObjectOfType<EventListener>().Listener[EventTypes.WaveOver] += GenerateWave;
        //Debug only: start from wave x
        _waveNumber = 0;
        //End debug
        generateWaveLogic = GameObject.Find("Logic").GetComponent<GenerateWaveLogic>();
        GenerateWave();
        counter = 0;
        startCounter = false;
	}

	void FixedUpdate(){
        //if (generateWaveLogic.waveEnded)
        //{
        //    generateWaveLogic.waveEnded = false;
        //    startCounter = true;
        //}
        //if (startCounter)
        //{
        //    counter++;
        //}
        //if (counter == TimeBetweenWaves)
        //{
        //    startCounter = false;
        //    counter = 0;
        //    GenerateWave();
        //}
	}

	void GenerateWave(){
        Debug.Log(string.Format("Delegate was called with {0}", _waveNumber));
        WaveGenerateModel wm = new WaveGenerateModel(_waveNumber);
        generateWaveLogic.generateWave(wm);
        _waveNumber++;
	}
}
