using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissionLogic : MonoBehaviour {

  MissionModel[] missions;
  Dictionary<EnemyType, int> enemyKills;
  Dictionary<PowerUpType, int> powerUpstaken;
  Dictionary<CollectableTypes, int>collactablesTaken;
  public InternalMissionModel[] MissionsToggleAndText;
  int TrackTimeForMissionNumber;
  DeathLogic deathLogic; 
  MissionStats missionStats; 
  public Text missionTitle;
  private EventListener listener;

  public GameObject CMStar;
	// Use this for initialization
	void Start () {
        listener = EventListener.instance;
        deathLogic = this.gameObject.GetComponent<DeathLogic>();
        missionStats = GameObject.Find("GameManagerData").GetComponent<MissionStats>();     

        enemyKills = new Dictionary<EnemyType, int>	{ 
			{ EnemyType.Stupid, 0 },
            { EnemyType.General, 0 }
		};
        powerUpstaken = new Dictionary<PowerUpType, int>	{ 
			{ PowerUpType.SUPERHIT, 0 }
		};

        collactablesTaken = new Dictionary<CollectableTypes, int>	{ 
			{ CollectableTypes.COIN, 0 }
		};
		MissionsToggleAndText = new InternalMissionModel[] {
			new InternalMissionModel(),
			new InternalMissionModel(),
			new InternalMissionModel()
		};
        TrackTimeForMissionNumber = -1;
        int missionNum = 0;
		//var x = GameObject.Find("PauseMenu/Mission1/Mission1Label").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionText = GameObject.Find("PauseMenu/Mission1/Mission1Label").GetComponent<Text>();
        MissionsToggleAndText[missionNum].missionCount = GameObject.Find("PauseMenu/Mission1/Mission1Count").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionToggle = GameObject.Find("PauseMenu/Mission1").GetComponent<Toggle>();
        MissionsToggleAndText[missionNum].firstStarPos = GameObject.Find("PauseMenu/Mission1/FirstStarPos").transform.position;
        missionNum++;

		MissionsToggleAndText[missionNum].missionText = GameObject.Find("PauseMenu/Mission2/Mission2Label").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionToggle = GameObject.Find("PauseMenu/Mission2").GetComponent<Toggle>();
        MissionsToggleAndText[missionNum].missionCount = GameObject.Find("PauseMenu/Mission2/Mission2Count").GetComponent<Text>();
        MissionsToggleAndText[missionNum].firstStarPos = GameObject.Find("PauseMenu/Mission2/FirstStarPos").transform.position;
        missionNum++;

		MissionsToggleAndText[missionNum].missionText = GameObject.Find("PauseMenu/Mission3/Mission3Label").GetComponent<Text>();
		MissionsToggleAndText[missionNum].missionToggle = GameObject.Find("PauseMenu/Mission3").GetComponent<Toggle>();
        MissionsToggleAndText[missionNum].missionCount = GameObject.Find("PauseMenu/Mission3/Mission3Count").GetComponent<Text>();
        MissionsToggleAndText[missionNum].firstStarPos = GameObject.Find("PauseMenu/Mission3/FirstStarPos").transform.position;
        missionTitle = GameObject.Find("PauseMenu/MissionTitle").GetComponent<Text>();
    
	}

    //counting time to timed missions
    void FixedUpdate()
    {
        if (TrackTimeForMissionNumber != -1)
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
           if (missions[i].type == MissionType.takeCollectable)
           {
               collactablesTaken[missions[i].collectableType] = missions[i].currentNumberAchived;
           }
           if (missions[i].type == MissionType.survival)
           {  
               //if we need to track time for mission i
               this.TrackTimeForMissionNumber = i;
           }
           updateNumberAchived(i, missions[i].currentNumberAchived);
       }
       missionTitle.text = title;
       initilizeMissiosnData();

    }

   //helper method to format time text
   private string formatCountTimeString(int numberToAchive, int numberAchivedAlready)
   {
       int seconds = numberToAchive % 60;
       int minutes = numberToAchive / 60;
       string right = minutes + ":" + seconds;
       seconds = numberAchivedAlready % 60;
       minutes = numberAchivedAlready / 60;
       string left = minutes + ":" + seconds;
       return string.Format("{0}/{1}", left, right);
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
       if (missions[missionNumber].numberToAchive <= numberAchived)
       {
           numberAchived = missions[missionNumber].numberToAchive;
           finishedMission(missionNumber);

       }
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
        if (!missions[missionNum].isFinished)
        {
            MissionsToggleAndText[missionNum].missionToggle.isOn = true;
            missions[missionNum].isFinished = true;
            listener.Listener[EventTypes.FinishedMission].Invoke(MissionsToggleAndText[missionNum].missionText.text, PopupType.EndMission);
            missionStats.finishedMission(missionNum);
        }
    }

    public void updateMissionProggressEndOfGame()
    {
        if (missionStats.updateMissionStats(this.missions))
        {
         
        }
        else
        {

        }
    }

    internal int getTier()
    {
        return missionStats.getTier();
    }


    internal void saveMissionData()
    {
        missionStats.SaveMissionProgression();
    }

    internal void addCollactable(CollectableTypes collactable)
    {
        if (!collactablesTaken.ContainsKey(collactable))
        {
            return;
        }
        collactablesTaken[collactable]++;
        for (int i = 0; i < missions.Length; i++)
        {
			if (missions[i].type == MissionType.takeCollectable && missions[i].collectableType == collactable)
            {
                updateNumberAchived(i, collactablesTaken[collactable]);
                if (missions[i].type == MissionType.takeCollectable && missions[i].collectableType == collactable && missions[i].numberToAchive <= collactablesTaken[collactable])
                {
                    finishedMission(i);
                }
            }
        }
    }

    /**
     *  Check if a mission is finished.
     * */
    internal bool finishedMission()
    {
       foreach (var mission in  missions) 
       {
           if (mission.isFinished)
           {
               return true;
           }
       }
       return false;
    }

    internal MissionModel[] getMissions()
    {
        return missionStats.getMissions();
    }

    internal int getFirstMissingStarIndex()
    {
        int i = 0;
        bool[] missionStars = missionStats.rankStars;
        while (i < missionStars.Length)
        {
            if (!missionStars[i])
            {
                return i;
            }
            i++;
        }
        return missionStars.Length;
    }

    internal string getTierTitle()
    {
        return missionStats.getTitle();
    }

    internal bool[] getRankStars()
    {
        return missionStats.rankStars;
    }

    internal bool addRankStar()
    {
        var leveledUp =  missionStats.addRankStar();
		if (leveledUp) 
		{
			missionStats.upgradeTier();
		}
		return leveledUp;
    }

	public int getRankCompleteReward ()
	{
		return 100 + 300 * (getTier () - 1);
	}

    public bool didLevelUp()
    {
       int i = getFirstMissingStarIndex();
       int numberOfCurrentLevelStars = getRankStars().Length;
       int numberToAchive = numberOfCurrentLevelStars - i;
       foreach (var mission in missions)
       {
           if (mission.isFinished)
           {
               numberToAchive = numberToAchive - mission.numberOfStars;
           }
           if (numberToAchive <= 0)
           {
               return true;
           }
       }
       return false;
    }
    /**
     * Gets and sets new missions
     * */
    internal MissionModel[] getNewMissions()
    {
       return missionStats.getNewMissions();

    }

    internal void unMarkMissionsNew()
    {
        missionStats.unMarkMissionAsNew();
    }

    internal int getNextRankStars()
    {
        return missionStats.getNextLevelStars();
    }

    private void initilizeMissiosnData()
    {
        for (int index = 0; index < missions.Length; index++)
        {
            Vector3 firstStarPos = MissionsToggleAndText[index].firstStarPos;
            for (int i = 0; i < missions[index].numberOfStars; i++)
            {
                var CMProgressStars = Instantiate(CMStar, firstStarPos + new Vector3(i * 3.2f, 0, 0), Quaternion.identity) as GameObject;
                CMProgressStars.transform.parent = MissionsToggleAndText[index].missionToggle.transform;
            }
        }
    }
}
