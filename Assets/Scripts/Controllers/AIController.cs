using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	private IEnemy _logic;
	private AEnemyStats _stats;
	Vector2 _creationLocation, _movementDirection;
    public EnemyType type;


    public enum EnemyType { Stupid=1}

	// Use this for initialization
	void Start () {
		//TODO: change type to something general
        
		_logic = gameObject.AddComponent<StupidAILogic> ();
		_stats = gameObject.AddComponent<StupidGeneralStats> ();
		_logic.SetStats (_stats);
		_creationLocation = transform.position;
		_movementDirection = _logic.MoveToPoint (_creationLocation);
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			_stats.lifeDown();
			if (_stats.isDead())
				death();
				}

		
	}

	void death(){
		_logic.Split (transform.position);
		_logic.Death ();
		}

	// Update is called once per frame
	void Update () {
		_logic.MoveInDirection (_movementDirection);

	}
}
