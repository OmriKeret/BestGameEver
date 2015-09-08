using UnityEngine;
using System.Collections;

public class EnemyGeneratorController : MonoBehaviour {

	public GameObject stupidEnemy ;
	private GenerateWaveLogic generateWaveLogic;
	private int _waveNumber = 0;
    private int counter = 0;
    public int TimeBetweenWaves = 1;
    bool startCounter = false;
    EventListener listener;
	// Use this for initialization

    public void testMethon()
    {
        Debug.Log("Delegete works");
    }

	void Start () {
        listener = EventListener.instance;
        listener.Listener[EventTypes.WaveOver] += GenerateWaveWithDelay;
        listener.Listener[EventTypes.GameOver] += StopGeneration;
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
        WaveGenerateModel wm = new WaveGenerateModel(_waveNumber);
        generateWaveLogic.generateWave(wm);
        _waveNumber++;
	}

    void GenerateWaveWithDelay(params System.Object[] obj)
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {        yield return new WaitForSeconds(TimeBetweenWaves);
        GenerateWave();
    }

    void StopGeneration(params System.Object[] obj)
    {
        GameObject.FindObjectOfType<EventListener>().Listener[EventTypes.WaveOver] -= GenerateWaveWithDelay;
    }


}
