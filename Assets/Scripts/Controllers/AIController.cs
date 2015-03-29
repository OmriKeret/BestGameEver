using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	private IEnemy logic;
	private EnemyGeneralStats stats;


	// Use this for initialization
	void Start () {
		//TODO: change type to something general
		logic = gameObject.AddComponent<StupidAILogic> ();
		stats = gameObject.AddComponent<EnemyGeneralStats> ();
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			stats.lifeDown();
			if (stats.isDead())
				logic.death();
				}

		
	}

	// Update is called once per frame
	void Update () {

	}
}
