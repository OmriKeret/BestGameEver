using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DeathLogic : MonoBehaviour {
    public float timeToOpenDeathMenu = 1f;
    MovmentLogic movmentLogic;
    PlayerStatsLogic playerStatsLogic;
    MissionLogic missionLogic;
    ScoreLogic scoreLogic;
    TouchInterpeter touch;
    SoundLogic soundLogic;
    CurrencyLogic currencyLogic;

    AnimationLogic animationLogic;
    Text deathScore;
    GameObject losePanel;
    Vector3 OrigPos;
    Vector3 EndPos;
    public float delayToMoveDeathPanelUp = 0.4f;
    public float timeToEnterMission = 0.2f;
    public float delayBetweenMissionRemovalToEnterance = 0.2f;
    public float timeToRemoveMission = 0.2f;
    float origMissionTextX;
    float EndMissionTextX;
    public Text missionTitle;
    public Text missionTitleNew;
    public InternalMissionModel[] missionsToggleAndText;
    public InternalMissionModel[] deathMissionsToggleAndText;
    public InternalMissionModel[] deathMissionsToggleAndTextNew;
    //pause menu
    Button pauseBtn;
    Button superPower;
    //score
    private int scoreBegin;
    private int scoreEnd;
    private int currentHighScore;
    private bool changeScoreText = false;
    private bool newHighScore;
    private float finalAchivedScore;
    private int bonus;
    private Animator HighScore;
    Animator bonusAnimator;
    Text bonusText;
    private float timeStartedToChangeScore;
    private float TimeToScoreUp = 3f;
    private float displayedScore;
    // currency
    public float timeToWaitFromScoreLastUpdateToCurrencyChange = 1f;
    private float lastScoreUpdate;
    int addedCurrency;
    int currencyDiviser;
    bool shouldWriteCurrency;
    private Text currencyText;
    float currentDisplayedCurrency;
    private float TimeToCashIn = 2f;
	private float TimeToCashInChange;
    private float cashInDeviser;
    private float deviserRatio;
    private float timeSinceStartedCurrency;
    private float currentScore;
    private GameObject particles;

    Animator darkScreen;
    
    // New mission system
    EndscreenGuiMain endScreen;
	// Initialization
    void Start()
    {
        endScreen = this.GetComponentInChildren<EndscreenGuiMain>();
        animationLogic = this.gameObject.GetComponent<AnimationLogic>();
        HighScore = null;
        bonusAnimator = null;
        pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
        soundLogic = this.gameObject.GetComponent<SoundLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        losePanel = GameObject.Find("LosePanel");
        OrigPos = losePanel.transform.position;// new Vector3(0, 30, 0);
        EndPos = new Vector3(OrigPos.x, 35, OrigPos.z);
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
        movmentLogic = this.gameObject.GetComponent<MovmentLogic>();
        playerStatsLogic = this.gameObject.GetComponent<PlayerStatsLogic>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        scoreLogic = this.gameObject.GetComponent<ScoreLogic>();
        currencyLogic = this.gameObject.GetComponent<CurrencyLogic>();
        darkScreen = GameObject.Find("Canvas/DarkScreen").GetComponent<Animator>();
		deathScore = GameObject.Find("LosePanel/LoseScore").GetComponent<Text>();
        currencyText = GameObject.Find("LosePanel/LosePJ").GetComponent<Text>();
        timeSinceStartedCurrency = -1;
		TimeToCashInChange = TimeToCashIn ;
        particles = GameObject.Find("LosePanel/LosePJ/Particles");
        timeStartedToChangeScore = -1;
        superPower = GameObject.Find("Canvas/SuperHit").GetComponent<Button>();
       
    }

    void Update()
    {
    }


    public void DeathByFall()
    {
        if (playerStatsLogic.removeHp(1))
        {
            
            DeathScreen();
            return;
        }
        movmentLogic.MoveOnFallDeath();
		playerStatsLogic.resetDash ();
    }


    //the death screen
    private void DeathScreen()
    {
        saveCurrencyAndProgress();
        endScreen.initiateDeathScreen();

    }

    private void saveCurrencyAndProgress()
    {
        int reward = 0;
        scoreLogic.saveScoreData();
        if (missionLogic.didLevelUp())
        {
            reward = missionLogic.getRankCompleteReward();    
        }
        currencyLogic.updateCurrencyByKillsAndLoot(scoreLogic.kills, currencyLogic.getGoldEarned() + reward);

        // pre levelUp save.
        missionLogic.saveMissionData();
    }


    public void Reset()
    {
		Time.timeScale = 1;
        AutoFade.LoadLevel(Application.loadedLevel, 2, 1, Color.white);
    }


    internal void playerDie(int sign)
    {
        GameObject.Find("PlayerManager").GetComponent<Collider2D>().enabled = false;
        touch.SetDisableMovment();
        animationLogic.playerDie();
        pauseBtn.interactable = false;
        superPower.interactable = false;
        soundLogic.playDeathSound();
        movmentLogic.movePlayerDie(sign);
        DeathScreen();
    }
}
