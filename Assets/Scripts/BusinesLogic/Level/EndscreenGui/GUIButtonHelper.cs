using UnityEngine;
using System.Collections;

public class GUIButtonHelper : MonoBehaviour {

    // new missions
    NewMissionsHandler newMissionsHandler;

	// level up
	LevelUpHandler levelupHandler;

	// Mission complete 
	FinishedMissionHandler finishedMission;

	void Start () 
    {
        newMissionsHandler = GameObject.Find("Logic").GetComponentInChildren<NewMissionsHandler>();
		levelupHandler = GameObject.Find("Logic").GetComponentInChildren<LevelUpHandler>();
		finishedMission = GameObject.Find("Logic").GetComponentInChildren<FinishedMissionHandler>();
	}
	
	public void RewardNext() 
	{
		levelupHandler.OnClickRewardNext ();
	}

	public void FinishedMissionNext()
	{
		finishedMission.onNextButtonClicked ();
	}

    public void NewMissionsNext()
    {
        newMissionsHandler.OnClickedNext();
    }
}
