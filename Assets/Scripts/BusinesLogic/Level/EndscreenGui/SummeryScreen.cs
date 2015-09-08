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
    float timeToReciveCoins = 1.5f;
	//GUI
    //LOSE PANEL
    GameObject losePanel;

    //LOSE PANEL TEXT
    Text goldEarned;
    Text numOfKills;
    Text bestCombo;
    Text roundRank;
    Animator roundRankAnimator;
    Text roundRankText;
    GameObject roundRankObject;
    Text totalEarned;

    // special effects
    Animator newHighScore;
    Animator moneyCount;
    Animator goldCoin;

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
      //  goldEarned = GameObject.Find("LosePanel/TOTALEARNED/Num").GetComponent<Text>();
        numOfKills = GameObject.Find("LosePanel/KILLS/Num").GetComponent<Text>();
        bestCombo = GameObject.Find("LosePanel/COMBOBONUS/Num").GetComponent<Text>();
        roundRank = GameObject.Find("LosePanel/ROUNDRANK/rank").GetComponent<Text>();
        roundRankObject = GameObject.Find("LosePanel/ROUNDRANK/rank");

        totalEarned = GameObject.Find("LosePanel/TOTALEARNED/Num").GetComponent<Text>();

        // Special effects.
        newHighScore = GameObject.Find("LosePanel/NewHighScore").GetComponent<Animator>();
        roundRankAnimator = GameObject.Find("LosePanel/ROUNDRANK/rank").GetComponent<Animator>();
        roundRankText = GameObject.Find("LosePanel/ROUNDRANK/rank").GetComponent<Text>();
      //  goldCoin = GameObject.Find("LosePanel/LosePJ/PJS").GetComponent<Animator>();

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
        ShowKills();
        showBestCombo();
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
        //ShowGoldEarned();
 

        // pause for one sec
       // yield return new WaitForSeconds(1f);

        // change Kills to gold
        isAnimationPlaying = true;

        StartCoroutine(changeKillsToTotalEarned());

        // Wait until gold earned is exchanged
        while (isAnimationPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("finished to exchange kills with gold earned");

        // Shows round rank.
        calculateRoundRank();
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

      //  fitGoldCoin();
    }

    private void calculateRoundRank()
    {
        var kills = scoreLogic.kills;
        var combo = playerStats.getHighestCombo();
        var roundScore = kills * (combo * 0.3);
        if (roundScore < 15)
        {
            roundRankText.text = "F";
        }
        else if (roundScore < 25)
        {
            roundRankText.text = "D";
        }
        else if (roundScore < 35)
        {
            roundRankText.text = "C";
        }
        else if (roundScore < 45)
        {
            roundRankText.text = "C+";
        }
        else if (roundScore < 55)
        {
            roundRankText.text = "B";
        }
        else if (roundScore < 65)
        {
            roundRankText.text = "B+";
        }
        else if (roundScore < 75)
        {
            roundRankText.text = "A-";
        }
        else if (roundScore < 85)
        {
            roundRankText.text = "A";
        }
        else if (roundScore < 95)
        {
            roundRankText.text = "A+";
        }

       
    }

 

    /**
     * Puts the gold coin in place
     * */
    private void fitGoldCoin()
    {

        var camera = Camera.main;
        RectTransform rt = totalEarned.gameObject.GetComponent<RectTransform>();
        var canvas = totalEarned.gameObject.GetComponentInParent<Canvas>();
        Vector3 position = goldCoin.transform.position;
        position.x += ((rt.rect.width * canvas.scaleFactor)/2); // The important part!

        //// In my case, I needed to do this aswell, you probable don't need this in your setup and can just set rt.position with the input instead
        //Vector3 output;
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, input, camera, out output);
        goldCoin.transform.position = position;
    }

    private IEnumerator changeKillsToTotalEarned()
    {
        Debug.Log("got to change kills");
        var kills = scoreLogic.kills;
        int i = 0;
        var totalEarnedTemp = totalEarnedDisplayedSum;
        var timeStartedToChangeScore = Time.realtimeSinceStartup;
        int coinsPerKill = currencyLogic.getCoinsPerKill();
        Debug.Log("started at: " + Time.realtimeSinceStartup);
        while (i < kills)
        {
            // TODO: music!
            float deltaTime = Time.realtimeSinceStartup - timeStartedToChangeScore;
            //if (deltaTime > timeToReciveCoins)
            //{
            //    deltaTime = timeToReciveCoins;
            //}
            //i = (int)(kills * calculateEasing(deltaTime, timeToReciveCoins));
            i = (int)(Easing.cubicInOut(deltaTime, 0, kills, timeToReciveCoins));
            
            totalEarnedDisplayedSum = totalEarnedTemp + (i) * coinsPerKill; // 2 coins for each kill

            var displayedTotal = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "{0:0,0}", totalEarnedDisplayedSum);
            var killsDisplayed = string.Format(System.Globalization.CultureInfo.InvariantCulture,
            "{0:0,0}", kills - i);
          //  Debug.Log("kills is : " + kills);
           // Debug.Log("i is : " + i);

            totalEarned.text = string.Format("{0}", displayedTotal);
            numOfKills.text = string.Format("{0}", killsDisplayed);
            yield return null; // wait one frame
        }
        Debug.Log("finished at: " + Time.realtimeSinceStartup);
        Debug.Log("total time is : " + (Time.realtimeSinceStartup - timeStartedToChangeScore));
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

            totalEarned.text = string.Format("{0}", displayedTotal);
            goldEarned.text = string.Format("{0}", gold);
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
        bestCombo.text = string.Format("{0}", displayedCombo);

    }

    /**
     * write the number of kills this round
     * */
    private void ShowKills()
    {
        var numberOfKills = scoreLogic.kills;
        var displayedKills = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                         "{0:0,0}", numberOfKills);
        numOfKills.text = string.Format("{0}", displayedKills);

        // TODO: do somthing with this information
        //check if new high score and save
        bool isNewHighScore = scoreLogic.getHighScore() < numberOfKills;
        scoreLogic.saveScoreData(); // Saving here because it destroys the high score check.
    }

    /**
     * write the gold earned this round
     * */
    private void ShowGoldEarned()
    {
        var coinsEarned = currencyLogic.getGoldEarned();
        var displayedGold = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                         "{0:0,0}", coinsEarned);
        goldEarned.text = string.Format("{0}", displayedGold);
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
        return 1f - Easing.easeInOut(deltaTime, totalTime);
    }

}
