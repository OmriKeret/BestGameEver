using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionStats : MonoBehaviour {

    int tier;
    public string title;
    MissionModel[] currentMissions;
    MissionAssigner missionAssigner;
    Dictionary<int, string> tierTitle;
    public bool finishedInit = false;
	// Use this for initialization
	void Start()
    {
        IOMissionModel missionsFromDisc = null ;
        initalizeDictionary();
       // tier = missions.tier; //TODO: get tier from memory
        missionAssigner = this.gameObject.GetComponent<MissionAssigner>();
        if (true)//currentMissions == null)
        {
           missionsFromDisc = MemoryAccess.memoryAccess.LoadMission();

          //  var missionsFromDisc = MemoryAccess.memoryAccess.LoadMission();
	        if (true)//missionsFromDisc == null)
	        {
                tier = 1;
	            currentMissions = missionAssigner.getNewMissions(tier);
	        }
	        else
	        {
                //currentMissions = missionsFromDisc.missions;
                currentMissions = new MissionModel[missionsFromDisc.missions.Length];
                for (int i = 0; i < currentMissions.Length; i++)
                {
                    currentMissions[i] = missionsFromDisc.missions[i];
                }
                tier = missionsFromDisc.tier;
	        }     
        }
 
	}

    public void SaveMissionProgression()
    {
        MemoryAccess.memoryAccess.SaveMissions(new IOMissionModel { tier = tier, missions = currentMissions });
    }
    private void initalizeDictionary()
    {
        tierTitle = new Dictionary<int, string>{
            {1, "Novice samurai"},
            {2, "Lucky samurai"},
            {3, "Mexican burrito"},
            {4, "Samurai Jack!"}
        };
    }
    public string getTitle() 
    {
        string s = "";
        if (tierTitle.TryGetValue(tier, out s))
        {
            return s;
        }
        Debug.Log("Title got is null! tier is " + tier);
        return tierTitle[1];
    }
    public void upgradeTier()
    {
        tier++;
    }

    public void getNewMissions()
    {
        currentMissions = missionAssigner.getNewMissions(tier);

    }
	// Update is called once per frame
	void Update () {
	
	}

    public bool updateMissionStats(MissionModel[] missionModel)
    {
        bool finishedAll = true;
        for (int i = 0; i < 3; i++)
        {
            currentMissions[i].isFinished = missionModel[i].isFinished;
            if (!currentMissions[i].isFinished)
            {
                finishedAll = false;
            }
            if(currentMissions[i].isFinished || !currentMissions[i].needToBeCompletedInOneGame)
            {
                currentMissions[i].currentNumberAchived = missionModel[i].currentNumberAchived;
            }
        }
        return finishedAll;
    }

    public MissionModel[] getMissions()
    {
        return cloneMissions(currentMissions);
    }

    public MissionModel[] switchMissions()
    {
       upgradeTier();
       getNewMissions();
       return getMissions();
    }

    private MissionModel[] cloneMissions(MissionModel[] currentMissions)
    {
        MissionModel[] result = new MissionModel[3];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new MissionModel();
            result[i].currentNumberAchived = currentMissions[i].currentNumberAchived;
            result[i].enemyType = currentMissions[i].enemyType;
            result[i].isFinished = currentMissions[i].isFinished;
            result[i].missionText = currentMissions[i].missionText;
            result[i].needToBeCompletedInOneGame = currentMissions[i].needToBeCompletedInOneGame;
            result[i].numberToAchive = currentMissions[i].numberToAchive;
            result[i].powerUpType = currentMissions[i].powerUpType;
            result[i].type = currentMissions[i].type;
        }
        return result;
    }

    internal int getTier()
    {
        return this.tier;
    }
}
