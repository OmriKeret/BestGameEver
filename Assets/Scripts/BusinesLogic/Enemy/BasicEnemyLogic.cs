using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BasicEnemyLogic : MonoBehaviour {

    protected BasicEnemyStats _stats;
    Rigidbody2D _rigidbody;
    protected GameObject _leftBodyPartResouce, _rightBodyPartResouce;
    protected GameObject _leftBodyPart, _rightBodyPart;
    protected GameObject _blood;

    // Animation 
    Animator _animation;

    public float timeToFinishPath = 15f;
    public float minTimeForPath = 4f;
    public float maxTimeForPath = 30f;
    private Dictionary<EnemyLocation, Vector3[]> _pathMap;
    protected StupidPaths _allVectorPaths;

    // blood splash data
    protected EnemyGeneralAnimationLogic _generalAnimationLogic;

    AudioSource _audioSource;
    // Use this for initialization
    protected virtual void Awake()
    {
        _animation = this.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _stats = GetComponent<BasicEnemyStats>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _blood = Resources.Load("BloodSplash") as GameObject;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        _generalAnimationLogic = GameObject.Find("Logic").GetComponent<EnemyGeneralAnimationLogic>();
        initVectorPaths();
        initPaths();
    }

    protected virtual void Start()
    {
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

    public virtual void Death()
    {
        Destroy(this.gameObject);
    }

    public void SetStats(BasicEnemyStats i_stats)
    {
        _stats = i_stats;
    }

    public void Split(Vector2 i_location)
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
    }


    public virtual EnemyMode GetEnemyMode()
    {
        return EnemyMode.None;
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

    public abstract void initVectorPaths();

    public virtual void initPaths()
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
        _animation.SetTrigger("Hit");
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

    // splash blood + death
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

    public EnemyType getEnemyType()
    {
        return _stats._type;
    }
}
