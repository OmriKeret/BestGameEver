using UnityEngine;
using System.Collections;

public class CollisionLogic : MonoBehaviour  {

	public float enemyDeathTime = 1f;

    //collision with enemy
	public float impactTimeOnPlayer = 0.5f;
    public float rollTime = 0.5f;
	private PhysicsLogic physicsLogic;
	private MovmentLogic movmentLogic;
    private ScoreLogic scoreLogic;
    private PlayerStatsLogic playerStatsLogic;
    private MissionLogic missionLogic;
    AnimationLogic animationLogic;
    SoundLogic soundLogic;
	void Start () {
		physicsLogic = this.gameObject.GetComponent<PhysicsLogic> ();
		movmentLogic = this.gameObject.GetComponent<MovmentLogic> ();
        scoreLogic = this.gameObject.GetComponent<ScoreLogic>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        animationLogic = this.gameObject.GetComponent<AnimationLogic>();
        soundLogic = this.gameObject.GetComponent<SoundLogic>();
	}
	public void playerCollideWithEnemy(CollisionModel model) {
        if (playerStatsLogic.powerUpModeActive == PowerUpType.SUPERHIT)
        {
            playerStatsLogic.resetDash();
            playerStatsLogic.addOneToCombo();
            return;
        }
        //if(model.CollidedWith.GetComponent<>.stats == )

        movmentLogic.ResetRotation();
        LeanTween.cancel(model.mainCollider.gameObject, true);
		//to remove secondary collisions
		if (model.CollidedWith != null) {
			Physics2D.IgnoreCollision (model.CollidedWith.GetComponent<Collider2D> (), model.mainCollider.GetComponent<Collider2D> ());
		}
		var enemyPosition = model.CollidedWith.transform.position;
		var playerPosition = model.mainCollider.transform.position;
		var VectorForce = (Vector2)((playerPosition - enemyPosition).normalized);
        soundLogic.playSliceSound();
		
	    //if player hit some1 than he get back is dashes
        playerStatsLogic.resetDash();

        var sign = VectorForce.x > 0 ? 1 : -1;
        if (true)
        {
            movmentLogic.playerHit(model.CollidedWith.GetComponent<Collider2D>(), sign);
            return;
        }
		playerStatsLogic.addOneToCombo ();
        
        //building path
       
        Vector3[] path = new Vector3[] {
                                             new Vector3(playerPosition.x + (-2.697063f * sign) ,playerPosition.y),
                                             new Vector3(playerPosition.x + (-1.397063f * sign),playerPosition.y + 5.108706f),
                                             new Vector3(playerPosition.x + (0.397063f * sign),playerPosition.y + 6.408706f),
                                             new Vector3(playerPosition.x + (1f * sign),playerPosition.y + 7.749998f),
                                        };
 

        LeanTween.move(model.mainCollider.gameObject, path, impactTimeOnPlayer).setOnComplete(() => 
            {
                stopAfterBounce(new StopAfterCollisionModel{collidedWith = model.CollidedWith.gameObject, subject = model.mainCollider.GetComponent<Rigidbody2D> ()});
            });
        rollOver(model.mainCollider);       
        animationLogic.UnSetDashing();
        animationLogic.SetSlicing();
	}

    public void playerCollideWithPowerUp(CollisionModel model)
    {
        return;
    }

    public void rollOver(GameObject player)
    {
        soundLogic.playSpinningeSound();
        float numberOfRolls = 5;
        LeanTween.rotateZ(player, 360f * numberOfRolls, impactTimeOnPlayer + rollTime);
    }
	public void stopAfterBounce(StopAfterCollisionModel model){

		if (model.collidedWith != null) {
			var collidedWithCollider = model.collidedWith.GetComponent<Collider2D> ();
			if (collidedWithCollider != null) {
				Physics2D.IgnoreCollision (collidedWithCollider, model.subject.GetComponent<Collider2D> (), false); //remove collision ignorance
			}
		}
		movmentLogic.fallDown ();
        animationLogic.UnSetDashing();

	}


	public void EnemyCollidedWithPlayer(CollisionModel model) {
     //   Debug.Log("enemy collision detected");
		var enemyController = model.mainCollider.GetComponent<AIController> ();
		var enemy = model.mainCollider.GetComponent<IEnemy> ();
		var position = (Vector2)model.mainCollider.transform.position;
        if (enemy.lifeDown(playerStatsLogic.Strength)) //if enemy dead
        {
            scoreLogic.addPoint(new AddPointModel { type = enemyController.type, combo = playerStatsLogic.combo });
            missionLogic.addKill(enemyController.type);
            LeanTween.cancel(model.mainCollider.gameObject, false);
            if (Time.timeScale != 0) //could happen in super hit power up
            {
				enemy.Split(position);
                enemy.Death();

            }

        }

	}

    public void killEnemy(GameObject enemy)
    {
        var position = enemy.transform.position;
        var enemyLogic = enemy.GetComponent<IEnemy>();
		enemyLogic.Split(position);
		enemyLogic.Death ();
    }
	public void playerCollidedWithWall(CollisionModel model) 
	{
        LeanTween.cancel(model.mainCollider.gameObject, true);
        movmentLogic.ResetRotation();
        animationLogic.UnSetDashing();
        animationLogic.CheckIfGrounded();
        soundLogic.playLandingSound();
	}
}
