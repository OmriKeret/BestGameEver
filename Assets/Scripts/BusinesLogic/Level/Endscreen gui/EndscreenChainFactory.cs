using UnityEngine;
using System.Collections;

public class EndscreenChainFactory : MonoBehaviour {
    PhaseEventHandler finishedMissionHandler;
    PhaseEventHandler levelUpHandler;
    PhaseEventHandler summeryScreen;
	// Use this for initialization
	void Start () {
        finishedMissionHandler = this.gameObject.GetComponent<FinishedMissionHandler>();
        levelUpHandler = this.gameObject.GetComponent<LevelUpHandler>();
        summeryScreen = this.gameObject.GetComponent<SummeryScreen>();
	}

    public PhaseEventHandler getChain(bool finishedMission)
    {
        PhaseEventHandler first = null;
        if (finishedMission)
        {
            first = finishedMissionHandler;
            finishedMissionHandler.setNext(summeryScreen);
        }
        else
        {
            first = summeryScreen;
        }

        return first;
    }
}
