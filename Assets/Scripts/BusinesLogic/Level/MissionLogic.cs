using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissionLogic : MonoBehaviour {

  MissionModel[] missions;
  Dictionary<EnemyType, int> enemyKills;
  Dictionary<PowerUpType, int> powerUpstaken;
  public InternalMissionModel[] MissionsToggleAndText;
  int TrackTimeForMissionNumber;
  DeathLogic deathLogic; // todo: assign
  MissionStats missionStats; //todo: assign
  public Text missionTitle;

	// Use this for initialization
	void Start () {
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
        TrackTimeForMissionNumber = 0;
        int missionNum = 0;
		//var x = GameObject.Find("PauseMenu/Mission1/Mission1Label").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionText = GameObject.Find("PauseMenu/Mission1/Mission1Label").GetComponent<Text>();
        MissionsToggleAndText[missionNum].missionCount = GameObject.Find("PauseMenu/Mission1/Mission1Count").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionToggle = GameObject.Find("PauseMenu/Mission1").GetComponent<Toggle>();
        missionNum++;

		MissionsToggleAndText[missionNum].missionText = GameObject.Find("PauseMenu/Mission2/Mission2Label").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionToggle = GameObject.Find("PauseMenu/Mission2").GetComponent<Toggle>();
        MissionsToggleAndText[missionNum].missionCount = GameObject.Find("PauseMenu/Mission2/Mission2Count").GetComponent<Text>();
        missionNum++;

		MissionsToggleAndText[missionNum].missionText = GameObject.Find("PauseMenu/Mission3/Mission3Label").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionToggle = GameObject.Find("PauseMenu/Mission3").GetComponent<Toggle>();
        MissionsToggleAndText[missionNum].missionCount = GameObject.Find("PauseMenu/Mission3/Mission3Count").GetComponent<Text>();
        missionTitle = GameObject.Find("PauseMenu/MissionTitle").GetComponent<Text>();

	}

    //counting time to timed missions
    void FixedUpdate()
    {
        if (TrackTimeForMissionNumber != 0)
        {
            missions[TrackTimeForMissionNumber].currentNumberAchived = (int)Time.fixedTime;
            updateNumberAchived(TrackTimeForMissionNumber, missions[TrackTimeForMissionNumber].currentNumberAchived);
        }
    }

    //seting missions and text of missions
   public void setMissions(MissionModel[] missions,string title)
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
           if (missions[i].type == MissionType.survival)
           {  
               //if we need to track time for mission i
               this.TrackTimeForMissionNumber = i;
           }
           updateNumberAchived(i, missions[i].currentNumberAchived);
       }
       missionTitle.text = title;
    }

   //helper method to format time text
   private string formatCountTimeString(int numberToAchive, int numberAchivedAlready)
   {
       var Left = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                 "{0:0,0}", numberToAchive);
       var right = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                          "{0:0,0}", numberAchivedAlready);
       return string.Format("{0}/{1}", Left, right);
   }

   //helper method to format text
   private string formatCountString(int numberToAchive, int numberAchivedAlready)
   {
       return string.Format("{0}/{1}", numberAchivedAlready, numberToAchive);
   }

   //when reciving a powerUp update its count
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
               updateNumberAchived(i,powerUpstaken[powerUp]);
               if (missions[i].type == MissionType.takePowerUp && missions[i].powerUpType == powerUp && missions[i].numberToAchive <= powerUpstaken[powerUp])
               {
                   finishedMission(i);
               }
           }
       }
   }

    //helper method to update the number achived and write it
   private void updateNumberAchived(int missionNumber,int numberAchived)
   {
       missions[missionNumber].currentNumberAchived = numberAchived;
       if (missions[missionNumber].type == MissionType.survival)
       {
           MissionsToggleAndText[missionNumber].missionCount.text = formatCountTimeString(missions[missionNumber].numberToAchive, missions[missionNumber].currentNumberAchived);
       }
       else
       {
           MissionsToggleAndText[missionNumber].missionCount.text = formatCountString(missions[missionNumber].numberToAchive, missions[missionNumber].currentNumberAchived);
       }
   }

    //when reciving a kill update its count
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
            if (missions[i].type == MissionType.killTypeOfEnemy)
            {
                updateNumberAchived(i, enemyKills[enemy]);
                if (missions[i].numberToAchive <= enemyKills[enemy])
                {
                    finishedMission(i);
                }
            }
        }
    }

    //method to check if finished the score mission
   public void gotScoreOf(int score)
   {
       for (int i = 0; i < missions.Length; i++)
       {
           if(missions[i].type == MissionType.getScoreOf){
               updateNumberAchived(i, score);
               if (missions[i].numberToAchive <= score)
               {
                   finishedMission(i);
               }
          }
       }
   }

    private void finishedMission(int missionNum)
    {
        //TODO:playsound 
        MissionsToggleAndText[missionNum].missionToggle.isOn = true;
    }

    public void updateMissionProggressEndOfGame()
    {
        if(missionStats.updateMissionStats(this.missions))
            {
                deathLogic.switchMissionsOnComplete(missionStats.switchMissions());
            }  
    }
}
