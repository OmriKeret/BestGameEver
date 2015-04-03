using UnityEngine;
using System.Collections;

public class CollisionLogic : MonoBehaviour  {

	public float enemyHitForce = 100f;
	public float impactTime = 0.5f;
	private PhysicsLogic physicsLogic;
	private MovmentLogic movmentLogic;
	void Start () {
		physicsLogic = this.gameObject.GetComponent<PhysicsLogic> ();
		movmentLogic = this.gameObject.GetComponent<MovmentLogic> ();
	}
	public void playerCollideWithEnemy(CollisionModel model) {
		var enemyPosition = model.CollidedWith.transform.position;
		var playerPosition = model.mainCollider.transform.position;
		var VectorForce = (playerPosition - enemyPosition) * enemyHitForce;

	//	Debug.Log("Player position is: " + playerPosition);
	//	Debug.Log("Enemy position is: " + enemyPosition);
	//	Debug.Log ("Force To Apply is: " + VectorForce);
	//	Debug.Log ("________________________________");
		stopOtherEffectsOnPlayer(new ChangePhysicsModel{player = model.mainCollider.GetComponent<Rigidbody2D>()});
		model.mainCollider.GetComponent<Rigidbody2D> ().AddForce (VectorForce);
		//model.mainCollider.GetComponent<Rigidbody2D> ().AddForceAtPosition (VectorForce,                                                                   enemyPosition,           ForceMode2D.Impulse);
	}

	void FixedUpdate () {
		Time.fixedTime - startTime < dashTime
	}

	public void stopOtherEffectsOnPlayer(ChangePhysicsModel model){
		movmentLogic.StopMoving ();
		physicsLogic.Reset (model);
	}
}
