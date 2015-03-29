using UnityEngine;
using System.Collections;

public class PhysicsLogic : MonoBehaviour {
	public float HoverTime = 100f;
	float startHoverTime;
	Rigidbody2D character;
	bool hover;
	bool afterDashHover;
	// Use this for initialization
	void Start () {

	}

	void FixedUpdate(){
		if (hover) {
			if (Time.fixedTime - startHoverTime < HoverTime) {
				if (afterDashHover) {
					character.velocity = new Vector2();
				}
				//wait
			} else {
				character.gravityScale = 1f;
				hover = false;
				afterDashHover = false;
			}
		}
	}

	public void Reset(ChangePhysicsModel model){
		model.player.gravityScale = 1f;
		hover = false;
		//consider items

	}

	public void Hover(ChangePhysicsModel model){
		hover = true;
		afterDashHover = true;
		startHoverTime = Time.fixedTime;
		character = model.player;
		character.gravityScale = 0.01f;
	}

	public void FingerHoldHover(ChangePhysicsModel model){
		hover = true;
		startHoverTime = Time.fixedTime;
		character = model.player;
		character.gravityScale = 0.05f;
	}

	public void stopHover(ChangePhysicsModel model){
		model.player.gravityScale = 1f;
		hover = false;
		afterDashHover = false;
	}
}
