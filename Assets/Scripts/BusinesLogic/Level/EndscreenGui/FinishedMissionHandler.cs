using UnityEngine;
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

    // Rank Panel.
    GameObject rankPanel;
    Text rankTitle;
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

        // Rank Panel.
        rankPanel = GameObject.Find("MissionComplete/RankPanel");
        rankTitle =  GameObject.Find("MissionComplete/RankPanel/Title").GetComponent<Text>();
        rankFirstStar = GameObject.Find("MissionComplete/RankPanel/RankStarsPanel/FirstStarPos");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator run()
    {
        missions = missionLogic.getMissions();
        initiateRankStats();

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
		LeanTween.moveY (missionCompleteBtn.gameObject, CMObject.transform.position.y - 30f, 0.5f).setIgnoreTimeScale (true).setEase (LeanTweenType.linear);
		while (movingStarsAnimationIsPlaying)
		{
			yield return new WaitForSeconds(0.1f);
		}
        
        // After next button is clicked we move down the mission complete object
        LeanTween.moveY(CMObject, -200f, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.linear);
        LeanTween.moveY(CMObject, -200f, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.linear);
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
            rankProgressStars[i] = Instantiate(unCompletedStar, firstStarPos + new Vector3(i * 7.0f, 0, 0), Quaternion.identity) as GameObject;
            rankProgressStars[i].transform.parent = rankPanel.transform;
            i++;
        }
    }

    private IEnumerator processFinishedMission(MissionModel mission)
    {
        // should insert the mission ?
        isFirstMission = CMText.text.Equals("KILL THE BIG BAD DRAGON\n");

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
                CMObject = GameObject.Find("MissionComplete");
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
          CMProgressStars[i] = Instantiate(CMStar,firstStarPos + new Vector3(i * 3.0f,0,0),Quaternion.identity) as GameObject;
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
		movingStarsAnimationIsPlaying = false;
	}
}
