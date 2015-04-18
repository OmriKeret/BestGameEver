using UnityEngine;
using System.Collections;

public class MissionStats : MonoBehaviour {

    int tier;
    MissionModel[] currentMissions;
    MissionAssigner missionAssigner;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {
        tier = 1; //TODO: get tier from memory
        missionAssigner = this.gameObject.GetComponent<MissionAssigner>();
        if (currentMissions == null)
        {
            currentMissions = missionAssigner.getNewMissions(tier);
        }
	}

    public void upgradeTier()
    {
        tier++;
    }

    public void getNewMissions()
    {
        currentMissions = missionAssigner.getNewMissions(tier);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
