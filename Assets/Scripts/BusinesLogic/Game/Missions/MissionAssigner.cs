using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MissionAssigner : MonoBehaviour {

    MissionModel[] missions;
    Dictionary<EnemyType, string> enemyTypeString;
    Dictionary<CollectableTypes, string> collactableTypeString;
    Dictionary<PowerUpType, string> PowerUpTypeString;
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
        mission.enemyType = selectedMission.enemyType;
        mission.powerUpType = selectedMission.powerUpType;
        mission.isFinished = false;
        mission.needToBeCompletedInOneGame = selectedMission.needToBeCompletedInOneGame;
		mission.numberToAchive = TierFactor(tier, mission.type, mission.needToBeCompletedInOneGame);
        string inOneGame = mission.needToBeCompletedInOneGame ? " in one game!" : "!";
		//TODO : Different enemies
        if(mission.type == MissionType.killTypeOfEnemy) {
            mission.missionText = string.Format("Kill total of {0} {1}{2}", mission.numberToAchive, enemyTypeString[mission.enemyType], inOneGame);
        }
		//Up to here.
        if (mission.type == MissionType.takeCollectable)
        {
            mission.missionText = string.Format("Get total of {0} {1}{2}", mission.numberToAchive, collactableTypeString[mission.collectableType], inOneGame);
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
	private int TierFactor(int tier, MissionType mission, bool inOneGame) 
		/*Killing = 10x tier*/
		/*Surviving = 5 sec per tier*/
		/*Score = 10k Per tier*/
		/*Collectable = 1 per tier*/
		/*Powerup = 1 per tier*/
		/*Not in one Game = x10 */
	{
		int ResultingNumber = 1;
		switch (mission)
		{
			case MissionType.survival:
				ResultingNumber *= 5;
				break;
			case MissionType.killTypeOfEnemy:
				ResultingNumber *= 10;
				break;
			case MissionType.getScoreOf:
				ResultingNumber *= 10;
				break;
			case MissionType.takePowerUp:
				ResultingNumber *= 1;
				break;
			case MissionType.takeCollectable:
				ResultingNumber *= 1;
				break;
			default:
				ResultingNumber *= 1;
				break;
		}
		return inOneGame ? ResultingNumber : (ResultingNumber * 10);
		
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
			//TODO : foreach(PowerUpType powerUp in Enum.GetValues(typeof(PowerUpType))), for enemies, for collectables, etc.
                                             //Killing
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.General, needToBeCompletedInOneGame = true }, 
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Stupid, needToBeCompletedInOneGame = true }, 
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Spike, needToBeCompletedInOneGame = true }, 
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Tank, needToBeCompletedInOneGame = true }, 
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Hawk, needToBeCompletedInOneGame = true }, 
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Cannon, needToBeCompletedInOneGame = true },
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Stupid, needToBeCompletedInOneGame = false }, 
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Spike, needToBeCompletedInOneGame = false }, 
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Tank, needToBeCompletedInOneGame = false }, 
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Hawk, needToBeCompletedInOneGame = false }, 
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Cannon, needToBeCompletedInOneGame = false }, 
			new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 1, enemyType = EnemyType.Stupid, needToBeCompletedInOneGame = false },
			//Killing_End, Starting Survival
			new MissionModel {type = MissionType.survival , numberToAchive = 1 , needToBeCompletedInOneGame = true},
			new MissionModel {type = MissionType.survival , numberToAchive = 1 , needToBeCompletedInOneGame = false},
			//Survival_End, Starting Score
            new MissionModel {type = MissionType.getScoreOf , numberToAchive = 1, needToBeCompletedInOneGame = true},
			new MissionModel {type = MissionType.getScoreOf , numberToAchive = 1, needToBeCompletedInOneGame = false},
			//Score_End, Starting Collectable
            new MissionModel {type = MissionType.takeCollectable , numberToAchive = 1, collectableType = CollectableTypes.COIN, needToBeCompletedInOneGame = true},
            new MissionModel {type = MissionType.takeCollectable , numberToAchive = 1 ,collectableType = CollectableTypes.COIN, needToBeCompletedInOneGame = false},
			//Collectable_End, Starting PowerUp
			new MissionModel {type = MissionType.takePowerUp , numberToAchive = 1, powerUpType = PowerUpType.SUPERHIT , needToBeCompletedInOneGame = true},
			new MissionModel {type = MissionType.takePowerUp , numberToAchive = 1, powerUpType = PowerUpType.SUPERHIT , needToBeCompletedInOneGame = false},
        };

        enemyTypeString = new Dictionary<EnemyType, string> 
        {
            {EnemyType.General, "Enemies"},
            {EnemyType.Stupid, "Flying Goblins"},
            {EnemyType.Spike, "Fat Flying Thug"},
            {EnemyType.Tank, "Fire Demon"},
			{EnemyType.Cannon, "Cannon"},
			{EnemyType.Hawk, "Hawk"}
        };
        collactableTypeString = new Dictionary<CollectableTypes, string> 
        {
            {CollectableTypes.COIN, "Demon Coins!"
			}

        };
        PowerUpTypeString = new Dictionary<PowerUpType, string>
		{
			{PowerUpType.BUBBLE, "Bubble power up!"},
			{PowerUpType.INVINCABLE, "Invincible power up! ;)"},
			{PowerUpType.SUPERHIT, "Mega Hit Power"}

		};
    }


}
