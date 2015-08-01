using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndscreenGuiMain : MonoBehaviour {

    EndscreenChainFactory endScreenChainFactory;
    MissionLogic missionLogic;
    Button pauseBtn;
    PhaseEventHandler firstEvent;
    TouchInterpeter touch;
    Animator blackBackground;

	void Start ()
    {
        endScreenChainFactory = this.gameObject.GetComponent<EndscreenChainFactory>();
        missionLogic = this.gameObject.GetComponentInParent<MissionLogic>();
        pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
        blackBackground = GameObject.Find("Canvas/DarkScreen").GetComponent<Animator>();
	}

    /** 
     * Exposed method in order to start the death screen
     * */
    public void initiateDeathScreen()
    {
        blockInput();
        blackBackground.SetBool("darkScreen", true);
        startEndScreen();
    }

    private void blockInput()
    {
        pauseBtn.interactable = false;
        touch.SetDisableMovment();
    }

    void startEndScreen()
    {
        bool finishedMission = missionLogic.finishedMission();
        firstEvent = endScreenChainFactory.getChain(finishedMission);
        firstEvent.handleEvent();
    }
}
