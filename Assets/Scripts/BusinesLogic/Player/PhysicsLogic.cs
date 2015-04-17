using UnityEngine;
using System.Collections;

public class PhysicsLogic : MonoBehaviour {
	public float HoverTime = 0.3f;
	public float playerGravityScale = 0f;
    public Vector3 amountOfShake;
	float startHoverTime;
	Rigidbody2D character;
	bool hover;
	bool afterDashHover;
	// Use this for initialization
	void Start () {
        amountOfShake = new Vector3(0, 0, 10);
	}

	void FixedUpdate(){
		if (hover) {
			if (Time.fixedTime - startHoverTime < HoverTime) {
				if (afterDashHover) {
					//character.velocity = new Vector2();
				}
				//wait
			} else {
				character.gravityScale = playerGravityScale;
				hover = false;
				afterDashHover = false;
			}
		}
	}

	public void Reset(ChangePhysicsModel model){
		model.player.gravityScale = playerGravityScale;
		hover = false;
		//consider items

	}

	public void Hover(ChangePhysicsModel model){
		hover = true;
		afterDashHover = true;
		startHoverTime = Time.fixedTime;
		character = model.player;
	}

	public void FingerHoldHover(ChangePhysicsModel model){
		hover = true;
		startHoverTime = Time.fixedTime;
		character = model.player;
		character.gravityScale = 0.05f;

		/*iTween.ShakeRotation(model.player.gameObject, iTween.Hash(
          "name", StaticVars.ITWEEN_PLAYER_SHAKE,
          "time", HoverTime,
          "amount", amountOfShake
          ));*/
	}

	public void stopHover(ChangePhysicsModel model){
		model.player.gravityScale = playerGravityScale;
		model.player.velocity = new Vector2 ();
		hover = false;
		afterDashHover = false;
		//iTween.StopByName(StaticVars.ITWEEN_PLAYER_SHAKE);
	}
}
