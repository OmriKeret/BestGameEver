using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUpHandler : MonoBehaviour, PhaseEventHandler {
    PhaseEventHandler nextEvent;
    FinishedMissionHandler finishedMission;

	// Logic
	MissionLogic missionLogic;

	// GUI

	GameObject levelUpTitle;
	// Control
	bool animationRun;
	// Mission complete
	GameObject missionCompleteObject;

	// black screen
	Animator blackBackground;
	GameObject levelup;

	// rank
	GameObject rankPanelObject;
	GameObject[] rankProgressStars;

	// new rank
	public Sprite newStarSprite;


	public GameObject unCompletedStar;
	GameObject newRankFirstStarPos;
	GameObject newRankPanelObject;
	GameObject newRank;
	GameObject[] newRankProgressStars;
	Text newRankTitle;
    Text newRankNum;

	// Reward.
	Text rewardText;
	GameObject reward;
	float timeToReciveCoins = 2f;
	float rewardSum;
	Button nextButton;

    // Gui adjustment
    GuiAdjuster guiAdjuster;

	// Use this for initialization

	void Start () {
		missionLogic = this.gameObject.GetComponentInParent<MissionLogic>();
        finishedMission = this.GetComponent<FinishedMissionHandler>();
		missionCompleteObject = GameObject.Find("MissionComplete");

		rankPanelObject = GameObject.Find ("MissionComplete/RankPanel");
		blackBackground = GameObject.Find("Canvas/DarkScreen").GetComponent<Animator>();
		levelup = GameObject.Find("LevelUp");
		levelUpTitle = GameObject.Find("LevelUp/Title");
		reward = GameObject.Find("LevelUp/Reward");
		rewardText = GameObject.Find("LevelUp/Reward/RewardText").GetComponent<Text>();
		nextButton = GameObject.Find ("LevelUp/Reward/NextButton").GetComponent<Button> ();

		// new rank
		newRankFirstStarPos = GameObject.Find ("LevelUp/NewLevel/RankStarsPanel/FirstStarPos");
		newRankPanelObject = GameObject.Find ("LevelUp/NewLevel/RankStarsPanel");
        newRankTitle = GameObject.Find("MissionComplete/Armor/Title").GetComponent<Text>();
        newRankNum = GameObject.Find("MissionComplete/Armor/Level").GetComponent<Text>();
		newRank = GameObject.Find ("LevelUp/NewLevel");

        guiAdjuster = this.gameObject.GetComponent<GuiAdjuster>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator run()
    {
        // reGet new rank paramaters (because they have been initiated)
        newRankFirstStarPos = GameObject.Find("LevelUp/NewLevel/RankStarsPanel/FirstStarPos");
        newRankPanelObject = GameObject.Find("LevelUp/NewLevel/RankStarsPanel");

		// Dark screen
		animationRun = true;
		blackBackground.SetBool("darkScreenSolid", true);
		initiateNewRankStats ();

		// Move level title down in.
		float levelUpTitleOriginalY = levelUpTitle.transform.position.y;

		// Remove from the mission complete hirearchi
		rankPanelObject.transform.parent = GameObject.Find("Canvas").transform;

		// Moves the title down
		LeanTween.moveY (levelUpTitle, 40f, 0.5f).setIgnoreTimeScale (true).setEase (LeanTweenType.linear).setOnComplete (
			() => {
			animationRun = false;
			});

		// Moves rank object near the level up title.
		LeanTween.moveY (rankPanelObject, 10f, 0.5f).setIgnoreTimeScale (true).setEase (LeanTweenType.linear).setOnComplete (
			() => {
			animationRun = false;
			});

		// Moves completed mission screen down.
		float MissionCompleteOriginalY = missionCompleteObject.transform.position.y;
        var missionCompleteOriginalPos = missionCompleteObject.transform.position;

		LeanTween.moveY (missionCompleteObject, -200f, 0.5f).setIgnoreTimeScale (true).setEase (LeanTweenType.linear).setOnComplete (
			() => {
			animationRun = false;
			});

		// Wait until finish to bring level up screen.
		while (animationRun) {
			yield return new WaitForSeconds(0.1f);
		}

		Debug.Log ("levelUp: everything in place");
		// Change Stars sprite.
		animationRun = true;
		foreach (var star in rankProgressStars)
		{
			//TODO: particle system.
			yield return new WaitForSeconds(0.2f);
			star.GetComponent<SpriteRenderer>().sprite = newStarSprite;
			Debug.Log("switching star");
		}

		var rewardOrigY = reward.transform.position.y;
        var levelPanelHeight = guiAdjuster.getHeight(GameObject.Find("RankPanel/RankStarsPanel").GetComponent<RectTransform>());
		//Brings reward up
        LeanTween.moveY(reward, rankPanelObject.transform.position.y - levelPanelHeight / 2f - 10f, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.linear).setOnComplete(
			() => {
				animationRun = false;
			});

		// Counts the reward.
		int i = 0;
		float timeStartedToChangeScore = Time.realtimeSinceStartup;
		int rewardSum = missionLogic.getRankCompleteReward ();
		//TODO: play sound
		Debug.Log ("levelUp: Starting to cash in");
		while (i < rewardSum)
		{
			// TODO: music!
			float deltaTime = Time.realtimeSinceStartup - timeStartedToChangeScore;
			if (deltaTime > timeToReciveCoins) 
			{
				deltaTime = timeToReciveCoins; 
			}
			i = (int)(rewardSum * calculateReward(deltaTime, timeToReciveCoins));
			rewardText.text = string.Format("REWARD: {0}", i);
			yield return null; // wait one frame
		}
		//TODO: particles
		Debug.Log ("finish to recive reward");
		yield return new WaitForSeconds (1f);


		// moves up the Next button and wait for a next click :)
		float nextButtonOrigY = nextButton.transform.position.y;
		LeanTween.moveY (nextButton.gameObject, reward.transform.position.y - 15f, 0.5f).setIgnoreTimeScale (true).setEase (LeanTweenType.linear);
		animationRun = true;
		while (animationRun) {
			yield return new WaitForSeconds(0.1f);
		}


		animationRun = true;

		//Move reward down and title up

		// Move next button down.
		LeanTween.moveY (reward, rewardOrigY, 0.3f).
			setIgnoreTimeScale (true).setEase (LeanTweenType.linear);

		// Move next button down.
		LeanTween.moveY (nextButton.gameObject, nextButtonOrigY, 0.3f).
			setIgnoreTimeScale (true).setEase (LeanTweenType.linear);

		// Moves LevelUpObject back to place (and title).
		LeanTween.moveY (levelUpTitle, levelUpTitleOriginalY, 0.3f).
			setIgnoreTimeScale (true).setEase (LeanTweenType.linear);

        // calculation set of missionComplete object
        var missionCompleteCurrentPos = missionCompleteObject.transform.position;
        missionCompleteObject.transform.position = missionCompleteOriginalPos;
        // Adjust finished mission stars panel.
        float rankPanelOrignalY = guiAdjuster.AdjustPanelsOnLevelUpAndReciveNewY();

        missionCompleteObject.transform.position = missionCompleteCurrentPos;
		StartCoroutine(moveNewMissionIn (rankPanelOrignalY, MissionCompleteOriginalY));

		while (animationRun) {
			yield return new WaitForSeconds(0.1f);
		}

		blackBackground.SetBool("darkScreenSolid", false);
      Debug.Log("finished waiting");
		finishedMission.finishedLevelingUp(newRankProgressStars);
	}

	public void handleEvent()
	{
	}

	public void handleEvent(GameObject[] rankProgressStars)
    {
		this.rankProgressStars = rankProgressStars;
        StartCoroutine(run());
    }

	private float calculateReward(float deltaTime, float totalTime)
	{
		return Easing.easeInOut(deltaTime, totalTime);

	}

    public void next()
    {
        throw new System.NotImplementedException();
    }

    public void setNext(PhaseEventHandler next)
    {
        nextEvent = next;
    }



	public void OnClickRewardNext ()
	{
		animationRun = false;
	}

	IEnumerator moveNewMissionIn (float rankPanelOrignalY, float MissionCompleteOriginalY)
	{
		var animationIsRunning = true;

		// Insert new title and remove old title.
        LeanTween.moveX(newRankPanelObject, rankPanelObject.transform.position.x, 0.3f).
			setIgnoreTimeScale (true).setEase (LeanTweenType.linear);
		LeanTween.moveX (rankPanelObject, -200f, 0.3f).
			setIgnoreTimeScale (true).setEase (LeanTweenType.linear).setOnComplete (
				() => {
				animationIsRunning = false;
			});

		while (animationIsRunning) {
			yield return new WaitForSeconds(0.1f);
		}

		animationIsRunning = true;
		// unDark the screen
		blackBackground.SetBool("darkScreenSolid", false);

        LeanTween.moveY(newRankPanelObject, rankPanelOrignalY, 0.3f).
			setIgnoreTimeScale (true).setEase (LeanTweenType.linear);

		LeanTween.moveY (missionCompleteObject, MissionCompleteOriginalY, 0.3f).
			setIgnoreTimeScale (true).setEase (LeanTweenType.linear).setOnComplete (
				() => {
				//TODO: particles
				animationIsRunning = false;
			});
		 
		while (animationIsRunning) {
			yield return new WaitForSeconds(0.1f);
		}

        // Set new rank as mission complete sybling.
        newRank.transform.parent = missionCompleteObject.transform;

		animationRun = false; // release lock on leveling up

	}

	private void initiateNewRankStats()
	{
		newRankTitle.text = missionLogic.getTierTitle();
        newRankNum.text = string.Format("{0}", missionLogic.getTier());
		newRankProgressStars = new GameObject[missionLogic.getRankStars().Length];
		Vector3 firstStarPos =  newRankFirstStarPos.transform.position;
		int i = 0;
		while (i < newRankProgressStars.Length)
		{
			newRankProgressStars[i] = Instantiate(unCompletedStar, firstStarPos + new Vector3(i * 18.0f, 0, 0), Quaternion.identity) as GameObject;
			newRankProgressStars[i].transform.parent = newRankPanelObject.transform;
			i++;
		}
	}
}
