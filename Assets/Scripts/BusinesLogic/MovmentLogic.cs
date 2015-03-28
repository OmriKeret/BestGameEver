using UnityEngine;
using System.Collections;

public class MovmentLogic : MonoBehaviour {
	PhysicsLogic physicsLogic;
	PhyisicsController phyisicsController;
	// Use this for initialization
	void Start () {
		physicsLogic = this.gameObject.GetComponent<PhysicsLogic> ();
		phyisicsController = GameObject.Find("PlayerManager").GetComponent<PhyisicsController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveCharacter(MoveCharacterModel model){
		//
		model.player.AddForce (model.Direction * 1000);
		Debug.Log ("adding force in " + model.Direction + "direction");
	}
	public void HoverCharacter(MoveCharacterModel model){
		//moving 
		//phyisicsController.HoverPhyisics ();
	}
	public void StopHoverCharacter(MoveCharacterModel model){
		//moving 
		//phyisicsController.StopHoverPhyisics ();
	}
}