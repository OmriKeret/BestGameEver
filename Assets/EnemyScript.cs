using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	
	public bool DEBUG_MODE;
	Vector3 destination;
	bool travel = true;
	public float MAX_SPEED = 1f;

	GameObject leftPart,rightPart;


	// Use this for initialization
	void Start () {
		DEBUG_MODE = false;
		destination = GeneralPhysics.getRandomLocation ();
		if (DEBUG_MODE)
						Debug.Log (destination);
		leftPart = Resources.Load ("EnemyL") as GameObject;
		rightPart = Resources.Load ("EnemyR") as GameObject;
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag.Equals ("Player"))
						death ();
		}

	// Update is called once per frame
	void Update () {
		if (travel) {
						if (GeneralPhysics.onSpot (this.transform.position, destination))
								travel = false;
						else {
								if (rigidbody2D.velocity.magnitude < MAX_SPEED)
										rigidbody2D.AddForce (destination-transform.position);
						}
				} else
						rigidbody2D.velocity = new Vector2 (0, 0);
	}

	void death(){
		if (leftPart != null) {
						GameObject left = Instantiate (leftPart, transform.position, Quaternion.identity) as GameObject;
						left.rigidbody2D.AddForce (new Vector2 (-10, -10));
						Destroy (left, 1);
				}
		if (rightPart != null) {
						GameObject right = Instantiate (rightPart, transform.position, Quaternion.identity) as GameObject;
						right.rigidbody2D.AddForce (new Vector2 (10, -10));
						Destroy (right, 1);
				}
		Destroy (this.gameObject);
	}
}
