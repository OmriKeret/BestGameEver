using UnityEngine;
using System.Collections;

public class CollisionLogic : MonoBehaviour  {
	public float YForce = 0f;
	public float XForce = 0f;
	public float enemyDeathTime = 1f;
	//public float enemyHitForce = 100f;
	public float impactTimeOnPlayer = 0.5f;
	private PhysicsLogic physicsLogic;
	private MovmentLogic movmentLogic;
    private ScoreLogic scoreLogic;
    private PlayerStatsLogic playerStatsLogic;
    private MissionLogic missionLogic;
	void Start () {
		physicsLogic = this.gameObject.GetComponent<PhysicsLogic> ();
		movmentLogic = this.gameObject.GetComponent<MovmentLogic> ();
        scoreLogic = this.gameObject.GetComponent<ScoreLogic>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
	}
	public void playerCollideWithEnemy(CollisionModel model) {
		//to remove secondary collisions
		if (model.CollidedWith != null) {
			Physics2D.IgnoreCollision (model.CollidedWith.GetComponent<Collider2D> (), model.mainCollider.GetComponent<Collider2D> ());
		}
		var enemyPosition = model.CollidedWith.transform.position;
		var playerPosition = model.mainCollider.transform.position;
		var VectorForce = (Vector2)((playerPosition - enemyPosition).normalized);

		
	    //if player hit some1 than he get back is dashes
        playerStatsLogic.resetDash();
		playerStatsLogic.addOneToCombo ();
        
        //building path
        var sign = VectorForce.x > 0 ? 1 : -1;
        Vector3[] path = new Vector3[] {
                                             new Vector3(playerPosition.x,playerPosition.y),
                                             new Vector3(playerPosition.x + (0.397063f * sign),playerPosition.y + 7.108706f),
                                             new Vector3(playerPosition.x + (1.96f * sign),playerPosition.y + 8.749998f),
                                        };
        iTween.MoveTo(model.mainCollider, iTween.Hash(
           "name", StaticVars.ITWEEN_PLAYER_MOVMENT,
           "time", impactTimeOnPlayer,
           "path", path,
           "oncomplete", "stopAfterBounce",
           "oncompleteparams", new StopAfterCollisionModel
                       {
                           subject = model.mainCollider.GetComponent<Rigidbody2D>(),
                           collidedWith = model.CollidedWith
                       }

                                                             ));

	}

	public void stopAfterBounce(StopAfterCollisionModel model){

		if (model.collidedWith != null) {
			var collidedWithCollider = model.collidedWith.GetComponent<Collider2D> ();
			if (collidedWithCollider != null) {
				Physics2D.IgnoreCollision (collidedWithCollider, model.subject.GetComponent<Collider2D> (), false); //remove collision ignorance
			}
		}

	}

	public MoveAfterCollisionModel moveCircular(MoveAfterCollisionModel model){		
		model.impactForce.x += XForce * Mathf.Sign(model.impactForce.x);
    	model.impactForce.y -= YForce;//model.impactForce.y;
		model.impactForce.y = model.impactForce.y < -20 ? -20 : model.impactForce.y;
		model.subject.AddForce (model.impactForce);
		return model;
	}

	public void stopOtherEffectsOnPlayer(ChangePhysicsModel model){
		movmentLogic.StopMoving ();
		physicsLogic.Reset (model);
	}

	public void EnemyCollidedWithPlayer(CollisionModel model) {
		var enemyController = model.mainCollider.GetComponent<AIController> ();
		var enemy = model.mainCollider.GetComponent<IEnemy> ();
		var position = (Vector2)model.mainCollider.transform.position;
        if (enemyController.lifeDown(playerStatsLogic.Strength)) //if enemy dead
        {
            scoreLogic.addPoint(new AddPointModel { type = enemyController.type, combo = playerStatsLogic.combo });
            missionLogic.addKill(enemyController.type);
            var Itweenpart = model.mainCollider.GetComponent<iTween> ();
            if (Itweenpart != null)
            {
                Destroy(Itweenpart);
            }
            enemy.Death();
            enemy.Split(position);
        }

	}
}
