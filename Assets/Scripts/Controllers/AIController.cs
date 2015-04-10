using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	private IEnemy _logic;
	private AEnemyStats _stats;
	Vector2 _creationLocation, _movementDirection;
	public CollisionFacade collisionFacade;
    public EnemyType type;


	// Use this for initialization
	void Start () {
		//TODO: change type to something general
		 type = EnemyType.Stupid;
		_logic = gameObject.AddComponent<StupidAILogic> ();
		_stats = gameObject.AddComponent<StupidGeneralStats> ();
		_logic.SetStats (_stats);
		_creationLocation = transform.position;
		_movementDirection = _logic.MoveToPoint (_creationLocation);
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
    public bool lifeDown(int hitStrength)
    {
        _stats.lifeDown(hitStrength);
        return _stats.isDead();
    }

	public void death(StopAfterCollisionModel s){
		//_logic.split (transform.position);
		_logic.Death ();
		}

	// Update is called once per frame
	void Update () {
		_logic.MoveInDirection (_movementDirection);

	}

	public void onCollisionFacade(CollisionModel model) {
		collisionFacade.Collision (model);
	}
}

