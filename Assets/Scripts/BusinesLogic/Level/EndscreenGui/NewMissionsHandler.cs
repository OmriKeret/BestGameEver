﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NewMissionsHandler : MonoBehaviour, PhaseEventHandler{

    PhaseEventHandler nextEvent;

    // logic
    MissionLogic missionLogic;
    //GUI
    // Missions
    // newMissio Object
    GameObject newMissionObject;

    //new mission one
    Toggle missionOneToggle;
    Text missionOneText;
    GameObject missionOneFirstStarPos;

    //new mission two
    Toggle missionTwoToggle;
    Text missionTwoText;
    GameObject missionTwoFirstStarPos;

    //new mission Three
    Toggle missionThreeToggle;
    Text missionThreeText;
    GameObject missionThreeFirstStarPos;

	//new mission List
	List<GameObject> newMissonList;

    //missions stars
    public GameObject star;

	// Use this for initialization
	void Start () {
        missionLogic = this.gameObject.GetComponentInParent<MissionLogic>();
        //GUI
        // newMissio Object
        newMissionObject = GameObject.Find("NewMissions");
		newMissonList = new List<GameObject> ();

        //new mission One
        missionOneToggle = GameObject.Find("Canvas/NewMissions/MissionOne/Toggle").GetComponent<Toggle>();
        missionOneText = GameObject.Find("NewMissions/MissionOne/MissionText").GetComponent<Text>();
        missionOneFirstStarPos = GameObject.Find("NewMissions/MissionOne/firstStarPos");

        //new mission Two
        missionTwoToggle = GameObject.Find("NewMissions/MissionTwo/Toggle").GetComponent<Toggle>();
        missionTwoText = GameObject.Find("NewMissions/MissionTwo/MissionText").GetComponent<Text>();
        missionTwoFirstStarPos = GameObject.Find("NewMissions/MissionTwo/firstStarPos");

        //new mission Three
        missionThreeToggle = GameObject.Find("NewMissions/MissionThree/Toggle").GetComponent<Toggle>();
        missionThreeText = GameObject.Find("NewMissions/MissionThree/MissionText").GetComponent<Text>();
        missionThreeFirstStarPos = GameObject.Find("NewMissions/MissionThree/firstStarPos");
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void handleEvent()
    {
        updateMissions();
        moveMissionsIn();
    }

    private void moveMissionsIn()
    {
		//Change position of new mission to the left of the screen.
		foreach(var mission in newMissonList) {
			var pos = mission.transform.position;
			mission.transform.position = new Vector3(pos.x - 100, pos.y, pos.z);
		}

		// Move new mission panel down.
        LeanTween.moveY (newMissionObject, 0f, 0.5f).setIgnoreTimeScale (true).setEase (LeanTweenType.linear)
			.setOnComplete (()=>{
				// Moves new mission in.
				foreach(var mission in newMissonList) {
					var pos = mission.transform.position;
					LeanTween.moveX (mission,pos.x + 100, 0.3f).setIgnoreTimeScale (true).setEase (LeanTweenType.linear);
				}
			});
    }

    private void updateMissions()
    {
        var missions = missionLogic.getNewMissions();
        missionLogic.saveMissionData();

        for (int i = 0; i < missions.Length; i++)
        {
            switch (i)
            {
                case 0:
                    missionOneToggle.isOn = missions[i].isNew;
                    missionOneText.text = missions[i].missionText;
                    createStars(missionOneFirstStarPos, missions[i].numberOfStars, missionOneToggle.transform.parent.gameObject);

					if (missions[i].isNew) {
                        newMissonList.Add(missionOneToggle.transform.parent.gameObject);
					}
                    break;
                case 1:
                    missionTwoToggle.isOn = missions[i].isNew;
                    missionTwoText.text = missions[i].missionText;
                    createStars(missionTwoFirstStarPos, missions[i].numberOfStars, missionTwoToggle.transform.parent.gameObject);

                    if (missions[i].isNew)
                    {
                        newMissonList.Add(missionTwoToggle.transform.parent.gameObject);
					}
                    break;
                case 2:
                    missionThreeToggle.isOn = missions[i].isNew;
                    missionThreeText.text = missions[i].missionText;
                    createStars(missionThreeFirstStarPos, missions[i].numberOfStars, missionThreeToggle.transform.parent.gameObject);

                    if (missions[i].isNew)
                    {
                        newMissonList.Add(missionThreeToggle.transform.parent.gameObject);
					}
                    break;
            }
        }
        missionLogic.unMarkMissionsNew();
    }
    private void createStars(GameObject position, int numberOfStars, GameObject parent)
    {
        Vector3 firstStarPos = position.transform.position;
        int i = 0;
        GameObject tempStar;
        while (i < numberOfStars)
        {
            tempStar = Instantiate(star, firstStarPos + new Vector3(i * 2.0f, 0, 0), Quaternion.identity) as GameObject;
            tempStar.transform.parent = parent.transform;
            i++;
        }
    }

    public void OnClickedNext()
    {
        Debug.Log("clicked next");

        //Move up and call next
        LeanTween.moveY(newMissionObject, -200f, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.linear); 
        nextEvent.handleEvent();
    }
    public void next()
    {
        throw new System.NotImplementedException();
    }

    public void setNext(PhaseEventHandler next)
    {
        nextEvent = next;
    }
}
