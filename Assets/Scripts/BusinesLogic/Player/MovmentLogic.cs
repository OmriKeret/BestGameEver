using UnityEngine;
using System.Collections;

public class MovmentLogic : MonoBehaviour {

    //dash
    public float dashTime = 1f;
    public float dashDist = 5f;

    //hover
    public float hoverTime = 1f;

    //rotation
    public float timeToRotate = 0.5f;
    public float delayTillRotat = 0.08f;
    public float timeToRotateBack = 0.1f;

    //fall
    public float fallTime = 6f;

    //general info
	Rigidbody2D character;

    //death
    public float timeToReturnFromFall = 1f;

    //logic
	AnimationLogic animationLogic;
    TouchInterpeter touch;
    PhysicsLogic physicsLogic;
    SoundLogic soundLogic;
    private PlayerStatsLogic playerStatsLogic;

	// Use this for initialization
	void Start () {
	//	physicsLogic = this.gameObject.GetComponent<PhysicsLogic> ();
		character = GameObject.Find("PlayerManager").GetComponent<Rigidbody2D>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
		animationLogic = this.gameObject.GetComponent<AnimationLogic>();
        soundLogic = this.gameObject.GetComponent<SoundLogic>();
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
	}

	public void MoveCharacter(MoveCharacterModel model){
      //  LeanTween.color(character.gameObject, Color.white, 0);
		//player out of dashes
        if (playerStatsLogic.dashNum <= 0)
        {
            return;
        }
		playerStatsLogic.removeOneDash();
        soundLogic.playJumpSound();
        ResetRotation();
        animationLogic.SetDashing();

        Vector2 target = new Vector2 (model.touchPoint.x, model.touchPoint.y);
		Vector2 vecBetween = target - model.player.position;
        var distToGo = vecBetween.magnitude;
        if (distToGo > dashDist)
        {
            distToGo = dashDist;
           //target = model.player.position + model.Direction * dashDist;
        }
		target = model.player.position + model.Direction * distToGo;
        RotateToDash(vecBetween);
		animationLogic.OnMoveSetDirection (new moveAnimationModel{direction = vecBetween.normalized});
        

        LeanTween.cancel(character.gameObject,true);
        LeanTween.move(model.player.gameObject, (Vector2)target, CalculateTimeForDistance(distToGo)).setEase(LeanTweenType.easeInOutQuad).setOnComplete(
            () =>
            {
                FinishedMoving(playerStatsLogic.combo);
            });
	}
    //function for keeping the speed the same
    private float CalculateTimeForDistance(float distance)
    {
        float maxTime = dashTime;
        float maxDist = dashDist;
        float relativeTime = Mathf.Sin(distance / maxDist);
        return relativeTime * maxTime; 
    }

	public void FinishedMoving(int combo) 
	{
        ResetRotation();
        animationLogic.UnSetDashing();
        animationLogic.CheckIfGrounded();
		if (playerStatsLogic.combo == combo) 
		{
			playerStatsLogic.resetCombo();
		}
		fallDown ();
	}

	public void fallDown() 
	{
        soundLogic.playFallSound();
        ResetRotation();
		Vector2 target = new Vector2 (character.transform.position.x, character.transform.position.y - 60);
        RotateCharFall();
        LeanTween.move(character.gameObject, (Vector2)target, fallTime).setEase(LeanTweenType.easeInCubic).setDelay(hoverTime);
	}



    internal void MoveOnFallDeath()
    {
        soundLogic.playJumpSound();
        ResetRotation();
        LeanTween.cancel(character.gameObject);
		animationLogic.OnMoveSetDirection(new moveAnimationModel {direction = new Vector2(-1,0)});
		playerStatsLogic.resetCombo();
        character.GetComponent<Collider2D>().enabled = false;
   //     var playerPosition = character.transform;
        touch.SetDisableMovment();
        Vector3[] path = new Vector3[] {
                                             new Vector3(12.16001f ,-19.35955f),
                                             new Vector3(12.16001f ,-10.35955f),
                                             new Vector3(13f ,4.531893f),
                                             new Vector3(1f ,8.917604f),
                                        };

        LeanTween.move(character.gameObject, path, timeToReturnFromFall).setEase(LeanTweenType.easeInOutQuad).setDelay(hoverTime).setOnComplete(() => 
            {
                FinishedReturningFromFall();
            });
    }

	private void FinishedReturningFromFall()
    {
        touch.UnsetDisableMovment();
        character.GetComponent<Collider2D>().enabled = true;
        fallDown();
    }

