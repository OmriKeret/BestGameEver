using UnityEngine;
using System.Collections;

public class StupidAILogic : MonoBehaviour , IEnemy{

	AEnemyStats _stats;
	Rigidbody2D _rigidbody;
	GameObject _leftBodyPartResouce,_rightBodyPartResouce;
	GameObject _leftBodyPart,_rightBodyPart;
    public float timeToFinishPath = 15f;
    public float minTimeForPath = 4f;
    public float maxTimeForPath = 30f;
	// Use this for initialization
	void Awake () {
        _stats = GetComponent<AEnemyStats>();
		_rigidbody = GetComponent<Rigidbody2D> ();
		_leftBodyPartResouce = Resources.Load ("stupidL") as GameObject;
		_rightBodyPartResouce = Resources.Load ("stupidR") as GameObject;
		GetComponent<Rigidbody2D> ().gravityScale = 0;
	}

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

    public void setPath(Vector3[] path, int speed)
    {
        LeanTween.move(this.gameObject, path, calculateTime(speed)).setEase(LeanTweenType.linear).setOnComplete(() =>
        {
            FinishedMoving();
        });
    }

   public void FinishedMoving()
    {
        Destroy(this.gameObject);
    }

   public float calculateTime(float speed)
   {
       speed = speed > _stats.MAX_SPEED ? _stats.MAX_SPEED : speed;
       return minTimeForPath * (_stats.MAX_SPEED / speed);
   }
	
}
