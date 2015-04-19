using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TankAILogic : MonoBehaviour, IEnemy {


    AEnemyStats _stats;
    Rigidbody2D _rigidbody;
    GameObject _leftBodyPartResouce, _rightBodyPartResouce;
    GameObject _leftBodyPart, _rightBodyPart;
    public float timeToFinishPath = 15f;
    public float minTimeForPath = 4f;
    public float maxTimeForPath = 30f;
    private Vector3[][] _allPaths;
    private CommetLogic commet;

    // Use this for initialization
    void Awake()
    {
        _stats = GetComponent<AEnemyStats>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _leftBodyPartResouce = Resources.Load("tankL") as GameObject;
        _rightBodyPartResouce = Resources.Load("tankR") as GameObject;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        _allPaths = initPaths();
        commet = GetComponentInChildren<CommetLogic>();
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

    void IEnemy.Death()
    {
        Destroy(this.gameObject);
    }

    Vector2 IEnemy.MoveToPoint(Vector2 point)
    {
        return (new Vector2(point.x * (-1), point.y * (-1))).normalized;
    }

    //TODO: Get the location from the game manager
    Vector2 IEnemy.FindPlayerLocation()
    {
        return new Vector2(0, 0);
    }

    public void SetStats(AEnemyStats i_stats)
    {
        _stats = i_stats;
    }

    void IEnemy.MoveInDirection(Vector2 i_direction)
    {
        if (_rigidbody.velocity.magnitude < _stats.MAX_SPEED)
        {
            _rigidbody.AddForce(i_direction);
        }
    }

    void IEnemy.Split(Vector2 i_location)
    {
        _leftBodyPart = Instantiate(_leftBodyPartResouce, i_location, Quaternion.identity) as GameObject;
        if (_leftBodyPart != null)
        {
            _leftBodyPart.GetComponent<PartOfEnemyFadeOut>().FadeAndDestoryUp();
        }
        _rightBodyPart = Instantiate(_rightBodyPartResouce, i_location, Quaternion.identity) as GameObject;
        if (_rightBodyPart != null)
        {
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
            new Vector3(SceneStats.RightEdge,7), 
            new Vector3(18f,6f), 
            new Vector3(-9.9f,13.6f), 
            new Vector3(SceneStats.LeftEdge-6,7.9f), 
            new Vector3(SceneStats.LeftEdge-6,7.9f), 
            new Vector3(-25.9f,-5f),
            new Vector3(-15f,5f), 
            new Vector3(SceneStats.RightEdge,1), 
        });
        paths.AddLast(new Vector3[]
        {
            new Vector3(SceneStats.LeftEdge,7), 
            new Vector3(-18f,6f), 
            new Vector3(9.9f,13.6f), 
            new Vector3(SceneStats.RightEdge+6,7.9f), 
            new Vector3(SceneStats.RightEdge+6,7.9f), 
            new Vector3(25.9f,-5f),
            new Vector3(15f,5f), 
            new Vector3(SceneStats.LeftEdge,1), 
        });
        //paths.AddLast(new Vector3[]
        //{
        //    new Vector3(SceneStats.LeftEdge,SceneStats.TopEdge), 
        //    new Vector3(-5f,17.51f), 
        //    new Vector3(-10.15f,9.19f), 
        //    new Vector3(SceneStats.LeftEdge,SceneStats.BottomEdge), 
        //});
        //paths.AddLast(new Vector3[]
        //{
        //    new Vector3(SceneStats.RightEdge,SceneStats.TopEdge), 
        //    new Vector3(10f,17.51f), 
        //    new Vector3(13.15f,9.19f), 
        //    new Vector3(SceneStats.RightEdge,SceneStats.BottomEdge), 
        //});

        return paths.ToArray();

    }
}
