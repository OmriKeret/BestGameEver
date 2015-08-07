using UnityEngine;
using System.Collections;

public class EnemyGeneratorController : MonoBehaviour {

	public GameObject stupidEnemy ;
	private GenerateWaveLogic generateWaveLogic;
	private int _waveNumber = 0;
    private int counter = 0;
    public int TimeBetweenWaves = 1;
    bool startCounter = false;
	// Use this for initialization

    public void testMethon()
    {
        Debug.Log("Delegete works");
    }

	void Start () {
        GameObject.FindObjectOfType<EventListener>().Listener[EventTypes.WaveOver] += GenerateWaveWithDelay;
        //Debug only: start from wave x
        _waveNumber = 0;
        //End debug
        generateWaveLogic = GameObject.Find("Logic").GetComponent<GenerateWaveLogic>();
        GenerateWave();
        counter = 0;
        startCounter = false;
	}

	void FixedUpdate()
    {
	}

	void GenerateWave(){
        Debug.Log(string.Format("Delegate was called with {0}", _waveNumber));
        WaveGenerateModel wm = new WaveGenerateModel(_waveNumber);
        generateWaveLogic.generateWave(wm);
        _waveNumber++;
	}

    void GenerateWaveWithDelay()
    {
        StartCoroutine(Delay());
        
    }

    IEnumerator Delay()
    {
        Debug.Log(string.Format("Start Waiting {0} for {1} seconds", Time.time, TimeBetweenWaves));   
        yield return new WaitForSeconds(TimeBetweenWaves);
        Debug.Log(string.Format("Finish Waiting {0}", Time.time));
        GenerateWave();
        
    }

}
