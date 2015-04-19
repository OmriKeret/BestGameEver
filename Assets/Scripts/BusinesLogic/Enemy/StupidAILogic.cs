﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StupidAILogic : MonoBehaviour , IEnemy{

	AEnemyStats _stats;
	Rigidbody2D _rigidbody;
	GameObject _leftBodyPartResouce,_rightBodyPartResouce;
	GameObject _leftBodyPart,_rightBodyPart;
    public float timeToFinishPath = 15f;
    public float minTimeForPath = 4f;
    public float maxTimeForPath = 30f;
    private Vector3[][] _allPaths ;
    AudioSource _audioSource;
	// Use this for initialization
	void Awake () {
        _audioSource = GetComponent<AudioSource>();
        _stats = GetComponent<AEnemyStats>();
		_rigidbody = GetComponent<Rigidbody2D> ();
		_leftBodyPartResouce = Resources.Load ("stupidL") as GameObject;
		_rightBodyPartResouce = Resources.Load ("stupidR") as GameObject;
		GetComponent<Rigidbody2D> ().gravityScale = 0;
	    _allPaths = initPaths();
	}
   public bool lifeDown(int str)
    {
        _stats.lifeDown(str);
        return isDead();
    }

   public bool isDead()
    {
        return _stats.life <= 0;
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

    public void StartRandomPath(int speed)
    {
        Vector3[] path;
        selectRandomPath(out path);

        LeanTween.move(this.gameObject, path, calculateTime(speed)).setEase(LeanTweenType.linear).setOnComplete(() =>
        {
            FinishedMoving();
        });
    }

    public void selectRandomPath(out Vector3[] i_path)
    {
        int pathNumber = UnityEngine.Random.Range(0, _allPaths.Length);
        if (pathNumber == 1)
        {
            Debug.Log("right");
            goRight();
        }
        i_path = _allPaths[pathNumber];
    }

    public EnemyMode GetEnemyMode()
    {
        return EnemyMode.None;
    }

    public Vector3[] GetPath()
    {
        throw new System.NotImplementedException();
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

    public Vector3[][] initPaths()
   {
       LinkedList<Vector3[]> paths = new LinkedList<Vector3[]>();
       paths.AddFirst(new Vector3[]
        {
            new Vector3(23.16821f,SceneStats.TopEdge), 
            new Vector3(16.53627f,5f), 
            new Vector3(15.8f,4f), 
            new Vector3(0,SceneStats.TopEdge), 
        });
       paths.AddLast(new Vector3[]
        {
            new Vector3(-23.16821f,SceneStats.TopEdge), 
            new Vector3(-16.53627f,5f), 
            new Vector3(-15.8f,4f), 
            new Vector3(0,SceneStats.TopEdge), 
        });
       paths.AddLast(new Vector3[]
        {
            new Vector3(SceneStats.RightEdge,5f), 
            new Vector3(20f,SceneStats.TopEdge), 
            new Vector3(-10f,4f), 
            new Vector3(SceneStats.LeftEdge,15f)
        });
       //paths.AddLast(new Vector3[]
       // {
       //     new Vector3(SceneStats.RightEdge,SceneStats.TopEdge), 
       //     new Vector3(10f,17.51f), 
       //     new Vector3(13.15f,9.19f), 
       //     new Vector3(SceneStats.RightEdge,SceneStats.BottomEdge), 
       // });

       return paths.ToArray();
   }

    public void playSpawnSound()
    {
        _audioSource.PlayOneShot(Sound.sound.EnemyGetSpawnSound(_stats._type));
    }

    public void playDeathSound()
    {
        _audioSource.PlayOneShot(Sound.sound.EnemyGetDeathSound(_stats._type));
    }

    public void goRight()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}