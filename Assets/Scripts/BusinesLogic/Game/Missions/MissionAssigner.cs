using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MissionAssigner : MonoBehaviour {

    MissionModel[] missions;
    Dictionary<EnemyType, string> enemyTypeString;
    Dictionary<PowerUpType, string> powerUpTypeString;
	// Use this for initialization
	void Awake () {
        MissionInitializer();
	}


    public MissionModel[] getNewMissions(int tier)
    {
        List<MissionModel> result = new List<MissionModel>();
        int i = 0;
        while (i < 3)
        {
            MissionModel mission = getRandomMission(tier);

            //if mission doesn't exists yet
            if(!result.Exists(m => m.type == mission.type)) {
                result.Add(mission);
                i++;
            }
        }
        return result.ToArray();

    }

    //helper method to generate random mission by tier
    private MissionModel getRandomMission(int tier)
    {
        MissionModel mission = new MissionModel();
        int max = missions.Length;
        var selectedMission = missions[UnityEngine.Random.Range(0, max)];
        mission.type = selectedMission.type;
        mission.numberToAchive =(int) (selectedMission.numberToAchive * 1.1 * tier);
        mission.enemyType = selectedMission.enemyType;
        mission.powerUpType = selectedMission.powerUpType;
        mission.isFinished = false;
        mission.needToBeCompletedInOneGame = selectedMission.needToBeCompletedInOneGame;

        if(mission.type == MissionType.killTypeOfEnemy) {
            mission.missionText = string.Format("Kill total of {0} {1}", mission.numberToAchive, enemyTypeString[mission.enemyType]);
        }
        if (mission.type == MissionType.takePowerUp)
        {
            mission.missionText = string.Format("Get total of {0} {1}", mission.numberToAchive, powerUpTypeString[mission.powerUpType]);
        }
        if (mission.type == MissionType.getScoreOf)
        {
            mission.missionText = string.Format("Get score of {0}!", string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                 "{0:0,0}", mission.numberToAchive));
        }
        if (mission.type == MissionType.survival)
        {
            TimeSpan time = TimeSpan.FromSeconds(mission.numberToAchive);
            string text = string.Format("{0}:{1} ",time.TotalMinutes,time.TotalSeconds);
            mission.missionText = string.Format("Survive for {0} minutes!", text);
        }
        return mission;
    }

    //setting variables 
    public void MissionInitializer()
    {
        missions = new MissionModel[] { 
                                             new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 5, missionText ="Kill Total of five enemies!", enemyType = EnemyType.General, needToBeCompletedInOneGame = true }, 
                                             new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 5, missionText ="Kill five stupid enemies!", enemyType = EnemyType.Stupid, needToBeCompletedInOneGame = true }, 
                                             new MissionModel {type = MissionType.getScoreOf , numberToAchive = 10000, missionText ="Get score of 10,000!" , needToBeCompletedInOneGame = true}
                                               };
        enemyTypeString = new Dictionary<EnemyType, string> 
        {
            {EnemyType.General, "enemies!"},
            {EnemyType.Stupid, "flying goblins!"}
        };
        powerUpTypeString = new Dictionary<PowerUpType, string> 
        {
            {PowerUpType.SUPERHIT, "Super Hit PowerUPs!"}
        };
    }


}
