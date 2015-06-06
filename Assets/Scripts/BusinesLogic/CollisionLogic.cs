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
    private TouchInterpeter touch;

    AnimationLogic animationLogic;
    SoundLogic soundLogic;
    DeathLogic deathLogic;

    //landing
    private GameObject landDust;

	void Start () {
        landDust = Resources.Load("Spurt") as GameObject;
		physicsLogic = this.gameObject.GetComponent<PhysicsLogic> ();
		movmentLogic = this.gameObject.GetComponent<MovmentLogic> ();
        scoreLogic = this.gameObject.GetComponent<ScoreLogic>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        animationLogic = this.gameObject.GetComponent<AnimationLogic>();
        soundLogic = this.gameObject.GetComponent<SoundLogic>();
        deathLogic = this.gameObject.GetComponent<DeathLogic>();
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
	}
    public void playerCollideWithCommet(CollisionModel model)
    {
        if (playerStatsLogic.powerUpModeActive == PowerUpType.SUPERHIT)
        {
            return;
        }
        Debug.Log("playerCollided with commet");
        movmentLogic.ResetRotation();
        LeanTween.cancel(model.mainCollider.gameObject, true);
        var fatherCollider = model.CollidedWith.transform.parent.GetComponent<Collider2D>();
        if (model.CollidedWith != null )
        {
            Physics2D.IgnoreCollision(model.CollidedWith.GetComponent<Collider2D>(), model.mainCollider.GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(fatherCollider, model.mainCollider.GetComponent<Collider2D>());
        }
        var enemyPosition = model.CollidedWith.transform.position;
        var playerPosition = model.mainCollider.transform.position;
        var VectorForce = (Vector2)((playerPosition - enemyPosition).normalized);
        //soundLogic.playSliceSound();
        //if player hit some1 than he get back is dashes
        playerStatsLogic.resetDash();
        soundLogic.playHittedSound();
        var sign = VectorForce.x > 0 ? 1 : -1;

        movmentLogic.playerHitwithCommet(fatherCollider,model.CollidedWith.GetComponent<Collider2D>(), sign);
        if (playerStatsLogic.removeHp(1))
        {
            deathLogic.playerDie(sign);
        }
    }
	public void playerCollideWithEnemy(CollisionModel model) {
        if (playerStatsLogic.powerUpModeActive == PowerUpType.SUPERHIT)
        {
            playerStatsLogic.resetDash();
            playerStatsLogic.addOneToCombo();
            return;
        }
       // Debug.Log("playerCollided with enemy");

        movmentLogic.ResetRotation();
        LeanTween.cancel(model.mainCollider.gameObject, false); //else the movment will reset the combo
		//to remove secondary collisions
		if (model.CollidedWith != null ) {
			Physics2D.IgnoreCollision (model.CollidedWith.GetComponent<Collider2D> (), model.mainCollider.GetComponent<Collider2D> ());

		}
		var enemyPosition = model.CollidedWith.transform.position;
		var playerPosition = model.mainCollider.transform.position;
		var VectorForce = (Vector2)((playerPosition - enemyPosition).normalized);
       
		
	    //if player hit some1 than he get back is dashes
        playerStatsLogic.resetDash();

        var sign = VectorForce.x > 0 ? 1 : -1;
        var enemyStats = model.CollidedWith.GetComponent<AEnemyStats>();
        if (enemyStats._mode == EnemyMode.Attack || enemyStats._mode == EnemyMode.Both)
        {
            soundLogic.playHittedSound();
           // Debug.Log("player Collided with commet");
            movmentLogic.playerHit(model.CollidedWith.GetComponent<Collider2D>(), sign);
            if (playerStatsLogic.removeHp(1))
            {
                deathLogic.playerDie(sign);
            }
           
            return;
        }
        soundLogic.playSliceSound();
		playerStatsLogic.addOneToCombo ();
        
        //building path
       
        Vector3[] path = new Vector3[] {
                                             new Vector3(playerPosition.x + (4.48f * sign) ,playerPosition.y),
                                             new Vector3(playerPosition.x + (5f * sign),playerPosition.y + 1.81f),
                                             new Vector3(playerPosition.x + (5.519f * sign),playerPosition.y + 0.8f),
                                             new Vector3(playerPosition.x + (2.55f * sign),playerPosition.y + 2.59f),
                                        };
 

        LeanTween.move(model.mainCollider.gameObject, path, impactTimeOnPlayer).setOnComplete(() => 
            {
                stopAfterBounce(new StopAfterCollisionModel{collidedWith = model.CollidedWith.gameObject, subject = model.mainCollider.GetComponent<Rigidbody2D> ()});
            });
      //  rollOver(model.mainCollider);       
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
     //   Debug.Log("enemy collision detected. Enemy mode is:");
		var enemyController = model.mainCollider.GetComponent<AIController> ();
		var enemy = model.mainCollider.GetComponent<IEnemy> ();
		var position = (Vector2)model.mainCollider.transform.position;
	    EnemyMode mode = enemy.GetEnemyMode();
       // Debug.Log(mode);
        

	    switch (mode)
	    {
            case EnemyMode.Both: hitPlayer(); enemyDefende(); break; 
            case EnemyMode.Defence: enemyDefende(); break;
            case EnemyMode.Attack: hitPlayer();
	            goto case EnemyMode.None;
            case EnemyMode.None:
                if (hitEnemy(model)) //if enemy dead
                 {
                   //  Debug.Log("kill enemy");
                     scoreLogic.addPoint(new AddPointModel { type = enemyController.type, combo = playerStatsLogic.combo });
                     missionLogic.addKill(enemyController.type);
                     LeanTween.cancel(model.mainCollider.gameObject, false);
                     if (Time.timeScale != 0) //could happen in super hit power up
                     {
                         enemy.playDeathSound();
                         enemy.Split(position);
                         enemy.Death();
                     }

                 } break;
	    }

        

	}

    private bool hitEnemy(CollisionModel model)
    {
      
        var enemyPosition = model.CollidedWith.transform.position;
        var playerPosition = model.mainCollider.transform.position;
        var VectorForce = (Vector2)((playerPosition - enemyPosition).normalized);
        var signX = VectorForce.x > 0 ? 1 : -1;
        var signY = VectorForce.y > 0 ? 1 : -1;
        var enemy = model.mainCollider.GetComponent<IEnemy>();
        bool dead = enemy.lifeDown(playerStatsLogic.Strength);
        if (dead)
        {
            enemy.enemyDie(playerStatsLogic.combo, VectorForce);
        } else {
            // GUI
            enemy.hit(playerStatsLogic.combo, VectorForce);
        }
        return dead;
    }
    //TODO: Omri - what to do when the enemy is on defence mode (e.g. tank)
    private void enemyDefende()
    {
    //    Debug.Log("Defended");
    }

    //TODO: Omri - what to do when the enemy attack the player (e.g. hits spike AKA abu-nafha)
    private void hitPlayer()
    {
      //  Debug.Log("Attacked");
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
            animationLogic.Land();
            var podiumCollider = model.CollidedWith.GetComponent<BoxCollider2D>();

            // Reseting the rotation
            var storedRotation = podiumCollider.transform.rotation;

            Vector2 size = podiumCollider.size;
            Vector3 centerPoint = new Vector3(podiumCollider.offset.x, podiumCollider.offset.y, 0f);
            Vector3 worldPos = model.CollidedWith.gameObject.transform.TransformPoint(podiumCollider.offset);
            float btm = worldPos.y - ((size.y * podiumCollider.transform.localScale.y) / 2f);
            float top = worldPos.y + ((size.y * podiumCollider.transform.localScale.y) / 2f);
            float left = worldPos.x - ((size.x * podiumCollider.transform.localScale.x) / 2f);
            float right = worldPos.x + ((size.x * podiumCollider.transform.localScale.x) / 2f);
            Vector3 topLeft = new Vector3(left, top, worldPos.z);
            Vector3 topRight = new Vector3(right, top, worldPos.z);

            var podiumLogic = model.CollidedWith.gameObject.GetComponent<PodiumLogic>();
            var pos = model.CollidedWith.transform.position;
            pos.y = top;
            Instantiate(landDust, pos, Quaternion.identity);

 
            model.mainCollider.gameObject.transform.position = new Vector3((topLeft.x + topRight.x) / 2, top);

            soundLogic.playLandingSound();
            playerStatsLogic.resetDash();

	}

}
