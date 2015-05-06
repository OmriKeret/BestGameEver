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
    AudioSource _audioSource;
    private int stateCounter = 0;
    public int normalTime = 90;
    public int spikeTime = 150;
    

    // Use this for initialization
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _stats = GetComponent<AEnemyStats>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _leftBodyPartResouce = Resources.Load("spikeL") as GameObject;
        _rightBodyPartResouce = Resources.Load("spikeR") as GameObject;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        initPaths();
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

    //TODO: change this to map
    public void StartOrderPath(int i_speed, EnemyLocation i_Location)
    {
        Vector3[] path = null;
        //selectOrderPath(out path, i_WaveNumber);

        LeanTween.move(this.gameObject, path, calculateTime(i_speed)).setEase(LeanTweenType.linear).setOnComplete(() =>
        {
            FinishedMoving();
        });
    }

    public void selectOrderPath(out Vector3[] i_path, int i_WaveNumber)
    {
        i_path = _allPaths[i_WaveNumber];
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
        if (pathNumber==1||pathNumber==2)
        {
       //     Debug.Log("right");
            goRight();
        }
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

    //TODO: set to map
    public void initPaths()
    {

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
