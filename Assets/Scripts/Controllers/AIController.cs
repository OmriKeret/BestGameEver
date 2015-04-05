using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	private IEnemy _logic;
	private EnemyGeneralStats _stats;
	Vector2 _creationLocation, _movementDirection;
	public CollisionFacade collisionFacade;

	// Use this for initialization
	void Start () {
		//TODO: change type to something general
		_logic = gameObject.AddComponent<StupidAILogic> ();
		_stats = gameObject.AddComponent<EnemyGeneralStats> ();
		_logic.setStats (_stats);
		_creationLocation = transform.position;
		_movementDirection = _logic.moveToPoint (_creationLocation);
		collisionFacade = new CollisionFacade ();
	
	}

	void OnCollisionEnter2D(Collision2D col) {


		//	Debug.Log("collision detected");
		CollisionModel model = new CollisionModel{ mainCollider = this.gameObject, CollidedWith = col.gameObject};
		onCollisionFacade (model);

		//if (coll.gameObject.tag == "Player") {
	//		_stats.lifeDown();
	//		if (_stats.isDead())
	//			death();
	//			}

		
	}

	public void death(StopAfterCollisionModel s){
		//_logic.split (transform.position);
		_logic.death ();
		}

	// Update is called once per frame
	void Update () {
		_logic.moveInDirection (_movementDirection);

	}

	public void onCollisionFacade(CollisionModel model) {
		collisionFacade.Collision (model);
	}
}

