using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class MissionStats : MonoBehaviour {

    int tier;
    public string title;
    public bool[] rankStars;
    MissionModel[] currentMissions;
    MissionAssigner missionAssigner;
    Dictionary<int, string> tierTitle;
	Dictionary<int, int> tierStars;
    public bool finishedInit = false;

	// Use this for initialization.
	void OnLevelWasLoaded()
    {
        IOMissionModel missionsFromDisc = null ;
        initalizeDictionary();
       // tier = missions.tier; //TODO: get tier from memory
        missionAssigner = this.gameObject.GetComponent<MissionAssigner>();
        if (currentMissions == null)
        {
            if (MemoryAccess.memoryAccess == null)
            {
                missionsFromDisc = null;
            }
           missionsFromDisc = MemoryAccess.memoryAccess.LoadMission();

          //  var missionsFromDisc = MemoryAccess.memoryAccess.LoadMission();
           if (true)//missionsFromDisc == null || missionsFromDisc.missions == null) //todo: remove debug comment
	        {
                tier = 1;
                rankStars = new bool[3];
                initilizeMissions();
               foreach(var m in currentMissions)  // REMOVE THIS
               {                                  // REMOVE THIS
                   m.isFinished = true;
                   m.numberOfStars = 2;				// REMOVE THIS
               }                                    // REMOVE THIS
                
	        }
	        else
	        {

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
        MemoryAccess.memoryAccess.SaveMissions(new IOMissionModel { tier = tier, missions = currentMissions, rankStars = rankStars });
    }
    private void initalizeDictionary()
    {
        tierTitle = new Dictionary<int, string>{ //All names here are subject to change
            {1, "Novice"},
            {2, "Beginner Samurai"},
            {3, "Amateur Samurai"},
            {4, "Samurai Apprentice"},
			{5, "Samurai"},
            {6, "La Llorona"},
            {7, "Ninja"},
            {8, "Chupacabra"},
			{9, "Iturbide"},
            {10, "Master of the Heike Clan"},
            {11, "Samurai Jack!"}
        };

		tierStars = new Dictionary<int, int>{ //All ranks here are subject to change
			{1, 3}, //"Novice"
			{2, 4}, //"Beginner Samurai"
			{3, 5}, //"Amateur Samurai"
			{4, 6}, //"Samurai Apprentice"
			{5, 7}, //"Samurai"
			{6, 8}, //"La Llorona"
			{7, 8},
			{8, 8},
			{9, 8},
			{10, 8},
			{11, 8}
		};
    }
    public string getTitle() 
    {
        string s = "";
        if (tierTitle.TryGetValue(tier, out s))
        {
            return s;
        }
    
        return tierTitle[10];
    }
    public void upgradeTier()
    {
        tier++;
		int numStars;
		tierStars.TryGetValue (tier, out numStars);
		rankStars = new bool[numStars];
    }

    private void initilizeMissions()
    {
        currentMissions = new MissionModel[3];
		for (int j = 0; j < currentMissions.Length; j++) {
			currentMissions[j] = new MissionModel();
		}
        List<MissionModel> missionList = currentMissions.Where(e => !e.isFinished).ToList();
        for (int i = 0; i < currentMissions.Length; i++)
        {
            MissionModel mission;
            do
            {
                mission = missionAssigner.getNewMission(1);
            } while (missionList.Exists(m => m.type == mission.type));
            currentMissions[i] = mission;
            missionList.Add(mission);
        }
    }
    public MissionModel[] getNewMissions()
    {
        List<MissionModel> missionList = currentMissions.Where(e => !e.isFinished).ToList();
        for (int i = 0; i < currentMissions.Length; i++)
        {
            if (currentMissions[i].isFinished)
            {
                MissionModel mission;
                do
                {
                    mission = missionAssigner.getNewMission(tier);
                } while (missionList.Exists(m => m.type == mission.type));
                currentMissions[i] = mission;
                missionList.Add(mission);
            }
        }
        return currentMissions;
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
            result[i].numberOfStars = currentMissions[i].numberOfStars;
            result[i].missionText = currentMissions[i].missionText;
            result[i].needToBeCompletedInOneGame = currentMissions[i].needToBeCompletedInOneGame;
            result[i].numberToAchive = currentMissions[i].numberToAchive;
            result[i].powerUpType = currentMissions[i].powerUpType;
            result[i].collectableType = currentMissions[i].collectableType;
            result[i].type = currentMissions[i].type;
            result[i].isNew = currentMissions[i].isNew;
        }
        return result;
    }

    internal int getTier()
    {
        return this.tier;
    }

    internal bool addRankStar()
    {
        for (var i = 0; i < rankStars.Length; i++)
        {
            if (!rankStars[i])
            {
                Debug.Log("adding star number " + i);
                rankStars[i] = true;
                return (i == rankStars.Length - 1);
            }
        }
        return false;
    }


    internal void finishedMission(int missionNum)
    {
        currentMissions[missionNum].isFinished = true;
    }

    internal void unMarkMissionAsNew()
    {
        foreach (var mission in currentMissions)
        {
            mission.isNew = false;
        }
    }

    internal int getNextLevelStars()
    {
        int numStars;
        if (tierStars.TryGetValue(tier + 1, out numStars)){
            return numStars;
        }

        return rankStars.Length;
         
    }
}
