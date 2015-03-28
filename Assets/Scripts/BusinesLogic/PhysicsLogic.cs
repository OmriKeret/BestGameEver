using UnityEngine;
using System.Collections;

public class PhysicsLogic : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

	}

	public void Reset(ChangePhysicsModel model){
		model.player.gravityScale = 1f;
		//consider items

	}

	public void Hover(ChangePhysicsModel model){
		model.player.gravityScale = 0.3f;
	}
}
