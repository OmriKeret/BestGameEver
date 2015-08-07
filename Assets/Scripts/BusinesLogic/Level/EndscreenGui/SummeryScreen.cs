using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SummeryScreen : MonoBehaviour, PhaseEventHandler {

    //Logic
    MissionLogic missionLogic;


	//GUI
    //LOSE PANEL
    GameObject losePanel;

    //LOSE PANEL TEXT
    Text goldEarned;
    Text numOfKills;
    Text bestCombo;
    Text roundRank;
    Animator roundRankAnimator;
    Text totalEarned;

    // special effects
    Animator newHighScore;
    Animator moneyCount;

    //controlls
    bool isAnimationPlaying;
	// Use this for initialization
	void Start () {
        losePanel = GameObject.Find("LosePanel");
        goldEarned = GameObject.Find("LosePanel/TOTALEARNED").GetComponent<Text>();
        numOfKills = GameObject.Find("LosePanel/KILLS").GetComponent<Text>();
        bestCombo = GameObject.Find("LosePanel/COMBOBONUS").GetComponent<Text>();
        roundRank = GameObject.Find("LosePanel/ROUNDRANK").GetComponent<Text>();
        roundRankAnimator = null;//TOO THIS

        totalEarned = GameObject.Find("LosePanel/TOTALEARNED").GetComponent<Text>();

        // special effects
        newHighScore = GameObject.Find("LosePanel/NewHighScore").GetComponent<Animator>();
        moneyCount = null;//TODO: this

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void handleEvent()
    {
        run();
    }

    private void run()
    {
        //brings down summary screen
        isAnimationPlaying = true;
        LeanTween.moveY(losePanel, 0f, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.linear).setOnComplete(
            () =>
            {
                isAnimationPlaying = false;
            });

        // Wait until summary screen is down
        StartCoroutine(WaitUntilFinish());
        StartCoroutine(ShowGoldEarned());
        
  


    }

    private IEnumerator ShowGoldEarned()
    {
        isAnimationPlaying = true;


    }

    private IEnumerator WaitUntilFinish()
    {
        while (isAnimationPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void next()
    {
        throw new System.NotImplementedException();
    }

    public void setNext(PhaseEventHandler next)
    {
        throw new System.NotImplementedException();
    }
}
