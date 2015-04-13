using UnityEngine;
using System.Collections;

public class MovmentLogic : MonoBehaviour {
	PhysicsLogic physicsLogic;
	PhyisicsController phyisicsController;
	public float dashTime = 2f;
	public float speed = 50f;
	bool moveChar;
	Vector2 current;
	Vector2 target;
	Rigidbody2D character;
	float startTime;
	public float dashDist = 5f;
    private PlayerStatsLogic playerStatsLogic;
	float step;
    public float timeToReturnFromFall = 1f;
	AnimationLogic animationLogic;
	// Use this for initialization
	void Start () {
	//	physicsLogic = this.gameObject.GetComponent<PhysicsLogic> ();
		phyisicsController = GameObject.Find("PlayerManager").GetComponent<PhyisicsController>();
		character = GameObject.Find("PlayerManager").GetComponent<Rigidbody2D>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
		animationLogic = this.gameObject.GetComponent<AnimationLogic>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	public void MoveCharacter(MoveCharacterModel model){

		//player out of dashes
        if (playerStatsLogic.dashNum <= 0)
        {
            return;
        }
		playerStatsLogic.dashNum--;

        target = new Vector2 (model.touchPoint.x, model.touchPoint.y);
		Vector2 vecBetween = target - model.player.position;
		var distToGo = vecBetween.magnitude;
		if (distToGo > dashDist) {
			distToGo = dashDist;
			target = model.player.position + model.Direction * distToGo;
		}
		animationLogic.OnMoveSetDirection (new moveAnimationModel{direction = vecBetween.normalized});
        iTween.MoveTo(model.player.gameObject, iTween.Hash(
            "name", StaticVars.ITWEEN_PLAYER_MOVMENT,
            "speed", speed,
            "position", (Vector3)target,
			"easetype", iTween.EaseType.easeInOutQuad,
			"oncomplete", "FinishedMoving",
			"oncompleteparams",playerStatsLogic.combo,
			"oncompletetarget", this.gameObject
            ));
	}
	public void FinishedMoving(int combo) 
	{
		if (playerStatsLogic.combo == combo) 
		{
			playerStatsLogic.resetCombo();
		}
	}
    public void oncomplete()
    {
        phyisicsController.AfterDashHover();
    }

	public void HoverCharacter(MoveCharacterModel model){
		//moving 
		phyisicsController.fingerHoldHover ();
	}
	public void StopHoverCharacter(MoveCharacterModel model){
		//moving 
		phyisicsController.StopHoverPhyisics ();
	}
	public void StopMoving(){
		character.velocity = new Vector2 (0,0);
		moveChar = false;

	}

    internal void MoveOnFallDeath()
    {
		playerStatsLogic.resetCombo();
        character.GetComponent<Collider2D>().enabled = false;
        var playerPosition = character.transform;
        Vector3[] path = new Vector3[] {
                                             new Vector3(12.16001f ,-19.35955f),
                                             new Vector3(10.02703f ,1.531893f),
                                             new Vector3(4.62332f ,6.917604f),
                                        };
        iTween.MoveTo(character.gameObject, iTween.Hash(
           "name", StaticVars.ITWEEN_PLAYER_RETURNFROMFALL,
           "time", timeToReturnFromFall,
           "path", path,
           "oncomplete", "FinishedReturningFromFall",
           "easetype", iTween.EaseType.linear,
           "oncompletetarget", this.gameObject
           ));
    }

	private void FinishedReturningFromFall()
    {
        character.GetComponent<Collider2D>().enabled = true;
        character.AddForce(new Vector2(100, 100));
    }
}
