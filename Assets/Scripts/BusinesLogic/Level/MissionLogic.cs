using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissionLogic : MonoBehaviour {

    MissionModel[] missions;
    Dictionary<EnemyType, int> enemyKills;
    Dictionary<PowerUpType, int> powerUpstaken;
  public InternalMissionModel[] MissionsToggleAndText;
	// Use this for initialization
	void Awake () {
        enemyKills = new Dictionary<EnemyType, int>	{ 
			{ EnemyType.Stupid, 0 },
            { EnemyType.General, 0 }
		};
        powerUpstaken = new Dictionary<PowerUpType, int>	{ 
			{ PowerUpType.SUPERHIT, 0 }
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
           MissionsToggleAndText[i].missionToggle.isOn = missions[i].isFinished;

           if (missions[i].type == MissionType.killTypeOfEnemy)
           {
               enemyKills[missions[i].enemyType] = missions[i].currentNumberAchived;
           }
           if (missions[i].type == MissionType.takePowerUp)
           {
               powerUpstaken[missions[i].powerUpType] = missions[i].currentNumberAchived;
           }
       }
    }

   public void addPowerUp(PowerUpType powerUp)
   {
       if (!powerUpstaken.ContainsKey(powerUp))
       {
           return;
       }
       powerUpstaken[powerUp]++;
       for (int i = 0; i < missions.Length; i++)
       {
           if (missions[i].type == MissionType.takePowerUp && missions[i].powerUpType == powerUp)
           {
               if (missions[i].type == MissionType.takePowerUp && missions[i].powerUpType == powerUp && missions[i].numberToAchive <= powerUpstaken[powerUp])
               {
                   finishedMission(i);
               }
           }
       }
   }

   public void addKill(EnemyType enemy)
    {
        enemyKills[EnemyType.General]++;
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

    public void updateMissionProggress()
    {

    }
}
