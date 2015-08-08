using UnityEngine;
using System.Collections;

public class EndscreenChainFactory : MonoBehaviour {
    PhaseEventHandler finishedMissionHandler;
    PhaseEventHandler levelUpHandler;
    PhaseEventHandler summeryScreen;
    NewMissionsHandler newMissionHandler;
	// Use this for initialization
	void Start () {
        finishedMissionHandler = this.gameObject.GetComponent<FinishedMissionHandler>();
        levelUpHandler = this.gameObject.GetComponent<LevelUpHandler>();
        summeryScreen = this.gameObject.GetComponent<SummeryScreen>();
        newMissionHandler = this.gameObject.GetComponent<NewMissionsHandler>();
	}

    public PhaseEventHandler getChain(bool finishedMission)
    {
        PhaseEventHandler first = null;
        if (false)//finishedMission)
        {
            first = finishedMissionHandler;
            finishedMissionHandler.setNext(newMissionHandler);
            newMissionHandler.setNext(summeryScreen);
        }
        else
        {
            first = summeryScreen;
        }

        return first;
    }
}
