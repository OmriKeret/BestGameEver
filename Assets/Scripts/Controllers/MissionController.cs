using UnityEngine;
using System.Collections;

public class MissionController : MonoBehaviour {

    MissionLogic missionLogic;
    MissionModel[] missions;
    MissionStats missionDataStats;
	// Use this for initialization
    void OnEnable() { }
	void Start () {
        missionLogic = GameObject.Find("Logic").GetComponent<MissionLogic>();
        missionDataStats = GameObject.Find("GameManagerData").GetComponent<MissionStats>();     
        getMissionData();
        
        string title = missionDataStats.getTitle();
        //geting the model from a data object
        missionLogic.setMissions(missions,title);
	}

    private void getMissionData()
    {
        missions = missionDataStats.getMissions();
    }
	
	
}
