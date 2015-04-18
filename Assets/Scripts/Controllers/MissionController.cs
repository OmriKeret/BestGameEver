using UnityEngine;
using System.Collections;

public class MissionController : MonoBehaviour {

    MissionLogic missionLogic;
    MissionModel[] missions;
    MissionStats missionDataStats;
	// Use this for initialization
	void Start () {
        missionLogic = GameObject.Find("Logic").GetComponent<MissionLogic>();
        missionDataStats = GameObject.Find("GameManagerData").GetComponent<MissionStats>();     
        getMissionData();
        
        //stub missions TODO: replace with real missions from data
        //MissionModel[] missions = new MissionModel[] {  new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 5, missionText ="Kill Total of five enemies!", enemyType = EnemyType.General }, 
        //                                                new MissionModel {type = MissionType.killTypeOfEnemy , numberToAchive = 5, missionText ="Kill five stupid enemies!", enemyType = EnemyType.Stupid }, 
        //                                                new MissionModel {type = MissionType.getScoreOf , numberToAchive = 10000, missionText ="Get score of 10,000!" }
        //                                                };

        //stub missions
        string title = missionDataStats.getTitle();
        //geting the model from a data object
        missionLogic.setMissions(missions,title);
	}

    private void getMissionData()
    {
        missions = missionDataStats.getMissions();
    }
	
	
}
