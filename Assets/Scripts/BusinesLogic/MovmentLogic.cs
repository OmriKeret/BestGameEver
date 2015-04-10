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
	// Use this for initialization
	void Start () {
	//	physicsLogic = this.gameObject.GetComponent<PhysicsLogic> ();
		phyisicsController = GameObject.Find("PlayerManager").GetComponent<PhyisicsController>();
		character = GameObject.Find("PlayerManager").GetComponent<Rigidbody2D>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (moveChar) {
			phyisicsController.StopHoverPhyisics();
			if(Time.fixedTime - startTime < dashTime && ((Vector2)character.transform.position != target) && step != dashDist) {

				step += speed * Time.fixedDeltaTime;
				step = step < dashDist ? step : dashDist ;
				character.transform.position = Vector2.MoveTowards (current, target, step);

			} else {
				moveChar = false;
				phyisicsController.AfterDashHover();

                //player didnt hit a thing :(
                playerStatsLogic.resetCombo();
			}
		}
	}

	public void MoveCharacter(MoveCharacterModel model){

		//player out of dashes
        if (playerStatsLogic.dashNum <= 0)
        {
            return;
        }

        playerStatsLogic.removeOneDash();
		step = 0f;
		startTime = Time.fixedTime;
		moveChar = true;
		current = model.player.transform.position;
		character = model.player;
		target = new Vector2 (model.touchPoint.x, model.touchPoint.y);
		//model.player.AddForce (model.Direction * 600);
	//	Debug.Log ("adding force in " + model.Direction + "direction");
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
}