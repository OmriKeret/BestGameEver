using UnityEngine;
using System.Collections;

public class StupidAILogic : MonoBehaviour , IEnemy{

	EnemyGeneralStats _stats;
	Rigidbody2D _rigidbody;
	GameObject _leftBodyPartResouce,_rightBodyPartResouce;
	GameObject _leftBodyPart,_rightBodyPart;

	// Use this for initialization
	void Awake () {
		_rigidbody = GetComponent<Rigidbody2D> ();
		_leftBodyPartResouce = Resources.Load ("stupidL") as GameObject;
		_rightBodyPartResouce = Resources.Load ("stupidR") as GameObject;
		GetComponent<Rigidbody2D> ().gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

	}

	//TODO:
	void IEnemy.death(){
		Destroy (this.gameObject);
		}

	Vector2 IEnemy.moveToPoint(Vector2 point){
		return (new Vector2(point.x*(-1),point.y*(-1))).normalized;
	}

	//TODO: Get the location from the game manager
	Vector2 IEnemy.findPlayerLocation(){
		return new Vector2(0,0);
	}

	public void setStats(EnemyGeneralStats i_stats){
		_stats = i_stats;
	}

	void IEnemy.moveInDirection(Vector2 i_direction){
		if (_rigidbody.velocity.magnitude < _stats.MAX_SPEED){
			_rigidbody.AddForce(i_direction);
		}
	}

	void IEnemy.split(Vector2 i_location){
		_leftBodyPart = Instantiate (_leftBodyPartResouce, i_location, Quaternion.identity) as GameObject;
		if (_leftBodyPart != null) {
			_leftBodyPart.GetComponent<Rigidbody2D>().AddForce(_stats.leftSplitLocation);
				}
		_rightBodyPart = Instantiate (_rightBodyPartResouce, i_location, Quaternion.identity) as GameObject;
		if (_rightBodyPart != null) {
			_rightBodyPart.GetComponent<Rigidbody2D>().AddForce(_stats.leftSplitLocation*(-1));
		}
	}
	
}
