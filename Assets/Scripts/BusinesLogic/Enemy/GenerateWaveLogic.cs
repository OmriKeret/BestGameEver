using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
public class GenerateWaveLogic : MonoBehaviour {

	GameObject stupidEnemy;
	public float numberOfSecBetweenEnemies = 0.5f;

    public Vector3[][] paths;
    public Vector2[] instantiateLocations;
	public Dictionary <int,int> numberOfEnemysEachWave;
	public Dictionary <int,int[]> TypeOfEnemiesEachWave;

	void Start () {
		stupidEnemy = Resources.Load ("stupidEnemy") as GameObject;
        buildDictionaries();
	}

	// Use this for initialization
	public void generateWave(WaveGenerateModel model) {

        if (!TypeOfEnemiesEachWave.ContainsKey(model.waveNumber))
        {
            return;
        }
        StartCoroutine(generateEnemiesWithPause(model));
	}

     IEnumerator generateEnemiesWithPause(WaveGenerateModel model)
    {
		int[] waveTypeArr = TypeOfEnemiesEachWave[model.waveNumber];
		var typeMax = waveTypeArr.Length;
        var pathsMax = paths.Length;
		var waveLength = numberOfEnemysEachWave [model.waveNumber];
		for (int i = 0; i < waveLength; i++)
        {
            //genereate enemies randomly 
            var enemyType = waveTypeArr[UnityEngine.Random.Range(0, typeMax)];
            var enemyLocation = new Vector2(0f, 40f); ;//instantiateLocations[UnityEngine.Random.Range(0, locationMax)];

            //instantiate enemeies in random location
            var path = paths[UnityEngine.Random.Range(0, pathsMax)];
            var enemy1 = Instantiate(stupidEnemy, enemyLocation, Quaternion.identity) as GameObject;
            var enemyLogic = enemy1.GetComponent<IEnemy>();
            enemyLogic.setPath(path, model.waveNumber * 10);
            
            //enemyLogic.setPath("EnemyPath" + UnityEngine.Random.Range(1, 3), model.waveNumber * 10);
			yield return new WaitForSeconds(numberOfSecBetweenEnemies);    //Wait 
        }
    } 


	public void buildDictionaries()
	{
		numberOfEnemysEachWave = new Dictionary <int,int>
        { 
			{ 1 , 3 },
			{ 2 , 5 },
			{ 3 , 7 },
			{ 4 , 9 },
			{ 5 , 14 },
			{ 6 , 7 }
		};

		TypeOfEnemiesEachWave = new Dictionary<int,int[]>  	
		{ 
			{ 1 , new int[]{1} },
			{ 2 , new int[]{1} },
			{ 3 , new int[]{1} },
			{ 4 , new int[]{1} },
			{ 5 , new int[]{1} },
			{ 6 , new int[]{1} }
		};

        paths = new Vector3[][]
        {
           new Vector3[] {
               new Vector3(23.16821f,32.77761f,0f),
               new Vector3(16.53627f,23.94602f,0f),
               new Vector3(15.8f,-5.75f,0f),
               new Vector3(-30f,-10.11f,0f)
               //new Vector3(-19.64144f,-4.123909f,0f),
               //new Vector3(-7.596886f,-1.11f,0f),
               //new Vector3(-20.596886f,-1.11f,0f),
               //new Vector3(-34.6086f,0f,0f)
            },
            new Vector3[] {
               new Vector3(-10.59303f,32.91187f,0f),
               new Vector3(-8.758844f,14.06718f,0f),
               new Vector3(-11.58f,4.354736f,0f),
               new Vector3(40f,10.32509f,0f)
               //new Vector3(-8.758844f,14.06718f,0f),
               //new Vector3(-11.58f,4.354736f,0f),
               //new Vector3(14.8077f,10.32509f,0f),
               //new Vector3(-16.24f,10.62772f,0f)
            }
        };
      //  instantiateLocations = new Vector2[3] {  };
	}
}
