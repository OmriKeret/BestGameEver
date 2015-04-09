using UnityEngine;
using System.Collections;

public class CollisionLogic : MonoBehaviour  {
	public float YForce = 0f;
	public float XForce = 0f;
	public float enemyDeathTime = 1f;
	//public float enemyHitForce = 100f;
	public float impactTimeOnPlayer = 0.5f;
	private PhysicsLogic physicsLogic;
	private MovmentLogic movmentLogic;
	void Start () {
		physicsLogic = this.gameObject.GetComponent<PhysicsLogic> ();
		movmentLogic = this.gameObject.GetComponent<MovmentLogic> ();
	}
	public void playerCollideWithEnemy(CollisionModel model) {

		//to remove secondary collisions
		if (model.CollidedWith != null) {
			Physics2D.IgnoreCollision (model.CollidedWith.GetComponent<Collider2D> (), model.mainCollider.GetComponent<Collider2D> ());
		}
		var enemyPosition = model.CollidedWith.transform.position;
		var playerPosition = model.mainCollider.transform.position;
		var VectorForce = (Vector2)((playerPosition - enemyPosition).normalized);
		VectorForce.y = 600f;
	//	VectorForce *=  enemyHitForce;
		stopOtherEffectsOnPlayer(new ChangePhysicsModel{player = model.mainCollider.GetComponent<Rigidbody2D>()});
	//	model.mainCollider.GetComponent<Rigidbody2D> ().AddForce (VectorForce);

		this.gameObject.AddComponent<TimedAction>().doByTime(new TimeActionModel { 
									 subject = model.mainCollider.GetComponent<Rigidbody2D>(),
									 fixedTimeStart = Time.fixedTime,
								     durationTime = impactTimeOnPlayer,
									 stopingFunc = stopAfterBounce,
								     whileGoingDo = moveCircular,
									 impactForce = VectorForce,
								     collidedWith = model.CollidedWith
									});
	//	Debug.Log("Another impact");
	//	Debug.Log("____________________________________");
		//model.mainCollider.GetComponent<Rigidbody2D> ().AddForceAtPosition (VectorForce,                                                                   enemyPosition,           ForceMode2D.Impulse);
	}


	public void stopAfterBounce(StopAfterCollisionModel model){
		//Debug.Log("got to the after function");
		physicsLogic.Hover (new ChangePhysicsModel{ player = model.subject});
		if (model.collidedWith != null) {
			var collidedWithCollider = model.collidedWith.GetComponent<Collider2D> ();
			if (collidedWithCollider != null) {
				Physics2D.IgnoreCollision (collidedWithCollider, model.subject.GetComponent<Collider2D> (), false);
			}
		}

	}

	public MoveAfterCollisionModel moveCircular(MoveAfterCollisionModel model){		
	//	Debug.Log ("Force To Apply is: " + model.impactForce);
	//	Debug.Log ("________________________________");
		model.impactForce.x += XForce * Mathf.Sign(model.impactForce.x);
		model.impactForce.y -= YForce;//model.impactForce.y;
		model.impactForce.y = model.impactForce.y < -20 ? -20 : model.impactForce.y;
		model.subject.AddForce (model.impactForce);
		return model;
	}

	public void stopOtherEffectsOnPlayer(ChangePhysicsModel model){
		movmentLogic.StopMoving ();
		physicsLogic.Reset (model);
	}

	public void EnemyCollidedWithPlayer(CollisionModel model) {
		var enemyController = model.mainCollider.GetComponent<AIController> ();
		var enemy = model.mainCollider.GetComponent<IEnemy> ();
		var position = (Vector2)model.mainCollider.transform.position ;
		enemy.Death ();
		enemy.Split (position);
	/*	this.gameObject.AddComponent<TimedAction>().doByTime(new TimeActionModel { 
			subject = model.mainCollider.GetComponent<Rigidbody2D>(),
			fixedTimeStart = Time.fixedTime,
			durationTime = enemyDeathTime,
			stopingFunc = enemyController.death
		});
	*/
	}
}
