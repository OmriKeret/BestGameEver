﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGeneratorLogic  {

	GameObject stupid,spike,tank,cannon,hawk;
    
	public Dictionary<EnemyType,GameObject> maker;

	public EnemyGeneratorLogic(){
		stupid = Resources.Load ("stupidEnemy") as GameObject;
        spike = Resources.Load ("spikeEnemy") as GameObject;
        tank = Resources.Load("tankEnemy") as GameObject;
        cannon = Resources.Load("cannonEnemy") as GameObject;
        hawk = Resources.Load("eagleEnemy") as GameObject;
		maker = new Dictionary<EnemyType,GameObject>  	
		{ 
			{ EnemyType.Stupid, stupid },
            {EnemyType.Spike,spike},
            {EnemyType.Tank,tank},
            {EnemyType.Cannon,cannon},
            {EnemyType.Hawk,hawk},
            {EnemyType.End,new GameObject()}
		};
	}

	public GameObject getEnemy(EnemyType i_type){
		GameObject enemy;
		maker.TryGetValue (i_type, out enemy);
		if (enemy != null)
			return enemy;
		else
			throw new UnityException("Failed to create new enemy: Enemy type "+i_type+" doesn't exsist!");
	}


}
