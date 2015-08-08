using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DeathLogic : MonoBehaviour {
    public float timeToOpenDeathMenu = 1f;
    MovmentLogic movmentLogic;
    PlayerStatsLogic playerStatsLogic;
    MissionLogic missionLogic;
    ScoreLogic scoreLogic;
    TouchInterpeter touch;
    SoundLogic soundLogic;
    CurrencyLogic currencyLogic;

    AnimationLogic animationLogic;
    Text deathScore;
    GameObject losePanel;
    Vector3 OrigPos;
    Vector3 EndPos;
    public float delayToMoveDeathPanelUp = 0.4f;
    public float timeToEnterMission = 0.2f;
    public float delayBetweenMissionRemovalToEnterance = 0.2f;
    public float timeToRemoveMission = 0.2f;
    float origMissionTextX;
    float EndMissionTextX;
    public Text missionTitle;
    public Text missionTitleNew;
    public InternalMissionModel[] missionsToggleAndText;
    public InternalMissionModel[] deathMissionsToggleAndText;
    public InternalMissionModel[] deathMissionsToggleAndTextNew;
    //pause menu
    Button pauseBtn;
    Button superPower;
    //score
    private int scoreBegin;
    private int scoreEnd;
    private int currentHighScore;
    private bool changeScoreText = false;
    private bool newHighScore;
    private float finalAchivedScore;
    private int bonus;
    private Animator HighScore;
    Animator bonusAnimator;
    Text bonusText;
    private float timeStartedToChangeScore;
    private float TimeToScoreUp = 3f;
    private float displayedScore;
    // currency
    public float timeToWaitFromScoreLastUpdateToCurrencyChange = 1f;
    private float lastScoreUpdate;
    int addedCurrency;
    int currencyDiviser;
    bool shouldWriteCurrency;
    private Text currencyText;
    float currentDisplayedCurrency;
    private float TimeToCashIn = 2f;
	private float TimeToCashInChange;
    private float cashInDeviser;
    private float deviserRatio;
    private float timeSinceStartedCurrency;
    private float currentScore;
    private GameObject particles;

    Animator darkScreen;
    
    // New mission system
    EndscreenGuiMain endScreen;
	// Initialization
    void Start()
    {
        endScreen = this.GetComponentInChildren<EndscreenGuiMain>();
        animationLogic = this.gameObject.GetComponent<AnimationLogic>();
        HighScore = null;
        bonusAnimator = null;
        pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
        soundLogic = this.gameObject.GetComponent<SoundLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        losePanel = GameObject.Find("LosePanel");
        OrigPos = losePanel.transform.position;// new Vector3(0, 30, 0);
        EndPos = new Vector3(OrigPos.x, 35, OrigPos.z);
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
        movmentLogic = this.gameObject.GetComponent<MovmentLogic>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        scoreLogic = this.gameObject.GetComponent<ScoreLogic>();
        currencyLogic = this.gameObject.GetComponent<CurrencyLogic>();
        darkScreen = GameObject.Find("Canvas/DarkScreen").GetComponent<Animator>();
        missionsToggleAndText = new InternalMissionModel[] {
			new InternalMissionModel(),
			new InternalMissionModel(),
			new InternalMissionModel()
		};
        deathMissionsToggleAndText = new InternalMissionModel[] {
			new InternalMissionModel(),
			new InternalMissionModel(),
			new InternalMissionModel()
		};
        deathMissionsToggleAndTextNew = new InternalMissionModel[] {
			new InternalMissionModel(),
			new InternalMissionModel(),
			new InternalMissionModel()
		};
        int missionNum = 0;
        //deathMissionsToggleAndText[missionNum].missionText = GameObject.Find("LosePanel/LoseMission1/LoseMissionText1").GetComponent<Text>();
        //deathMissionsToggleAndText[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission1").GetComponent<Toggle>();
        //deathMissionsToggleAndTextNew[missionNum].missionText = GameObject.Find("LosePanel/LoseMission1New/LoseMissionText1").GetComponent<Text>();
        //deathMissionsToggleAndTextNew[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission1New").GetComponent<Toggle>();
        //missionNum++;

        //deathMissionsToggleAndText[missionNum].missionText = GameObject.Find("LosePanel/LoseMission2/LoseMissionText2").GetComponent<Text>();
        //deathMissionsToggleAndText[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission2").GetComponent<Toggle>();
        //deathMissionsToggleAndTextNew[missionNum].missionText = GameObject.Find("LosePanel/LoseMission2New/LoseMissionText2").GetComponent<Text>();
        //deathMissionsToggleAndTextNew[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission2New").GetComponent<Toggle>();
        //missionNum++;

        //deathMissionsToggleAndText[missionNum].missionText = GameObject.Find("LosePanel/LoseMission3/LoseMissionText3").GetComponent<Text>();
        //deathMissionsToggleAndText[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission3").GetComponent<Toggle>();
        //deathMissionsToggleAndTextNew[missionNum].missionText = GameObject.Find("LosePanel/LoseMission3New/LoseMissionText3").GetComponent<Text>();
        //deathMissionsToggleAndTextNew[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission3New").GetComponent<Toggle>();

        //origMissionTextX = deathMissionsToggleAndText[missionNum].missionToggle.transform.position.x;
        //EndMissionTextX = origMissionTextX - 200;
		deathScore = GameObject.Find("LosePanel/LoseScore").GetComponent<Text>();
        currencyText = GameObject.Find("LosePanel/LosePJ").GetComponent<Text>();
        timeSinceStartedCurrency = -1;
		TimeToCashInChange = TimeToCashIn ;
        particles = GameObject.Find("LosePanel/LosePJ/Particles");
        timeStartedToChangeScore = -1;
        superPower = GameObject.Find("Canvas/SuperHit").GetComponent<Button>();
       
    }

    void Update()
    {
        if (changeScoreText && displayedScore < scoreEnd)
        {
            timeStartedToChangeScore = timeStartedToChangeScore == -1 ? Time.realtimeSinceStartup : timeStartedToChangeScore;
            //TODO: SOUND - change score by mission sound

            float deltaTime = Time.realtimeSinceStartup - timeStartedToChangeScore;
            calculateAndSetTimeToScore(deltaTime);
            if (displayedScore >= scoreEnd)
            {
                changeScoreText = false;
                if (newHighScore)
                {
                    HighScore.SetTrigger("new");
                }
                else
                {
                    shouldWriteCurrency = true;
                    lastScoreUpdate = Time.realtimeSinceStartup;
                }
            }
            var scoreTxt = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                         "{0:0,0}", displayedScore);
            deathScore.text = string.Format("{0}", scoreTxt);

        }
        else if (shouldWriteCurrency && (Time.realtimeSinceStartup - lastScoreUpdate > timeToWaitFromScoreLastUpdateToCurrencyChange))
        {
            turnOnParticles();
            // If just started chagning, store the time started
            timeSinceStartedCurrency = timeSinceStartedCurrency == -1 ? Time.realtimeSinceStartup : timeSinceStartedCurrency;
            float deltaTime = Time.realtimeSinceStartup - timeSinceStartedCurrency;
            calculateAndSetTimeToCashIn(deltaTime);
            
            if (finalAchivedScore - cashInDeviser >= 0)
            {
                // Sound
                if (deltaTime > TimeToCashIn - 1.00f)
                {
                    soundLogic.playchangeMoneyEndSound();
                }
                else
                {
                    soundLogic.playChangeMoneySound();
                }
                
                // Currenct score is updated in calculateAndSetTimeToCashIn()
                var scoreTxt = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "{0:0,0}", (int)currentScore);
                deathScore.text = string.Format("{0}", scoreTxt);

                //updating currency text
                currentDisplayedCurrency += deviserRatio;
                currencyText.text = string.Format("PJ:{0}", (int)currentDisplayedCurrency);
                if (currentScore < 1)
                {
					finalAchivedScore = 0;
				}
            }
            else
            {
                turnOffParticles();
                //showing score as 0
                var scoreTxt = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                    "{0:0,0}", 0);
                deathScore.text = string.Format("{0}", scoreTxt);
                shouldWriteCurrency = false;
            }
        }
    }

    public void startChangeScoreWithCurrency()
    {
        shouldWriteCurrency = true;
        lastScoreUpdate = Time.realtimeSinceStartup;
    }
    public void DeathByFall()
    {
        if (playerStatsLogic.removeHp(1))
        {
            
            DeathScreen(0f);
            return;
        }
        movmentLogic.MoveOnFallDeath();
		playerStatsLogic.resetDash ();
    }


    //the death screen
    private void DeathScreen(float delay)
    {
        endScreen.initiateDeathScreen();
        //pauseBtn.interactable = false;
        //superPower.interactable = false;
        //touch.SetDisableMovment();
        //GetMissionData();
        //GetScoreData();
        //MoveGUI(delay);
        //saveScoreAndMissions();
    }

    private void saveScore()
    {
        scoreLogic.saveScoreData();
    }

    public void Reset()
    {
		Time.timeScale = 1;
        AutoFade.LoadLevel(Application.loadedLevel, 2, 1, Color.white);
    }

    //brings death screen up
    private void MoveGUI(float delay)
    {
        darkScreen.SetBool("darkScreen", true);
        touch.SetDisableMovment();
        pauseBtn.enabled = false;
        LeanTween.move (losePanel, EndPos, timeToOpenDeathMenu).setDelay (delay).setIgnoreTimeScale (true).setOnComplete (() => 
		{
			//update mission progress
			missionLogic.updateMissionProggressEndOfGame ();
			Time.timeScale = 1;

		});   
    }

    public void updateCurrencyGui()
    {
        
        //update highscore
        if (currentHighScore < scoreBegin || currentHighScore < scoreEnd) {
			newHighScore = true;
			//TODO: do something with this information (show "new high score") 
           
		}
            if (scoreBegin < scoreEnd)
            {
                finalAchivedScore = scoreEnd;
            }
            else
            {
                finalAchivedScore = scoreBegin;
            }
            
            lastScoreUpdate = Time.realtimeSinceStartup;

            // currency var
            currentScore = finalAchivedScore;
            updatePJcurrency(finalAchivedScore);
       
        
    }

    private void updatePJcurrency(float score)
    {
        addedCurrency = currencyLogic.updateCurrencyByScore((int)score);
        currencyDiviser = currencyLogic.deviderToScore;
        
        //calculateAndSetTimeToCashIn();
        if (!changeScoreText)
        {
            if (score > 0)
            {
                shouldWriteCurrency = true;
            }

        }
        
    }

    private void calculateAndSetTimeToCashIn(float deltaTime)
    {
        var factor = Easing.easeInOut(deltaTime, TimeToCashIn);
        float currentTemp = finalAchivedScore - (finalAchivedScore * factor);
        cashInDeviser = currentScore - currentTemp;
        currentScore = currentTemp;
        deviserRatio = (cashInDeviser / currencyDiviser);
    }
    
    private void calculateAndSetTimeToScore(float deltaTime)
    {
		float currentTemp = 0f;
		var factor = Easing.easeInOut(deltaTime, TimeToScoreUp);
        displayedScore = scoreBegin + (scoreEnd - scoreBegin) * factor;
    }
    private void turnOnParticles()
    {
        particles.SetActive(true);
    }
    private void turnOffParticles()
    {
        particles.GetComponent<ParticleSystem>().Stop();
    }

    private void GetScoreData()
    {
        var scoreTxt = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                          "{0:0,0}", scoreLogic.score);
        deathScore.text = string.Format("{0}", scoreTxt);
        scoreBegin = scoreLogic.score;
        currentHighScore = scoreLogic.getHighScore();
    }

    //geting the mission data from the mission logic
    private void GetMissionData()
    {
        int missionNum = 0;
        missionsToggleAndText[missionNum].missionText = missionLogic.MissionsToggleAndText[missionNum].missionText;
        missionsToggleAndText[missionNum].missionToggle = missionLogic.MissionsToggleAndText[missionNum].missionToggle;
        missionNum++;

        missionsToggleAndText[missionNum].missionText = missionLogic.MissionsToggleAndText[missionNum].missionText;
        missionsToggleAndText[missionNum].missionToggle = missionLogic.MissionsToggleAndText[missionNum].missionToggle;
        missionNum++;

        missionsToggleAndText[missionNum].missionText = missionLogic.MissionsToggleAndText[missionNum].missionText;
        missionsToggleAndText[missionNum].missionToggle = missionLogic.MissionsToggleAndText[missionNum].missionToggle;
     
        for (int i = 0; i < missionsToggleAndText.Length; i++)
        {
            deathMissionsToggleAndText[i].missionText.text = missionsToggleAndText[i].missionText.text;
            deathMissionsToggleAndText[i].missionToggle.isOn = missionsToggleAndText[i].missionToggle.isOn;
        }

    }

    //called if finished all missions
    internal void switchMissionsOnComplete(MissionModel[] missionModel)
    {
        updateNewMissions(missionModel);
        multiplyScore(missionLogic.getTier());
        moveOldMissionsAndReplaceWithNew();
       // saveScoreAndMissions();
    }

    private void saveScoreAndMissions()
    {
        saveScore();
        missionLogic.saveMissionData();
    }

    private void multiplyScore(int tier)
    {

       scoreBegin = scoreLogic.score;
       bonus = 0;
       scoreEnd = scoreBegin + bonus;

       
    }



    private void moveOldMissionsAndReplaceWithNew()
    {
        //start animation of bonus. when finishes it will call  moveOldMission(i); with i = 0
        bonusText.text = string.Format("FINISHED ALL MISSION\n +{0}", bonus);
        bonusAnimator.SetTrigger("finished");
        //int i = 0;
        //moveOldMission(i);

    }
    private void moveNewMission(int i)
    {
    
        //TODO: SOUND - move new mission sound
        LeanTween.moveX(deathMissionsToggleAndTextNew[i].missionToggle.gameObject, origMissionTextX, timeToEnterMission).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInOutElastic).setDelay(delayBetweenMissionRemovalToEnterance).setOnComplete(
              () =>
              {
                  i++;
                  if (i < 3)
                  {
                      moveOldMission(i);
                  }
                  else
                  {
                      // no more updates to score
					  changeScoreText = true;
                      newHighScore = false;
                      updateCurrencyGui();
                      saveScoreAndMissions();
                      
                  }

              }
          );
    }

    public void moveOldMission(int i)
    {
    
        //TODO: SOUND - move old mission sound
        LeanTween.moveX(deathMissionsToggleAndText[i].missionToggle.gameObject, EndMissionTextX, timeToRemoveMission).setDelay(delayBetweenMissionRemovalToEnterance).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInOutElastic).setOnComplete(
                () =>
                {
                    moveNewMission(i);
                }
            );
    }

    private void updateNewMissions(MissionModel[] missionModel)
    {
        for(int missionNum = 0; missionNum < 3; missionNum++)
        {
            deathMissionsToggleAndTextNew[missionNum].missionText.text = missionModel[missionNum].missionText;
            deathMissionsToggleAndTextNew[missionNum].missionToggle.isOn = false; ;
        }
    }



    internal void playerDie(int sign)
    {
        GameObject.Find("PlayerManager").GetComponent<Collider2D>().enabled = false;
        touch.SetDisableMovment();
        animationLogic.playerDie();
        pauseBtn.interactable = false;
        superPower.interactable = false;
        soundLogic.playDeathSound();
        movmentLogic.movePlayerDie(sign);
        DeathScreen(delayToMoveDeathPanelUp);
    }
}
