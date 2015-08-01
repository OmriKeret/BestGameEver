using UnityEngine;
using System.Collections;

public class GUIButtonHelper : MonoBehaviour {

	// level up
	LevelUpHandler levelupHandler;

	// Mission complete 
	FinishedMissionHandler finishedMission;

	void Start () {
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
}
