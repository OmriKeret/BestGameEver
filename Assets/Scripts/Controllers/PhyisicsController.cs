using UnityEngine;
using System.Collections;

public class PhyisicsController : MonoBehaviour {

	
	public PhysicsLogic physicsLogic;
	// Use this for initialization
	void Start () {
		physicsLogic = GameObject.Find("Logic").GetComponent<PhysicsLogic>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void ResetPhysics(){
		//consider items
		ChangePhysicsModel model = new ChangePhysicsModel{player = this.gameObject.GetComponent<Rigidbody2D>()};
		physicsLogic.Reset (model);
	}
	
	public void HoverPhyisics(){
		ChangePhysicsModel model = new ChangePhysicsModel{player = this.gameObject.GetComponent<Rigidbody2D>()};
		physicsLogic.Hover (model);
		
	}
}
