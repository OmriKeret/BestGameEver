using UnityEngine;
using System.Collections;

public class LevelUpHandler : MonoBehaviour, PhaseEventHandler {

    FinishedMissionHandler finishedMission;
	// Use this for initialization
	void Start () {
        finishedMission = this.GetComponent<FinishedMissionHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator run()
    {
      yield return new WaitForSeconds(2);
      Debug.Log("finished waiting");
      finishedMission.finishedLevelingUp();
    }

    public void handleEvent()
    {
        StartCoroutine(run());
    }

    public void next()
    {
        throw new System.NotImplementedException();
    }

    public void setNext(PhaseEventHandler next)
    {
        throw new System.NotImplementedException();
    }

    internal void handleEvent(ref bool animationRuns)
    {
        throw new System.NotImplementedException();
    }
}
