using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
public class GenerateWaveLogic : MonoBehaviour {
	
	WaveLogicFactory _wave;
	EnemyGeneratorLogic enemyGenerator;
	private int waveNumber;
	public float numberOfSecBetweenEnemies = 0.5f;
    public Vector3[][] paths;
    public Vector2[] instantiateLocations;
    public bool waveEnded = false;

	void Start () {
		_wave = new WaveLogicFactory ();
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

        i_model.wave = _wave.createWaveByOrder(i_model);
		StartCoroutine(GenerateEnemiesWithPause(i_model));
		waveNumber++;
	}

    IEnumerator GenerateEnemiesWithPause(WaveGenerateModel i_waveModel)
    {
        EnemyType currentType;
        EnemyLocation currentLocation;
        while (true)
        {
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
            currentEnemyLogic.StartOrderPath(5 * 10, currentLocation);
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
