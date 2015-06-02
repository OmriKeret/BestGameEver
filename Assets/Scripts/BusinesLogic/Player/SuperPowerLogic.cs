using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuperPowerLogic : MonoBehaviour {

    

    // SuperHit
    public float superHitTime = 1f;
    private int count;
    private int originalStr;
    int comboToAchiveToSuperHit = 8;
    int maxComboReached;
    bool canSuperPower;

    // General
    TouchInterpeter touch;
    MovmentLogic movmentLogic;
    AnimationLogic animationLogic;
    GameObject character;
    PlayerStatsLogic playerStatsLogic;
    MissionLogic missionLogic;
    SoundLogic soundLogic;

	// Use this for initialization
	void Start () {
        soundLogic = this.gameObject.GetComponent<SoundLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
        movmentLogic = this.gameObject.GetComponent<MovmentLogic>();
        animationLogic = this.gameObject.GetComponent<AnimationLogic>();
        character = GameObject.Find("PlayerManager");
	}

    public void doSuperHit() {
        Debug.Log("called super hit");
        if (canSuperPower) {
            canSuperPower = false; 
            maxComboReached = 0;
            changeState(0);
            SuperHit();
        }
    }

    public void changeState(int combo)
    {
        // if there is a need to update
        if (maxComboReached < combo)
        {
            // Update reached combo and animate
            maxComboReached = combo;
            if (maxComboReached >= comboToAchiveToSuperHit)
            {
                maxComboReached = comboToAchiveToSuperHit;
                canSuperPower = true;
            }

            //TODO: animation            
        }

    }

    private void SuperHit()
    {
        // Debug.Log("doing super hit");
        playerStatsLogic.powerUpModeActive = PowerUpType.SUPERHIT;
        touch.SetDisableMovment();
        movmentLogic.ResetRotation();
        animationLogic.SetDashing();
        LeanTween.cancel(character.gameObject, true);
        // var playerPosition = character.transform;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        LeanTween.pauseAll();
        Time.timeScale = 0f;
        originalStr = playerStatsLogic.Strength;
        playerStatsLogic.Strength = 100;

        Stack<GameObject> enemiesObject = new Stack<GameObject>();
        foreach (var enemy in enemies)
        {
            if (insideScreen(enemy.transform))
            {
                enemiesObject.Push(enemy);
                var enemyCollider = enemy.GetComponent<Collider2D>();
                enemyCollider.enabled = false;
            }
        }
        punchEnemies(enemiesObject);
        if (enemiesObject.Count == 0)
        {
            finishedSuperHit();
        }
    }
    void punchEnemies(Stack<GameObject> enemyStack)
    {
        if (enemyStack.Count == 0)
        {
            return;
        }
        var enemy = enemyStack.Pop();
        //   var enemyCollider = enemy.GetComponent<Collider2D>();

        var enemyController = enemy.GetComponent<AIController>();
        var playerController = character.GetComponent<CollisionController>();

        //set collision between character and enemy manually
        playerController.OnCollisionEnter2DManual(enemy);
        enemyController.OnCollisionEnter2DManual(character);
        soundLogic.playSliceSound();
        Vector2 target = enemy.transform.position;
        Vector2 vecBetween = target - (Vector2)character.transform.position;
        movmentLogic.RotateToDash(vecBetween);
        animationLogic.OnMoveSetDirection(new moveAnimationModel { direction = vecBetween.normalized });
        LeanTween.move(character, (Vector2)target, superHitTime).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInOutElastic).setOnComplete(() =>
        {
            punchRecursionLogic(enemyStack);
        });


    }
    void punchRecursionLogic(Stack<GameObject> enemyStack)
    {
        if (enemyStack.Count == 0)
        {
            finishedSuperHit();
            return;
        }
        else
        {
            punchEnemies(enemyStack);
        }
    }
    void punchEnemies(SuperHitModel model)
    {
        Debug.Log("play slice");
        soundLogic.playSliceSound();
        Vector2 target = model.enemy.transform.position;
        Vector2 vecBetween = target - (Vector2)model.character.transform.position;
        movmentLogic.RotateToDash(vecBetween);
        animationLogic.OnMoveSetDirection(new moveAnimationModel { direction = vecBetween.normalized });
        LeanTween.move(model.character, (Vector2)target, superHitTime).setEase(LeanTweenType.punch).setIgnoreTimeScale(true).setOnComplete(() =>
        {

            finishedSuperHit();
        });

    }
    public void finishedSuperHit()
    {
        int i = 0;
        //return living enemies colliders
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            var collider = enemy.GetComponent<Collider2D>();
            collider.enabled = true;
            //Debug.Log("iterating on enemy " + i + "\ndead status: " + enemy.GetComponent<IEnemy>().isDead());
            //Debug.Log("character strength is: " + playerStatsLogic.Strength );
            if (enemy.GetComponent<IEnemy>().isDead())
            {
                killEnemy(enemy);

            }
            i++;

        }
        playerStatsLogic.Strength = originalStr;
        movmentLogic.ResetRotation();
        movmentLogic.fallDown();
        touch.UnsetDisableMovment();
        animationLogic.UnSetDashing();
        Time.timeScale = 1f;
        LeanTween.resumeAll();
        playerStatsLogic.powerUpModeActive = PowerUpType.NONE;
    }

    private void killEnemy(GameObject enemy)
    {
        //Debug.Log("killing enemy");
        if (LeanTween.isTweening(enemy))
        {
            LeanTween.cancel(enemy, false);
        }

        var position = enemy.transform.position;
        var enemyLogic = enemy.GetComponent<IEnemy>();
        enemyLogic.Split(position);
        enemyLogic.playDeathSound();
        enemyLogic.Death();

    }

    private bool insideScreen(Transform t)
    {
        var dist = (t.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        var buttomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        // teleport me to the other side of the screen when I reach the edge
        if (t.position.x > rightBorder)
        {
            return false;
        }
        if (t.position.x < leftBorder)
        {
            return false;
        }
        // teleport me to the top or buttom
        if (t.position.y > topBorder)
        {
            return false;
        }
        if (t.position.y < buttomBorder)
        {
            return false;
        }
        return true;
    }
    
}
