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
         _logic = this.gameObject.GetComponent<StupidAILogic>();
         _stats = this.gameObject.GetComponent<StupidGeneralStats>();
		//_logic.SetStats (_stats);
		_creationLocation = transform.position;
	//	_movementDirection = _logic.MoveToPoint (_creationLocation);
		collisionFacade = new CollisionFacade ();
	
	}

	public void OnCollisionEnter2D(Collision2D col) {
		CollisionModel model = new CollisionModel{ mainCollider = this.gameObject, CollidedWith = col.gameObject};
		onCollisionFacade (model);
	}
    public bool lifeDown(int hitStrength)
    {
        
        _stats.lifeDown(hitStrength);
    //    Debug.Log("hitting enemy with strength: " + hitStrength + "\n enemy has health of: " + _stats.life + " is dead: " + _stats.isDead());
        return _stats.isDead();
    }

	public void death(StopAfterCollisionModel s){
		//_logic.split (transform.position);
		_logic.Death ();
		}

	// Update is called once per frame
	void Update () {
		//_logic.MoveInDirection (_movementDirection);

	}

	public void onCollisionFacade(CollisionModel model) {
		collisionFacade.Collision (model);
	}

    public void OnCollisionEnter2DManual(GameObject col)
    {
        CollisionModel model = new CollisionModel { mainCollider = this.gameObject, CollidedWith = col.gameObject };
        onCollisionFacade(model);

    }
}

