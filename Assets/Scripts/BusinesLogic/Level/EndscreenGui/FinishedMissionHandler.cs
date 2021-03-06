﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinishedMissionHandler : MonoBehaviour, PhaseEventHandler{

    // event queue
    PhaseEventHandler nextEvent;

    // level up
    LevelUpHandler levelupHandler;
    bool levelingUp; 

    // Missions
    MissionLogic missionLogic;
    MissionModel[] missions;


    // GUI.
    // Mission complete panel
    GameObject missionComplete;

    // Mission complete object and components.
    GameObject CMObject;
    GameObject CMFirstStar;
    Text CMText;
    GameObject[] CMProgressStars;
    public GameObject CMStar;

	Button missionCompleteBtn;
    Vector3 btnFinalPos;

    // Rank Panel.
    GameObject rankPanel;
    Text rankTitle;
    Text rankLevel;

    GameObject rankFirstStar;
    GameObject[] rankProgressStars;

    public GameObject completedStar;
    public GameObject unCompletedStar;
    public Sprite rankCompletedStar;

    // logic
    bool isFirstMission;

    // Timing param
    bool movingStarsAnimationIsPlaying = false;
	// Use this for initialization
	void Start () {
        
        // Logic.
        missionLogic = this.gameObject.GetComponentInParent<MissionLogic>();
        levelupHandler = this.gameObject.GetComponent<LevelUpHandler>();

        // GUI.
        // Mission complete Panel
        missionComplete = GameObject.Find("MissionComplete");

        // Completed Mission Object.
        CMObject = GameObject.Find("MissionComplete/CompletedMission");
        CMFirstStar = GameObject.Find("MissionComplete/CompletedMission/firstStarPos");
        CMText = GameObject.Find("MissionComplete/CompletedMission/MissionCompleteText").GetComponent<Text>();
		missionCompleteBtn = GameObject.Find("MissionComplete/NextButton").GetComponent<Button>();
        btnFinalPos = GameObject.Find("MissionComplete/NextBtnPos").transform.position;

        // Rank Panel.
        rankPanel = GameObject.Find("MissionComplete/RankPanel");
        rankTitle =  GameObject.Find("Canvas/MissionComplete/Armor/Title").GetComponent<Text>();
        rankLevel = GameObject.Find("Canvas/MissionComplete/Armor/Level").GetComponent<Text>();
      
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator run()
    {
        rankFirstStar = GameObject.Find("Canvas/MissionComplete/RankPanel/RankStarsPanel/FirstStarPos"); 
        missions = missionLogic.getMissions();
        initiateRankStats();
        isFirstMission = true;
        foreach (var mission in missions)
        {
            if (mission.isFinished)
            {
                movingStarsAnimationIsPlaying = true;
                StartCoroutine(processFinishedMission(mission));
                Debug.Log("passed proccess mission");
                // Move mission in.

            }

            while (movingStarsAnimationIsPlaying)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
		movingStarsAnimationIsPlaying = true;


		Debug.Log("finished all mission");
		// moving next button up
        btnFinalPos = GameObject.Find("MissionComplete/NextBtnPos").transform.position;
        LeanTween.moveY(missionCompleteBtn.gameObject, btnFinalPos.y, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.linear);
		while (movingStarsAnimationIsPlaying)
		{
			yield return new WaitForSeconds(0.1f);
		}
        
        // After next button is clicked we move down the mission complete object
        LeanTween.moveY(missionComplete, -200f, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.linear);
      //  LeanTween.moveY(CMObject, -200f, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.linear);
		nextEvent.handleEvent ();
	}

    private void initiateRankStats()
    {
        rankTitle.text = missionLogic.getTierTitle(); ; 
        rankProgressStars = new GameObject[missionLogic.getRankStars().Length];
        Vector3 firstStarPos = rankFirstStar.transform.position;
        int i = 0;
        while (i < rankProgressStars.Length)
        {
            var downARow = i >= 4 ? 1 : 0;
            rankProgressStars[i] = Instantiate(unCompletedStar, firstStarPos + new Vector3((i  % 4) * 13f, downARow * -11f, 0), Quaternion.identity) as GameObject;
            rankProgressStars[i].transform.parent = rankPanel.transform;
            i++;
        }
        var fillTill = missionLogic.getFirstMissingStarIndex();

        for (int j = 0; j < fillTill; j++)
        {
            rankProgressStars[j].GetComponent<SpriteRenderer>().sprite = rankCompletedStar;
        }
    }

    private IEnumerator processFinishedMission(MissionModel mission)
    {
        // should insert the mission ?

        if (!isFirstMission)
        {
            bool animationRuns = true;
            Debug.Log("not first mission, start moving mission");
            var originalX = CMObject.transform.position.x;
            LeanTween.moveX(CMObject, 100f, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInExpo).setOnComplete(
                () =>
                {
                    CMObject.transform.position = new Vector3(-200f, CMObject.transform.position.y, CMObject.transform.position.z);
                    initilizeMissionData(mission);
                    LeanTween.moveX(CMObject, originalX, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInExpo).setOnComplete(
                        () =>
                        {
                            animationRuns = false;
                        });
                });
            while (animationRuns)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            isFirstMission = false;
            initilizeMissionData(mission);
        }
        
        StartCoroutine(moveStarsToRankBar());


        Debug.Log("continue running even function did not finish");
        // wait for darkScreen to come up
        
    }

    private IEnumerator moveStarsToRankBar()
    {
        var i = missionLogic.getFirstMissingStarIndex();
        Debug.Log("first index is " + i);
        Debug.Log("completed mission stars length is " + CMProgressStars.Length);
        foreach (var star in CMProgressStars)
        {
            bool animationRuns = true;
            LeanTween.move(star, rankProgressStars[i].transform.position, 1f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInCubic).setOnComplete(
                () =>
                {
                    rankProgressStars[i].GetComponent<SpriteRenderer>().sprite = rankCompletedStar;
                    Destroy(star);
                    //TODO: PARTICLE SYSTEM
                    animationRuns = false;
                });

            // Waiting to star move animation to end.
            while (animationRuns)
            {
                yield return null;
            }

            // Mission logic will tell us if we leveled up
            if (missionLogic.addRankStar())
            {
                levelingUp = true;
				levelupHandler.handleEvent(rankProgressStars);

                while (levelingUp)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                missionComplete = GameObject.Find("MissionComplete");
  //              Debug.Log("finished level up");
				// TODO: reset 
				i = missionLogic.getFirstMissingStarIndex();
			} else { 
           		i++;
			}
        //    Debug.Log("iteration number is " + i);
        }
        movingStarsAnimationIsPlaying = false;
    }

    /**
     * Initilize the mission text and stars
     * */
    private void initilizeMissionData(MissionModel mission)
    {
        CMText.text = mission.missionText;
        CMProgressStars = new GameObject[mission.numberOfStars];
        Vector3 firstStarPos = CMFirstStar.transform.position;
        for (int i = 0; i < mission.numberOfStars; i++)
        {

          CMProgressStars[i] = Instantiate(CMStar,firstStarPos + new Vector3(i * 3.2f,0,0),Quaternion.identity) as GameObject;
          CMProgressStars[i].transform.parent = CMObject.transform;
        }

    }

    public void handleEvent()
    {
        missionComplete.transform.position = new Vector3(0, 0, missionComplete.transform.position.z);
		Time.timeScale = 0f;
		Debug.Log ("time scale is " + Time.timeScale);
        StartCoroutine(run());
      
    }

    public void next()
    {
        this.nextEvent.handleEvent();
    }

    public void setNext(PhaseEventHandler next)
    {
        this.nextEvent = next;
    }

	internal void finishedLevelingUp(GameObject[] newRankProgressStars)
	{
		this.rankProgressStars = newRankProgressStars;
        levelingUp = false;
    }

	public void onNextButtonClicked() 
	{
        Debug.Log("Click next btn");
		movingStarsAnimationIsPlaying = false;
	}
}
