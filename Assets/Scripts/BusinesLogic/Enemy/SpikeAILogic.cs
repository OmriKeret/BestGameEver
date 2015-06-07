using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpikeAILogic : MonoBehaviour, IEnemy {

    // Animation 
    Animator _animation;

    AEnemyStats _stats;
    Rigidbody2D _rigidbody;
    GameObject _leftBodyPartResouce, _rightBodyPartResouce;
    GameObject _leftBodyPart, _rightBodyPart;
    GameObject _blood;
    public float timeToFinishPath = 15f;
    public float minTimeForPath = 4f;
    public float maxTimeForPath = 30f;
    private SpriteRenderer _spriteRenderer;
    AudioSource _audioSource;
    private int stateCounter = 0;
    public int normalTime = 90;
    public int spikeTime = 150;
    private Dictionary<EnemyLocation, Vector3[]> _pathMap;
    private StupidPaths _allVectorPaths;

    // blood splash data
    private EnemyGeneralAnimationLogic _generalAnimationLogic;

    

    // Use this for initialization
    // Use this for initialization
    void Awake()
    {
        _animation = this.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _stats = GetComponent<AEnemyStats>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _leftBodyPartResouce = Resources.Load("spikeL") as GameObject;
        _rightBodyPartResouce = Resources.Load("spikeR") as GameObject;
        _blood = Resources.Load("BloodSplash") as GameObject;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        _allVectorPaths = new StupidPaths();
        initPaths();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _generalAnimationLogic = GameObject.Find("Logic").GetComponent<EnemyGeneralAnimationLogic>();
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


    void FixedUpdate()
    {
        //stateCounter++;
        //if (stateCounter == normalTime)
        //{
        //    normalMode();
        //}
        //else if (stateCounter == spikeTime)
        //{
        //    spikeMode();
        //    stateCounter = 0;
        //}
        
    }

    //private void spikeMode()
    //{
    //    _stats._mode = EnemyMode.Attack;
    //    _spriteRenderer.sprite = _stats.GetCurrentAnimation();
    //}

    //private void normalMode()
    //{
    //    _stats._mode = EnemyMode.None;
    //    _spriteRenderer.sprite = _stats.GetCurrentAnimation();
    //}

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

    //TODO: Get the location from the game manager
    Vector2 IEnemy.FindPlayerLocation()
    {
        return new Vector2(0, 0);
    }



    public void StartOrderPath(int i_speed, EnemyLocation i_Location)
    {
        Vector3[] path;
        if (!_pathMap.TryGetValue(i_Location, out path))
        {

        }

        //selectOrderPath(out path, i_PathNumber);

        LeanTween.move(this.gameObject, path, calculateTime(i_speed)).setEase(LeanTweenType.linear).setOnComplete(() =>
        {
            FinishedMoving();
        });

        //Goto gameObj logic => animationLogic =>isDashing
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

    public void initPaths()
    {
        _pathMap = new Dictionary<EnemyLocation, Vector3[]>();
        _pathMap.Add(EnemyLocation.TopLeft, _allVectorPaths.topLeft);
        _pathMap.Add(EnemyLocation.TopRight, _allVectorPaths.topRight);
        _pathMap.Add(EnemyLocation.MidRight, _allVectorPaths.midRight);
        _pathMap.Add(EnemyLocation.MidLeft, _allVectorPaths.midLeft);
        _pathMap.Add(EnemyLocation.TopMid, _allVectorPaths.topMid);
        _pathMap.Add(EnemyLocation.BottomLeft, _allVectorPaths.bottomLeft);
        _pathMap.Add(EnemyLocation.BottomRight, _allVectorPaths.bottomRight);

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

    // splash blood
    public void hit(int combo, Vector2 dir)
    {
        int minNum = _generalAnimationLogic.minEmissioNum;
        int maxNum = _generalAnimationLogic.maxEmission;
        if (combo != 0)
        {
            minNum = minNum * combo;
            maxNum = maxNum * combo;
        }
        dir = dir * _generalAnimationLogic.magnitude;
        var bloodObject = Instantiate(_blood, this.transform.position, Quaternion.identity) as GameObject;
        var bloodEmiter = bloodObject.GetComponent<EllipsoidParticleEmitter>();
        bloodEmiter.minEmission = minNum;
        bloodEmiter.maxEmission = maxNum;
        bloodEmiter.localVelocity = new Vector3(dir.x, dir.y, 0);
    }

    public void enemyDie(int combo, Vector2 dir)
    {
        GetComponent<Collider2D>().enabled = false;
        _animation.SetTrigger("Die");
        int minNum = _generalAnimationLogic.minEmissioNum;
        int maxNum = _generalAnimationLogic.maxEmission;
        if (combo != 0)
        {
            minNum = minNum * combo;
            maxNum = maxNum * combo;
        }
        dir = dir * _generalAnimationLogic.magnitude;
        var bloodObject = Instantiate(_blood, this.transform.position, Quaternion.identity) as GameObject;
        var bloodEmiter = bloodObject.GetComponent<EllipsoidParticleEmitter>();
        bloodEmiter.minEmission = minNum;
        bloodEmiter.maxEmission = maxNum;
        bloodEmiter.localVelocity = new Vector3(dir.x, dir.y, 0);
    }

    // Animation driven
    public void spikesOut()
    {
        _stats._mode = EnemyMode.Attack;
    }

    public void spikesIn()
    {
        _stats._mode = EnemyMode.None;
    }
    public EnemyType getEnemyType()
    {
        return _stats._type;
    }
}
