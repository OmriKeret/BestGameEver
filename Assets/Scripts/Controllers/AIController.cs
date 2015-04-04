using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	private IEnemy _logic;
	private AEnemyStats _stats;
	Vector2 _creationLocation, _movementDirection;


	// Use this for initialization
	void Start () {
		//TODO: change type to something general
		_logic = gameObject.AddComponent<StupidAILogic> ();
		_stats = gameObject.AddComponent<StupidGeneralStats> ();
		_logic.setStats (_stats);
		_creationLocation = transform.position;
		_movementDirection = _logic.moveToPoint (_creationLocation);
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			_stats.lifeDown();
			if (_stats.isDead())
				death();
				}

		
	}

	void death(){
		_logic.split (transform.position);
		_logic.death ();
		}

	// Update is called once per frame
	void Update () {
		_logic.moveInDirection (_movementDirection);

	}
}
