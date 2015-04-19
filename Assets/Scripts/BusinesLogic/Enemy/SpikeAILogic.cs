using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpikeAILogic : MonoBehaviour, IEnemy {

    AEnemyStats _stats;
    Rigidbody2D _rigidbody;
    GameObject _leftBodyPartResouce, _rightBodyPartResouce;
    GameObject _leftBodyPart, _rightBodyPart;
    public float timeToFinishPath = 15f;
    public float minTimeForPath = 4f;
    public float maxTimeForPath = 30f;
    private Vector3[][] _allPaths;
    private SpriteRenderer _spriteRenderer;

    private int stateCounter = 0;
    public int normalTime = 90;
    public int spikeTime = 150;
    

    // Use this for initialization
    void Awake()
    {
        _stats = GetComponent<AEnemyStats>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _leftBodyPartResouce = Resources.Load("spikeL") as GameObject;
        _rightBodyPartResouce = Resources.Load("spikeR") as GameObject;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        _allPaths = initPaths();
        _stats.initAnimation();
        _spriteRenderer = GetComponent<SpriteRenderer>();


    }

    void FixedUpdate()
    {
        stateCounter++;
        if (stateCounter == normalTime)
        {
            normalMode();
        }
        else if (stateCounter == spikeTime)
        {
            spikeMode();
            stateCounter = 0;
        }
        
    }

    private void spikeMode()
    {
        _stats._mode = EnemyMode.Attack;
        _spriteRenderer.sprite = _stats.GetCurrentAnimation();
    }

    private void normalMode()
    {
        _stats._mode = EnemyMode.None;
        _spriteRenderer.sprite = _stats.GetCurrentAnimation();
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
        return _stats._mode;
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
            new Vector3(SceneStats.RightEdge,0), 
            new Vector3(15f,23.94602f), 
            new Vector3(-15.8f,23.94602f), 
            new Vector3(SceneStats.LeftEdge,0), 
        });
        paths.AddLast(new Vector3[]
        {
            new Vector3(SceneStats.LeftEdge,0), 
            new Vector3(-14.37f,23.51f),  
            new Vector3(10f,-5.75f), 
            new Vector3(SceneStats.RightEdge,SceneStats.BottomEdge), 
        });
        paths.AddLast(new Vector3[]
        {
            new Vector3(SceneStats.LeftEdge,SceneStats.TopEdge), 
            new Vector3(5f,12.51f), 
            new Vector3(-10.15f,0.19f), 
            new Vector3(SceneStats.RightEdge,SceneStats.TopEdge), 
        });
        paths.AddLast(new Vector3[]
        {
            new Vector3(SceneStats.RightEdge,SceneStats.TopEdge), 
            new Vector3(10f,12.51f), 
            new Vector3(19.15f,0.19f), 
            new Vector3(SceneStats.LeftEdge,SceneStats.TopEdge), 
        });

        return paths.ToArray();

    }
}
