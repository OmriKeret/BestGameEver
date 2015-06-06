using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Threading;

public class GenerateWaveLogic : MonoBehaviour {
	
	WaveLogicFactory _waveFactory;
	EnemyGeneratorLogic enemyGenerator;
	private int waveNumber;
	public float numberOfSecBetweenEnemies = 0.5f;
    public Vector3[][] paths;
    public Vector2[] instantiateLocations;
    public bool waveEnded = false;
    private int TimeToFinishPath = 50;
    public int MaxEasyEnemies = 4;

	void Awake () {
		_waveFactory = new WaveLogicFactory ();
		enemyGenerator = new EnemyGeneratorLogic ();
		waveNumber = 1;
		waveEnded = false;
	}

    public void generateNonEmpty(WaveGenerateModel i_model)
    {
        int numberOfActiveEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        int minimalNumberOfEnemies = 1+i_model.waveNumber/10;
        if (numberOfActiveEnemies < minimalNumberOfEnemies)
        {
            generateWave(i_model);
        }
    }

    
	// Use this for initialization
	public void generateWave(WaveGenerateModel i_model) {
		/** --omri's code
        if (!TypeOfEnemiesEachWave.ContainsKey(model.waveNumber))
        {
            return;
        }
        */
        //_waveFactory.createWaveByOrder(i_model);
	    WaveType waveType = _waveFactory.ChooseRandomWaveType(i_model.waveNumber);
        i_model.wave = _waveFactory.CreateWave(waveType);
        StartCoroutine(GenerateEnemiesWithPause(i_model));
        waveNumber++;
        
	}

    IEnumerator GenerateEnemiesWithPause(WaveGenerateModel i_waveModel)
    {
        EnemyType currentType;
        EnemyLocation currentLocation;
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length >= MaxEasyEnemies)
            {
                Thread.Sleep(2);
            }
            //Init the enemy propreties
            bool lastInRow = i_waveModel.wave.InitEnemy(out currentType, out currentLocation);
            //Check if wave ended
            if (currentType.Equals(EnemyType.End))
            {
                waveEnded = true;
                Debug.Log("End wave");
                yield break;
            }
            var enemyLocation = new Vector2(0f, 40f); ;//instantiateLocations[UnityEngine.Random.Range(0, locationMax)];
            GameObject currentEnemy = Instantiate(enemyGenerator.getEnemy(currentType), enemyLocation, Quaternion.identity) as GameObject;
            IEnemy currentEnemyLogic = currentEnemy.GetComponent<IEnemy>();
            currentEnemyLogic.playSpawnSound();
            currentEnemyLogic.StartOrderPath(TimeToFinishPath, currentLocation);
            //If there are more enemies in row, don't wait.
            if (lastInRow)
            {
                yield return new WaitForSeconds(numberOfSecBetweenEnemies);
            }
            else
            {
                continue;
            }

        }
    }
}
