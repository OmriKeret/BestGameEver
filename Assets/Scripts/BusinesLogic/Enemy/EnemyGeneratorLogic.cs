using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGeneratorLogic : MonoBehaviour {

	GameObject stupid;
    private GameObject spike;
	public Dictionary<EnemyType,GameObject> maker;

	public EnemyGeneratorLogic(){
		stupid = Resources.Load ("stupidEnemy") as GameObject;
        spike = Resources.Load ("spikeEnemy") as GameObject;
		maker = new Dictionary<EnemyType,GameObject>  	
		{ 
			{ EnemyType.Stupid, stupid },
            {EnemyType.Spike,spike}
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
