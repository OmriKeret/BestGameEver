using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{

    private BasicEnemyLogic _logic;
    private BasicEnemyStats _stats;
    Vector2 _creationLocation, _movementDirection;
    public CollisionFacade collisionFacade;


    // Use this for initialization
    void Start()
    {
        _logic = this.gameObject.GetComponent<BasicEnemyLogic>();
        _stats = this.gameObject.GetComponent<BasicEnemyStats>();
        _creationLocation = transform.position;
        collisionFacade = new CollisionFacade();

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        //    Debug.Log("collided with commet");
        CollisionModel model = new CollisionModel { mainCollider = this.gameObject, CollidedWith = col.gameObject };
        onCollisionFacade(model);
    }
    public bool lifeDown(int hitStrength)
    {

        _stats.lifeDown(hitStrength);
        //    Debug.Log("hitting enemy with strength: " + hitStrength + "\n enemy has health of: " + _stats.life + " is dead: " + _stats.isDead());
        return _stats.isDead();
    }

    public void death(StopAfterCollisionModel s)
    {
        //_logic.split (transform.position);
        _logic.Death();
    }

    // Update is called once per frame
    void Update()
    {
        //_logic.MoveInDirection (_movementDirection);

    }

    public void onCollisionFacade(CollisionModel model)
    {

        collisionFacade.Collision(model);
    }

    public void OnCollisionEnter2DManual(GameObject col)
    {
        CollisionModel model = new CollisionModel { mainCollider = this.gameObject, CollidedWith = col.gameObject };
        onCollisionFacade(model);

    }

    public EnemyType type
    {
        get
        {
            return _stats._type;
        }
    }
}

