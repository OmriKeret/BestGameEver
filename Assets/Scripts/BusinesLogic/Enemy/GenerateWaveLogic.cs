using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
public class GenerateWaveLogic : MonoBehaviour {
	
	WaveLogicFactory _wave;
	EnemyGeneratorLogic enemyGenerator;
	private int waveNumber;
	public float numberOfSecBetweenEnemies = 0.5f;
	public Vector2 enemy1Loc;
	public Vector2 enemy2Loc;
	public Vector2 enemy3Loc;
    public Vector3[][] paths;
    public Vector2[] instantiateLocations;
    public bool waveEnded = true;

	void Start () {
		 enemy1Loc = new Vector2(-10.5f, 14.9f);
		 enemy2Loc = new Vector2(10.5f, 14.9f);
		 enemy3Loc = new Vector2(0f, 14.9f);
		_wave = new WaveLogicFactory ();
		enemyGenerator = new EnemyGeneratorLogic ();
		waveNumber = 1;
        waveEnded = true;
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
            currentEnemyLogic.StartOrderPath(5 * 10, i_waveModel.waveNumber);
            //If there are more enemies in row, don't wait.
            if (lastInRow)
            {
                yield return new WaitForSeconds(numberOfSecBetweenEnemies);
            }
            else
            {
                continue;
            }


            ////genereate enemies randomly 
            //var enemyType = i_waveModel.wave.GetNextEnemy();
            //if (enemyType.Equals(EnemyType.End))
            //{
            //    waveEnded = true;
            //    Debug.Log("End wave");
            //    yield break;
            //}
            
            //var enemyLocation = new Vector2(0f, 40f); ;//instantiateLocations[UnityEngine.Random.Range(0, locationMax)];

            ////instantiate enemeies in random location
            //var enemy1 = Instantiate(enemyGenerator.getEnemy(enemyType), enemyLocation, Quaternion.identity) as GameObject;
            //var enemyLogic = enemy1.GetComponent<IEnemy>();
            //enemyLogic.playSpawnSound();
            
            //enemyLogic.StartRandomPath(5 * 10);
            
            ////enemyLogic.StartRandomPath("EnemyPath" + UnityEngine.Random.Range(1, 3), model.waveNumber * 10);
            //yield return new WaitForSeconds(numberOfSecBetweenEnemies);    //Wait 
        }
    }
}
