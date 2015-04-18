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
    Text deathScore;
    GameObject losePanel;
    Vector3 OrigPos;
    Vector3 EndPos;
    public float timeToRemoveMission = 0.2f;
    float origMissionTextX;
    float EndMissionTextX;
    public Text missionTitle;
    public Text missionTitleNew;
    public InternalMissionModel[] missionsToggleAndText;
    public InternalMissionModel[] deathMissionsToggleAndText;
    public InternalMissionModel[] deathMissionsToggleAndTextNew;

	// Use this for initialization
    void Start()
    {
        
        losePanel = GameObject.Find("LosePanel");
        OrigPos = new Vector3(0, 30, 0);
        EndPos = new Vector3(0, 6, 0);

        movmentLogic = this.gameObject.GetComponent<MovmentLogic>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        scoreLogic = this.gameObject.GetComponent<ScoreLogic>();
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

        origMissionTextX = deathMissionsToggleAndText[missionNum].missionToggle.transform.position.x;
        EndMissionTextX = origMissionTextX - 30;
		deathScore = GameObject.Find("LosePanel/LoseScore").GetComponent<Text>();
    }

    public void DeathByFall()
    {
        if (playerStatsLogic.removeHp(1))
        {
            DeathScreen();
        }
        movmentLogic.MoveOnFallDeath();
		playerStatsLogic.resetDash ();
    }

    private void DeathScreen()
    {
        GetMissionData();
        GetScoreData();
        MoveGUI();
    }

    public void Reset()
    {
		Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);
    }

    private void MoveGUI()
    {
        LeanTween.move(losePanel, EndPos, timeToOpenDeathMenu).setIgnoreTimeScale(true);
        Time.timeScale = 0;
    }

    private void GetScoreData()
    {
        deathScore.text = string.Format("SCORE: {0}",scoreLogic.score);
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
        int i = 0;
        foreach(var mission in missionsToggleAndText) 
        {
            if (mission.missionToggle.isOn)
            {
              //  deathMissionsToggleAndText[i].missionToggle.gameObject.re = true;
                deathMissionsToggleAndText[i].missionText.text = mission.missionText.text;
                deathMissionsToggleAndText[i].missionToggle.isOn = true;
                i++;
            }
        }
        foreach (var mission in missionsToggleAndText)
        {
            if (mission.missionToggle.isOn == false)
            {
                deathMissionsToggleAndText[i].missionToggle.gameObject.SetActive(false);
                i++;
            }
        }
    }

    internal void switchMissionsOnComplete(MissionModel[] missionModel)
    {
        updateNewMissions(missionModel);
        moveOldMissionsAndReplaceWithNew();
    }

    private void moveOldMissionsAndReplaceWithNew()
    {
        int i = 0;
        LeanTween.moveX(missionsToggleAndText[i].missionToggle.gameObject, EndMissionTextX, timeToRemoveMission).setIgnoreTimeScale(true).setOnComplete(
            () =>
                {
                    moveOldMission(i);
                }
            );
    }
    private void moveNewMission(int i)
    {
        LeanTween.moveX(deathMissionsToggleAndTextNew[i].missionToggle.gameObject, origMissionTextX, timeToRemoveMission).setIgnoreTimeScale(true).setOnComplete(
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
            LeanTween.moveX(missionsToggleAndText[i].missionToggle.gameObject, EndMissionTextX, timeToRemoveMission).setIgnoreTimeScale(true).setOnComplete(
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
}
