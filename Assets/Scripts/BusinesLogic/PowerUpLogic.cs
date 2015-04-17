using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class PowerUpLogic : MonoBehaviour {
    //dictionaries to rally data
    public int numOfPowerUpsToPowerUp;
    Dictionary<PowerUpType, int> powerUps;
    Dictionary<PowerUpType, Action> powerUpActions;
    Dictionary<PowerUpType, GameObject> powerUpIcon;
    Dictionary<PowerUpType, GameObject> powerUpSprite;
    //GUI
    public GameObject powerUpContainer;
    public float width;
    public Text powerUPText;
    private char powerUpChar ='☺';

    //superHit
    public float superHitTime = 1f;
    private int count;
    private int originalStr;
    //icons
    public GameObject _BubblePowerUpIcon;
    public GameObject _InvincablePowerUpIcon;
    public GameObject _SuperhitPowerUpIcon;

    //prefabs
    public GameObject _BubblePowerUpPrefab;
    public GameObject _InvincablePowerUpPrefab;
    public GameObject _SuperhitPowerUpPrefab;

    //general
    TouchInterpeter touch;
    MovmentLogic movmentLogic;
    AnimationLogic animationLogic;
    GameObject character;
    PlayerStatsLogic playerStatsLogic;

    
	// Use this for initialization

	void Start () {
        //TODO: SET ASSETST
        //  _BubblePowerUpIcon = Resources.Load("stupidL") as GameObject;
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
        powerUPText = GameObject.Find("PowerUpText").GetComponent<Text>();
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
        movmentLogic = this.gameObject.GetComponent<MovmentLogic>();
        animationLogic = this.gameObject.GetComponent<AnimationLogic>();
        character = GameObject.Find("PlayerManager");
        _SuperhitPowerUpPrefab = Resources.Load("SuperHitPowerUp") as GameObject;
        BuildDictionary();
	}
	

    public void GotPowerUp(PowerUpType powerUp) 
    {
        powerUps[powerUp]++;
        clearPowerUps(powerUp);
        ReWritePowerUp(powerUp);
        if (powerUps[powerUp] >= numOfPowerUpsToPowerUp)
        {
            UsePowerUP(powerUp);
        }
        
    }

    public void UsePowerUP(PowerUpType powerUP) 
    {
        clearPowerUps();
        ReWritePowerUp(powerUP);
        powerUpActions[powerUP].Invoke();
    }

    public void clearPowerUps()
    {
        powerUps[PowerUpType.BUBBLE] = 0;
        powerUps[PowerUpType.INVINCABLE] = 0;
        powerUps[PowerUpType.SUPERHIT] = 0;
    }

    public void clearPowerUps(PowerUpType powerUP)
    {
        if (powerUP != PowerUpType.BUBBLE)
        {
            powerUps[PowerUpType.BUBBLE] = 0;
        }
        if (powerUP != PowerUpType.INVINCABLE)
        {
            powerUps[PowerUpType.INVINCABLE] = 0;
        }
        if (powerUP != PowerUpType.SUPERHIT)
        {
            powerUps[PowerUpType.SUPERHIT] = 0;
        }
      
    }
    private void SuperHit() 
    {
		playerStatsLogic.powerUpModeActive = PowerUpType.SUPERHIT;
        touch.SetDisableMovment();
        movmentLogic.ResetRotation();
        animationLogic.SetDashing();
        LeanTween.cancel(character.gameObject,true);
        var playerPosition = character.transform;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Time.timeScale = 0.5f;
        originalStr = playerStatsLogic.Strength;
        playerStatsLogic.Strength = 100;
        int count = 0;

        foreach(var enemy in enemies) 
        {
            if(insideScreen(enemy.transform)) {
                var dbt = character.AddComponent<TimedAction>();
                dbt.doByTime(new TimeActionModel
                {
                    durationTime = superHitTime * count,
                    stopingFunc = punchEnemies,
                    subject = character,
                    collidedWith = enemy,
                    fixedTimeStart = Time.fixedTime
                });
				count++;
			}
        }
		if (count == 0) 
		{
			finishedSuperHit();
		}
    }
    void punchEnemies(SuperHitModel model)
    {
        Vector2 target = model.enemy.transform.position;
        Vector2 vecBetween = target - (Vector2)model.character.transform.position;
        movmentLogic.RotateToDash(vecBetween);
        animationLogic.OnMoveSetDirection(new moveAnimationModel { direction = vecBetween.normalized });
        LeanTween.move(model.character, (Vector2)target, 0.5f).setEase(LeanTweenType.punch).setIgnoreTimeScale(true).setOnComplete(() =>
        {
            finishedSuperHit();
        });

    }
    public void finishedSuperHit()
    {
        count--;
        if(count > 0) 
        {
            return;
        }

        playerStatsLogic.Strength = originalStr;
        movmentLogic.ResetRotation();
        movmentLogic.fallDown();
        touch.UnsetDisableMovment();
        animationLogic.UnSetDashing();
        Time.timeScale = 1f;
		playerStatsLogic.powerUpModeActive = PowerUpType.NONE;
    }
    //dict to know what action to use
    private void BuildDictionary()
    {
        powerUpActions = new Dictionary<PowerUpType, Action> 	
		{ 
			{ PowerUpType.SUPERHIT ,SuperHit } 

		};
        powerUps = new Dictionary<PowerUpType, int> 	
        {
           {PowerUpType.BUBBLE, 0},
           {PowerUpType.INVINCABLE, 0},
           {PowerUpType.SUPERHIT, 0}
         };

        powerUpIcon = new Dictionary<PowerUpType, GameObject> 	
        {
           {PowerUpType.BUBBLE, _BubblePowerUpIcon},
           {PowerUpType.INVINCABLE, _InvincablePowerUpIcon},
           {PowerUpType.SUPERHIT, _SuperhitPowerUpIcon}
         };
        powerUpSprite = new Dictionary<PowerUpType, GameObject> 	
        {
           {PowerUpType.BUBBLE, _BubblePowerUpPrefab},
           {PowerUpType.INVINCABLE, _InvincablePowerUpPrefab},
           {PowerUpType.SUPERHIT, _SuperhitPowerUpPrefab}
         };
    }
    public void ReWritePowerUp(PowerUpType powerUP)
    {
        var num = powerUps[powerUP];
        //UNCOMMENT WHEN ASSETS READY
        //var icons = gameObject.GetComponentsInChildren<Transform>();
        //foreach(var i in icons) {
        //    Destroy(i);
        //}

        if (num == 0) {
            powerUPText.text = "";
            return;
        }

        //var icon = powerUpIcon[powerUP];
        for (int i = 0; i < num; i++)
        {
            powerUPText.text += powerUpChar;
            //UNCOMMENT WHEN ASSETS READY
            //var vec = powerUpContainer.transform.position;
            //vec.x += i * width;
            //var sprite = Instantiate(icon, vec, Quaternion.identity) as GameObject;
        }
    }

    internal void generatePowerUp()
    {
        //TODO: select randomly a powerUP
        var pref = powerUpSprite[PowerUpType.SUPERHIT];
        var sprite = Instantiate(pref, new Vector3(-14.67f, 2.36f, 0f), Quaternion.identity) as GameObject;
        sprite.GetComponent<PowerUpTrigger>().Set();
    }

    private bool insideScreen(Transform t){
		var dist = (t.position - Camera.main.transform.position).z;
		var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).x; 
		var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1,0,dist)).x;
		var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0,1,dist)).y; 
		var buttomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0,0,dist)).y; 
		// teleport me to the other side of the screen when I reach the edge
		if (t.position.x > rightBorder) {
            return false;
		}
		if (t.position.x < leftBorder) {
            return false;
		}
		// teleport me to the top or buttom
		if (t.position.y > topBorder) {
            return false;
		}
		if (t.position.y < buttomBorder) {
            return false;
		}
        return true;
	}
}
