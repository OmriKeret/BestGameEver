using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
public class GenerateWaveLogic : MonoBehaviour {

	GameObject stupidEnemy;
	public float numberOfSecBetweenEnemies = 0.5f;
	public Vector2 enemy1Loc;
	public Vector2 enemy2Loc;
	public Vector2 enemy3Loc;
    public Vector2[] instantiateLocations;
	public Dictionary <int,int> numberOfEnemysEachWave;
	public Dictionary <int,int[]> TypeOfEnemiesEachWave;
	void Start () {
		 enemy1Loc = new Vector2(-10.5f, 14.9f);
		 enemy2Loc = new Vector2(10.5f, 14.9f);
		 enemy3Loc = new Vector2(0f, 14.9f);
		stupidEnemy = Resources.Load ("stupidEnemy") as GameObject;
        buildDictionaries();
	}
	// Use this for initialization
	public void generateWave(WaveGenerateModel model) {

        if(!TypeOfEnemiesEachWave.ContainsKey(model.waveNumber)) {
            return;
        }
		 StartCoroutine(generateEnemiesWithPause(model));
	}

     IEnumerator generateEnemiesWithPause(WaveGenerateModel model)
    {
		int[] waveTypeArr = TypeOfEnemiesEachWave[model.waveNumber];
		var typeMax = waveTypeArr.Length;
        var locationMax = instantiateLocations.Length;
		var waveLength = numberOfEnemysEachWave [model.waveNumber];
		for (int i = 0; i < waveLength; i++)
        {
            //genereate enemies randomly 
            var enemyType = waveTypeArr[UnityEngine.Random.Range(0, typeMax)];
            var enemyLocation = instantiateLocations[UnityEngine.Random.Range(0, locationMax)];

            //instantiate enemeies in random location
            var enemy1 = Instantiate(stupidEnemy, enemyLocation, Quaternion.identity) as GameObject;

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
        instantiateLocations = new Vector2[3] { enemy1Loc, enemy2Loc, enemy3Loc };
	}
}
