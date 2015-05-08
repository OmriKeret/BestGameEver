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

    //score
    private int scoreBegin;
    private int scoreEnd;
    private int currentHighScore;
    private bool changeScoreText = false;
    private bool newHighScore;
    private int finalAchivedScore;
    //currency
    int addedCurrency;
    int currencyDiviser;
    bool shouldWriteCurrency;
    private Text currencyText;
    int currentDisplayedCurrency;
	// Use this for initialization
    void Start()
    {
        soundLogic = this.gameObject.GetComponent<SoundLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        losePanel = GameObject.Find("LosePanel");
        OrigPos = new Vector3(0, 30, 0);
        EndPos = new Vector3(0, 6, 0);
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
        movmentLogic = this.gameObject.GetComponent<MovmentLogic>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        scoreLogic = this.gameObject.GetComponent<ScoreLogic>();
        currencyLogic = this.gameObject.GetComponent<CurrencyLogic>();
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
        deathMissionsToggleAndText[missionNum].missionText = GameObject.Find("LosePanel/LoseMission1/LoseMissionText1").GetComponent<Text>();
        deathMissionsToggleAndText[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission1").GetComponent<Toggle>();
        deathMissionsToggleAndTextNew[missionNum].missionText = GameObject.Find("LosePanel/LoseMission1New/LoseMissionText1").GetComponent<Text>();
        deathMissionsToggleAndTextNew[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission1New").GetComponent<Toggle>();
        missionNum++;

        deathMissionsToggleAndText[missionNum].missionText = GameObject.Find("LosePanel/LoseMission2/LoseMissionText2").GetComponent<Text>();
        deathMissionsToggleAndText[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission2").GetComponent<Toggle>();
        deathMissionsToggleAndTextNew[missionNum].missionText = GameObject.Find("LosePanel/LoseMission2New/LoseMissionText2").GetComponent<Text>();
        deathMissionsToggleAndTextNew[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission2New").GetComponent<Toggle>();
        missionNum++;

        deathMissionsToggleAndText[missionNum].missionText = GameObject.Find("LosePanel/LoseMission3/LoseMissionText3").GetComponent<Text>();
        deathMissionsToggleAndText[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission3").GetComponent<Toggle>();
        deathMissionsToggleAndTextNew[missionNum].missionText = GameObject.Find("LosePanel/LoseMission3New/LoseMissionText3").GetComponent<Text>();
        deathMissionsToggleAndTextNew[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission3New").GetComponent<Toggle>();

        origMissionTextX = deathMissionsToggleAndText[missionNum].missionToggle.transform.position.x - 1;
        EndMissionTextX = origMissionTextX - 30;
		deathScore = GameObject.Find("LosePanel/LoseScore").GetComponent<Text>();
        currencyText = GameObject.Find("LosePanel/LosePJ").GetComponent<Text>();
    }

    void Update()
    {
        if (changeScoreText && scoreBegin < scoreEnd)
        {
            var scoreTxt = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                         "{0:0,0}", scoreBegin++);
            deathScore.text = string.Format("{0}", scoreTxt);
            if (scoreBegin >= scoreEnd)
            {
                changeScoreText = false;
            }
        }
        else if (shouldWriteCurrency)
        {
            if (finalAchivedScore - currencyDiviser >= 0)
            {


                //updating score text
                finalAchivedScore -= currencyDiviser;
                var scoreTxt = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "{0:0,0}", finalAchivedScore);
                deathScore.text = string.Format("{0}", scoreTxt);

                //updating currency text
                currentDisplayedCurrency += currencyDiviser;
                currencyText.text = string.Format("PJ EARNED:{0}", currentDisplayedCurrency);
            }
            else
            {
                //showing score as 0
                var scoreTxt = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                    "{0:0,0}", 0);
                deathScore.text = string.Format("{0}", scoreTxt);
                shouldWriteCurrency = false;
            }
        }
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
        GetMissionData();
        GetScoreData();
        MoveGUI(delay);
        saveScoreAndMissions();
    }

    private void saveScore()
    {
        scoreLogic.saveScoreData();
    }

    public void Reset()
    {
		Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);
    }

    //brings death screen up
    private void MoveGUI(float delay)
    {
        LeanTween.move(losePanel, EndPos, timeToOpenDeathMenu).setDelay(delay).setIgnoreTimeScale(true).setOnComplete( () => 
            {
                //update mission progress
                missionLogic.updateMissionProggressEndOfGame();
                Time.timeScale = 0;
            }).setOnComplete( () => 
                {
                    //update highscore
                    if (currentHighScore < scoreBegin || currentHighScore < scoreEnd)
                    {
                        newHighScore = true;
                        //TODO: do something with this information (show "new high score") 
                        if (scoreBegin < scoreEnd)
                        {
                            finalAchivedScore = scoreEnd;
                            updatePJcurrency(finalAchivedScore);
                        }
                        else
                        {
                            finalAchivedScore = scoreBegin;
                            updatePJcurrency(finalAchivedScore);
                        }
                    }
                });
      
    }

    private void updatePJcurrency(int score)
    {
        addedCurrency = currencyLogic.updateCurrencyByScore(score);
        currencyDiviser = currencyLogic.deviderToScore;
        shouldWriteCurrency = true;
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
        moveOldMissionsAndReplaceWithNew();
        multiplyScore(missionLogic.getTier());
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
       scoreEnd = scoreLogic.multiplyScoreAfterFinishingMissions(tier);
       changeScoreText = true;
    }



    private void moveOldMissionsAndReplaceWithNew()
    {
        int i = 0;
        LeanTween.moveX(deathMissionsToggleAndText[i].missionToggle.gameObject, EndMissionTextX, timeToRemoveMission).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInOutElastic).setOnComplete(
            () =>
                {
                    moveOldMission(i);
                }
            );
    }
    private void moveNewMission(int i)
    {
        LeanTween.moveX(deathMissionsToggleAndTextNew[i].missionToggle.gameObject, origMissionTextX, timeToEnterMission).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInOutElastic).setDelay(delayBetweenMissionRemovalToEnterance).setOnComplete(
              () =>
              {
                  i++;
                  if (i < 3)
                  {
                      moveOldMission(i);
                  }

              }
          );
    }

    private void moveOldMission(int i)
    {
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
        soundLogic.playDeathSound();
        movmentLogic.movePlayerDie(sign);
        touch.SetDisableMovment();
        DeathScreen(delayToMoveDeathPanelUp);
    }
}
