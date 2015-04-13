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
    public InternalMissionModel[] missionsToggleAndText;
    public InternalMissionModel[] deathMissionsToggleAndText;
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
        int missionNum = 0;
        deathMissionsToggleAndText[missionNum].missionText = GameObject.Find("LosePanel/LoseMission1/LoseMissionText1").GetComponent<Text>();
        deathMissionsToggleAndText[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission1").GetComponent<Toggle>();
        missionNum++;

        deathMissionsToggleAndText[missionNum].missionText = GameObject.Find("LosePanel/LoseMission2/LoseMissionText2").GetComponent<Text>();
        deathMissionsToggleAndText[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission2").GetComponent<Toggle>();
        missionNum++;

        deathMissionsToggleAndText[missionNum].missionText = GameObject.Find("LosePanel/LoseMission3/LoseMissionText3").GetComponent<Text>();
        deathMissionsToggleAndText[missionNum].missionToggle = GameObject.Find("LosePanel/LoseMission3").GetComponent<Toggle>();

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
        Time.timeScale = 0;
        iTween.MoveTo(losePanel, iTween.Hash(
           "name", StaticVars.ITWEEN_MENU_PAUSE,
           "time", timeToOpenDeathMenu,
           "position", EndPos,
           "ignoretimescale", true
           ));
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
}
