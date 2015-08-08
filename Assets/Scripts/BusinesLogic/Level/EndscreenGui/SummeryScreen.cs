using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SummeryScreen : MonoBehaviour, PhaseEventHandler {

    //Logic
    MissionLogic missionLogic;
    CurrencyLogic currencyLogic;
    ScoreLogic scoreLogic;
    PlayerStatsLogic playerStats;
    int totalEarnedDisplayedSum = 0;
    float timeToReciveCoins = 1f;
	//GUI
    //LOSE PANEL
    GameObject losePanel;

    //LOSE PANEL TEXT
    Text goldEarned;
    Text numOfKills;
    Text bestCombo;
    Text roundRank;
    Animator roundRankAnimator;
    GameObject roundRankObject;
    Text totalEarned;

    // special effects
    Animator newHighScore;
    Animator moneyCount;

    //controlls
    bool isAnimationPlaying;
	// Use this for initialization
	void Start () {

        // Logic.
        missionLogic = this.gameObject.GetComponentInParent<MissionLogic>();
        currencyLogic = this.gameObject.GetComponentInParent<CurrencyLogic>();
        scoreLogic = this.gameObject.GetComponentInParent<ScoreLogic>();
        playerStats = this.gameObject.GetComponentInParent<PlayerStatsLogic>();

        // Gui.
        losePanel = GameObject.Find("LosePanel");
        goldEarned = GameObject.Find("LosePanel/TOTALEARNED").GetComponent<Text>();
        numOfKills = GameObject.Find("LosePanel/KILLS").GetComponent<Text>();
        bestCombo = GameObject.Find("LosePanel/COMBOBONUS").GetComponent<Text>();
        roundRank = GameObject.Find("LosePanel/ROUNDRANK").GetComponent<Text>();
        roundRankObject = GameObject.Find("LosePanel/ROUNDRANK/rank");

        totalEarned = GameObject.Find("LosePanel/TOTALEARNED").GetComponent<Text>();

        // Special effects.
        newHighScore = GameObject.Find("LosePanel/NewHighScore").GetComponent<Animator>();
        roundRankAnimator = GameObject.Find("LosePanel/ROUNDRANK/rank").GetComponent<Animator>();
        moneyCount = null;//TODO: this

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void handleEvent()
    {
        StartCoroutine(run());
    }

    private IEnumerator run()
    {
        Debug.Log("started");
  
        //brings down summary screen
        isAnimationPlaying = true;
        LeanTween.moveY(losePanel, 0f, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.linear).setOnComplete(
            () =>
            {
                isAnimationPlaying = false;
            });

        // Wait until summary screen is down
        while (isAnimationPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        ShowGoldEarned();
        ShowKills();
        showBestCombo();

        // Shows round rank.
        isAnimationPlaying = true;
        roundRankAnimator.SetTrigger("show");
        var mainCamera = Camera.main;
        var roundRankoriginalPos = roundRankObject.transform.position;
        roundRankObject.transform.position = mainCamera.transform.position;
        LeanTween.move(roundRankObject, roundRankoriginalPos, 0.7f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInCirc).setOnComplete(
           () =>
           {
               isAnimationPlaying = false;
           });

        // Wait until round rank is showen
        while (isAnimationPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("finished rotation animation");
        // change gold earned to gold
        isAnimationPlaying = true;
       
        StartCoroutine(changeCoinsEarnedToTotalEarned());

        // Wait until gold earned is exchanged
        while (isAnimationPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("finished to exchange gold earned");

        // change Kills to gold
        isAnimationPlaying = true;

        StartCoroutine(changeKillsToTotalEarned());

        // Wait until gold earned is exchanged
        while (isAnimationPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("finished to exchange gold earned");
  


    }

    private IEnumerator changeKillsToTotalEarned()
    {
        Debug.Log("got to change kills");
        var kills = 10;//scoreLogic.score;
        int i = kills;
        var totalEarnedTemp = totalEarnedDisplayedSum;
        var timeStartedToChangeScore = Time.realtimeSinceStartup;
        while (i > 0)
        {
            // TODO: music!
            float deltaTime = Time.realtimeSinceStartup - timeStartedToChangeScore;
            if (deltaTime > timeToReciveCoins)
            {
                deltaTime = timeToReciveCoins;
            }
            i = (int)(kills * calculateEasing(deltaTime, timeToReciveCoins));
            totalEarnedDisplayedSum = totalEarnedTemp + (kills - i) * 2; // 2 coins for each kill

            var displayedTotal = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "{0:0,0}", totalEarnedDisplayedSum);
            var killsDisplayed = string.Format(System.Globalization.CultureInfo.InvariantCulture,
            "{0:0,0}", i);
            Debug.Log("kills is : " + kills);
            Debug.Log("i is : " + i);
            totalEarned.text = string.Format("TOTAL EARNED: {0}", displayedTotal);
            numOfKills.text = string.Format("KILLS: {0}", killsDisplayed);
            yield return null; // wait one frame
        }
        Debug.Log("total kills: " + (kills - i));
        isAnimationPlaying = false;
    }

    /**
     * Changes the amount given to gold
     * */
    private IEnumerator changeCoinsEarnedToTotalEarned()
    { 
        var coinsEarned = currencyLogic.getGoldEarned();
        int i = coinsEarned;
        var totalEarnedTemp = totalEarnedDisplayedSum;
        var timeStartedToChangeScore = Time.realtimeSinceStartup;
        while (i > 0)
        {
            Debug.Log("i is " + i);
            // TODO: music!
            float deltaTime = Time.realtimeSinceStartup - timeStartedToChangeScore;
            if (deltaTime > timeToReciveCoins)
            {
                deltaTime = timeToReciveCoins;
            }
            i = (int)(coinsEarned * calculateEasing(deltaTime, timeToReciveCoins));
            totalEarnedDisplayedSum = totalEarnedTemp + coinsEarned - i;

            var displayedTotal = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "{0:0,0}", totalEarnedDisplayedSum);
            var gold = string.Format(System.Globalization.CultureInfo.InvariantCulture,
            "{0:0,0}", coinsEarned - i);

            totalEarned.text = string.Format("TOTAL EARNED: {0}", displayedTotal);
            goldEarned.text = string.Format("GOLD EARNED: {0}", gold);
            yield return null; // wait one frame
        }
        isAnimationPlaying = false;
    }

    /**
      * write the best combo
      * */
    private void showBestCombo()
    {
        var combo = playerStats.getHighestCombo();
        var displayedCombo = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                         "{0:0,0}", combo);
        bestCombo.text = string.Format("BEST COMBO:   {0}", displayedCombo);
    }

    /**
     * write the number of kills this round
     * */
    private void ShowKills()
    {
        var numberOfKills = scoreLogic.score;
        var displayedKills = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                         "{0:0,0}", numberOfKills);
        numOfKills.text = string.Format("KILLS:   {0}", displayedKills);
    }

    /**
     * write the gold earned this round
     * */
    private void ShowGoldEarned()
    {
        var coinsEarned = currencyLogic.getGoldEarned();
        var displayedGold = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                         "{0:0,0}", coinsEarned);
        goldEarned.text = string.Format("GOLD EARNED:   {0}", displayedGold);
    }

    public void next()
    {
        throw new System.NotImplementedException();
    }

    public void setNext(PhaseEventHandler next)
    {
        throw new System.NotImplementedException();
    }

    private float calculateEasing(float deltaTime, float totalTime)
    {
        return 1 - Easing.easeInOut(deltaTime, totalTime);

    }
}
