using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissionLogic : MonoBehaviour {

    MissionModel[] missions;
    Dictionary<EnemyType, int> enemyKills;
  public InternalMissionModel[] MissionsToggleAndText;
	// Use this for initialization
	void Start () {
        enemyKills = new Dictionary<EnemyType, int>	{ 
			{ EnemyType.Stupid, 0 }
		};
		MissionsToggleAndText = new InternalMissionModel[] {
			new InternalMissionModel(),
			new InternalMissionModel(),
			new InternalMissionModel()
		};
        int missionNum = 0;
		//var x = GameObject.Find("PauseMenu/Mission1/Mission1Label").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionText = GameObject.Find("PauseMenu/Mission1/Mission1Label").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionToggle = GameObject.Find("PauseMenu/Mission1").GetComponent<Toggle>();
        missionNum++;

		MissionsToggleAndText[missionNum].missionText = GameObject.Find("PauseMenu/Mission2/Mission2Label").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionToggle = GameObject.Find("PauseMenu/Mission2").GetComponent<Toggle>();
        missionNum++;

		MissionsToggleAndText[missionNum].missionText = GameObject.Find("PauseMenu/Mission3/Mission3Label").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionToggle = GameObject.Find("PauseMenu/Mission3").GetComponent<Toggle>();
	}


    //seting missions and text of missions
   public void setMissions(MissionModel[] missions)
    {
       this.missions = missions;
       for (int i = 0; i < 3; i++)
       {
           MissionsToggleAndText[i].missionText.text = missions[i].missionText;
       }
    }

   public void addKill(EnemyType enemy)
    {
        if (!enemyKills.ContainsKey(enemy))
        {
            return;
        }
        enemyKills[enemy]++;
        for (int i = 0; i < missions.Length; i++ )
        {
            if (missions[i].type == MissionType.killTypeOfEnemy && missions[i].numberToAchive <= enemyKills[enemy])
            {
                finishedMission(i);
            }
        }
    }

   public void gotScoreOf(int score)
   {

       for (int i = 0; i < missions.Length; i++)
       {
           if (missions[i].type == MissionType.getScoreOf && missions[i].numberToAchive <= score)
           {
               finishedMission(i);
           }
       }
   }

    private void finishedMission(int missionNum)
    {
        //TODO:playsound 

        MissionsToggleAndText[missionNum].missionToggle.isOn = true;




    }
}