    public void RotateCharFall()
    {
        float angles = 40;
        if (animationLogic.faceRight)
        {
            angles = 360 - angles;
        }

        LeanTween.rotateZ(character.gameObject, angles, timeToRotate).setDelay(0.1f); 
    }

    public void RotateToDash(Vector2 dir)
    {
       Vector2 dashAnimationVector;
        var x = dir.normalized.x;
       if (x > 0)
       {
           dashAnimationVector = new Vector2(1f, 0.5f);
       }
       else
       {
           dashAnimationVector = new Vector2(-1f, 0.5f);
       }
     //  Quaternion newRotation = Quaternion.LookRotation(dir);

      character.transform.rotation = Quaternion.FromToRotation(dashAnimationVector, dir);

    }
    public void ResetRotation()
    {
        character.transform.rotation = Quaternion.identity;

    }



    internal void playerHit(Collider2D collider2D, int sign)
    {

        Debug.Log("disable collisions");
        character.GetComponent<Collider2D>().enabled = false;
        touch.SetDisableMovment();
        var playerPosition = character.position;
        Vector3[] path = new Vector3[] {
                                             new Vector3(playerPosition.x + (-2.697063f * sign) ,playerPosition.y),
                                             new Vector3(playerPosition.x + (-1.397063f * sign),playerPosition.y + 5.108706f),
                                             new Vector3(playerPosition.x + (0.397063f * sign),playerPosition.y + 6.408706f),
                                             new Vector3(playerPosition.x + (1f * sign),playerPosition.y + 7.749998f),
                                        };
        animationLogic.playerHit();
        LeanTween.move(character.gameObject, path, 0.4f).setOnComplete(() =>
        {
            stopAfterHitBounce(collider2D);

        });
    }

    private void stopAfterHitBounce(Collider2D collider2D)
    {
        if (collider2D != null)
        {
            var collidedWithCollider = collider2D;
            if (collidedWithCollider != null)
            {
                Physics2D.IgnoreCollision(collidedWithCollider, character.GetComponent<Collider2D>(), false); //remove collision ignorance
            }
        }
        character.GetComponent<Collider2D>().enabled = true;
        touch.UnsetDisableMovment();
        fallDown();
        animationLogic.UnSetDashing();
    }



    internal void playerHitwithCommet(Collider2D fatherCollider, Collider2D collider2D, int sign)
    {
        character.GetComponent<Collider2D>().enabled = false;
        touch.SetDisableMovment();
        var playerPosition = character.position;
        Vector3[] path = new Vector3[] {
                                             new Vector3(playerPosition.x + (-2.697063f * sign) ,playerPosition.y),
                                             new Vector3(playerPosition.x + (-1.397063f * sign),playerPosition.y + 5.108706f),
                                             new Vector3(playerPosition.x + (0.397063f * sign),playerPosition.y + 6.408706f),
                                             new Vector3(playerPosition.x + (1f * sign),playerPosition.y + 7.749998f),
                                        };
        animationLogic.playerHit();
        LeanTween.move(character.gameObject, path, 0.4f).setOnComplete(() =>
        {
            stopAfterHitBounce(fatherCollider,collider2D);
        });
    }

    private void stopAfterHitBounce(Collider2D fatherCollider, Collider2D collider2D)
    {
        if (collider2D != null)
        {
            var collidedWithCollider = collider2D;
            if (collidedWithCollider != null)
            {
                Physics2D.IgnoreCollision(collidedWithCollider, character.GetComponent<Collider2D>(), false); //remove collision ignorance
                Physics2D.IgnoreCollision(fatherCollider, character.GetComponent<Collider2D>(), false);

            }
        }
        character.GetComponent<Collider2D>().enabled = true;
        touch.UnsetDisableMovment();
        fallDown();
        animationLogic.UnSetDashing();
    }



    internal void movePlayerDie(int sign)
    {
        character.GetComponent<Collider2D>().enabled = false;
        var playerPosition = character.position;
        animationLogic.UnSetDashing();
        Vector3[] path = new Vector3[] {
                                             new Vector3(playerPosition.x + (-2.697063f * sign) ,playerPosition.y),
                                             new Vector3(playerPosition.x + (-1.397063f * sign),playerPosition.y + 5.108706f),
                                             new Vector3(playerPosition.x + (0.397063f * sign),playerPosition.y + 6.408706f),
                                             new Vector3(playerPosition.x + (1f * sign),playerPosition.y + 7.749998f),
                                        };
        animationLogic.playerHit();
        fallDown();
    }
}
