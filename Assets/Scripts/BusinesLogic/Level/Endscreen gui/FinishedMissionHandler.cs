using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinishedMissionHandler : MonoBehaviour, PhaseEventHandler{

    // Missions
    MissionLogic missionLogic;
    MissionModel[] missions;


    // GUI.
    // Mission complete object and components.
    GameObject CMObject;
    GameObject CMFirstStar;
    Text CMText;
    bool[] CMProgressStars;

    // Rank Panel.
    Text rankTitle;
    GameObject rankFirstStar;
    bool[] rankProgressStars;

    // Timing param
    bool animationIsPlaying = false;
	// Use this for initialization
	void Start () {
        
        // Logic.
        missionLogic = this.gameObject.GetComponent<MissionLogic>();

        // GUI.
        // Completed Mission Object 
        CMObject = GameObject.Find("MissionComplete/CompletedMission");
        CMFirstStar = GameObject.Find("MissionComplete/CompletedMission/firstStarPos");
        CMText = GameObject.Find("MissionComplete/CompletedMission/MissionCompleteText").GetComponent<Text>(); 
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator run()
    {
        missions = missionLogic.getMissions();
        foreach (var mission in missions)
        {
            if (mission.isFinished)
            {
                // Initiate stars.
                // Move mission in.
                // .

            }

            while (animationIsPlaying)
            {
                yield return null;
            }
        }
    }
    public void handleEvent()
    {
        StopCoroutine("Movement");
      
    }

    public void next()
    {
        throw new System.NotImplementedException();
    }

    public void setNext(PhaseEventHandler next)
    {
        throw new System.NotImplementedException();
    }
}
