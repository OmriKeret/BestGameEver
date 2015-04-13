using UnityEngine;
using System.Collections;

public class StupidAILogic : MonoBehaviour , IEnemy{

	AEnemyStats _stats;
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
	void IEnemy.Death(){
		Destroy (this.gameObject);
		}

	Vector2 IEnemy.MoveToPoint(Vector2 point){
		return (new Vector2(point.x*(-1),point.y*(-1))).normalized;
	}

	//TODO: Get the location from the game manager
	Vector2 IEnemy.FindPlayerLocation(){
		return new Vector2(0,0);
	}

	public void SetStats(AEnemyStats i_stats){
		_stats = i_stats;
	}

	void IEnemy.MoveInDirection(Vector2 i_direction){
		if (_rigidbody.velocity.magnitude < _stats.MAX_SPEED){
			_rigidbody.AddForce(i_direction);
		}
	}

	void IEnemy.Split(Vector2 i_location){
		_leftBodyPart = Instantiate (_leftBodyPartResouce, i_location, Quaternion.identity) as GameObject;
		if (_leftBodyPart != null) {
            _leftBodyPart.GetComponent<PartOfEnemyFadeOut>().FadeAndDestoryUp();
				}
		_rightBodyPart = Instantiate (_rightBodyPartResouce, i_location, Quaternion.identity) as GameObject;
		if (_rightBodyPart != null) {
            _rightBodyPart.GetComponent<PartOfEnemyFadeOut>().FadeAndDestoryDown(); ;
		}
	}

    public void setPath(string path, int speed)
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
           "name", StaticVars.ITWEEN_ENEMY_MOVMENT,
           "speed", speed,
           "path", iTweenPath.GetPath(path),
           "ease" ,iTween.EaseType.easeOutExpo,
           "oncomplete", "FinishedMoving",
           "oncompletetarget", this.gameObject
           ));
    }

   public void FinishedMoving()
    {
        Destroy(this.gameObject);
    }

	
}
