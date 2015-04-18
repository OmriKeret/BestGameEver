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
        DontDestroyOnLoad(gameObject);
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
            if (!result.Exists(m => m.type == mission.type))
            {
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
        string inOneGame = mission.needToBeCompletedInOneGame ? " in one game!" : "!";
        if(mission.type == MissionType.killTypeOfEnemy) {
            mission.missionText = string.Format("Kill total of {0} {1}{2}", mission.numberToAchive, enemyTypeString[mission.enemyType], inOneGame);
        }
        if (mission.type == MissionType.takePowerUp)
        {
            mission.missionText = string.Format("Get total of {0} {1}{2}", mission.numberToAchive, powerUpTypeString[mission.powerUpType], inOneGame);
        }
        if (mission.type == MissionType.getScoreOf)
        {
            mission.missionText = string.Format("Get score of {0}{1}", string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                 "{0:0,0}", mission.numberToAchive), inOneGame);
        }
        if (mission.type == MissionType.survival)
        {
          //  TimeSpan time = TimeSpan.FromSeconds(mission.numberToAchive);
            string text = formatCountTimeString(mission.numberToAchive);
            mission.missionText = string.Format("Survive for {0} minutes!", text);
        }
        return mission;
    }
    private string formatCountTimeString(int numberToAchive)
    {
        int seconds = numberToAchive % 60;
        int minutes = numberToAchive / 60;
        return minutes + ":" + seconds;
    }
    //setting variables 
    private void MissionInitializer()
    {
        missions = new MissionModel[] { 
                                             new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 20, missionText ="Kill Total of five enemies!", enemyType = EnemyType.General, needToBeCompletedInOneGame = true }, 
                                             new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 6, missionText ="Kill five stupid enemies!", enemyType = EnemyType.Stupid, needToBeCompletedInOneGame = true }, 
                                             new MissionModel {type = MissionType.getScoreOf , numberToAchive = 10000, missionText ="Get score of 10,000!" , needToBeCompletedInOneGame = true},
                                             new MissionModel {type = MissionType.survival , numberToAchive = 70, missionText ="Survive for:" , needToBeCompletedInOneGame = true},
                                             new MissionModel {type = MissionType.takePowerUp , numberToAchive = 5, powerUpType = PowerUpType.SUPERHIT , needToBeCompletedInOneGame = true},
                                             new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 40, missionText ="Kill Total of five enemies!", enemyType = EnemyType.General, needToBeCompletedInOneGame = false }, 
                                             new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 20, missionText ="Kill five stupid enemies!", enemyType = EnemyType.Stupid, needToBeCompletedInOneGame = false }, 
                                             new MissionModel {type = MissionType.getScoreOf , numberToAchive = 100000, missionText ="Get score of 10,000!" , needToBeCompletedInOneGame = false},
                                             new MissionModel {type = MissionType.takePowerUp , numberToAchive = 10, powerUpType = PowerUpType.SUPERHIT , needToBeCompletedInOneGame = false},
                                               };
        enemyTypeString = new Dictionary<EnemyType, string> 
        {
            {EnemyType.General, "enemies"},
            {EnemyType.Stupid, "flying goblins"}
        };
        powerUpTypeString = new Dictionary<PowerUpType, string> 
        {
            {PowerUpType.SUPERHIT, "Super Hit PowerUPs"}
        };
    }


}
