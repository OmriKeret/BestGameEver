using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiAdjuster : MonoBehaviour {


    // Logic
    MissionLogic missionLogic;

    // shild
    public Sprite bronzeShild;
    public Sprite silverShild;
    public Sprite goldShild;

    // Panels
    public GameObject smallPanel;
    public GameObject bigPanel;
    public float panelDistanceFromHeader = 10;
    public float panelDistanceFromCompletedMission = 10;
    public float nextBtnDistanceFromCompletedMission = 10;

    // New rank panel.
    GameObject newRankFirstStarPos;
    GameObject newRankPanelObject;
    GameObject newRank;


    // Finished mission rank Panel.
    GameObject finishedMissionRankPanel;
    GameObject finishedMissionFirstStarPos;
    GameObject finishedMissionHeader;
    GameObject finishedMissionCompletedMission;
    GameObject finishedMissionNextBtnPos;
    GameObject finishedMissionNextBtn;

    // Finished mission Armor.
    Text rankTitle;
    Text rankLevel;
    Image finishedMissionArmor;

    // summaryScreen Armor
    Text summaryShildTitle;
    Text summaryLevel;
    Image summaryLevelArmor;

	// Use this for initialization
	void Start () {
	
        // Mission logic.
        missionLogic = this.gameObject.GetComponentInParent<MissionLogic>();

		// New rank panel.
		newRankFirstStarPos = GameObject.Find ("LevelUp/NewLevel/RankStarsPanel/FirstStarPos");
		newRankPanelObject = GameObject.Find ("LevelUp/NewLevel/RankStarsPanel");
		newRank = GameObject.Find ("LevelUp/NewLevel");

        // Finished mission rank Panel.
        finishedMissionRankPanel = GameObject.Find("MissionComplete/RankPanel/RankStarsPanel");
        finishedMissionFirstStarPos = GameObject.Find("Canvas/MissionComplete/RankPanel/RankStarsPanel/FirstStarPos");

        // Finished mission adjusting parameters
        finishedMissionHeader = GameObject.Find("MissionComplete/Header");
        finishedMissionCompletedMission = GameObject.Find("MissionComplete/CompletedMission");
		finishedMissionNextBtnPos = GameObject.Find("MissionComplete/NextBtnPos");
		finishedMissionNextBtn = GameObject.Find("MissionComplete/NextButton");

        // Finished mission armor.
        rankTitle = GameObject.Find("Canvas/MissionComplete/Armor/Title").GetComponent<Text>();
        rankLevel = GameObject.Find("Canvas/MissionComplete/Armor/Level").GetComponent<Text>();
        finishedMissionArmor = GameObject.Find("Canvas/MissionComplete/Armor").GetComponent<Image>();

        // Summary armor.
        summaryShildTitle = GameObject.Find("Canvas/LosePanel/Armor/Title").GetComponent<Text>();
		summaryLevel = GameObject.Find("LosePanel/Armor/Level").GetComponent<Text>();
        summaryLevelArmor = GameObject.Find("Canvas/LosePanel/Armor").GetComponent<Image>();

	}

    public void initiateShilds()
    {
        //TODO: check player level and set shilds accordingly
        var level = missionLogic.getTier();
        var levelTitle = missionLogic.getTierTitle();

        if (level <= 3)
        {
            // Bronze shild.
            summaryLevelArmor.sprite = bronzeShild;
            finishedMissionArmor.sprite = bronzeShild;
        }
        else if (level <= 6)
        {
            // Silver shild.
            summaryLevelArmor.sprite = silverShild;
            finishedMissionArmor.sprite = silverShild;
        }
        else
        {
            // Gold shild.
            summaryLevelArmor.sprite = goldShild;
            finishedMissionArmor.sprite = goldShild;
        }

        // Set level number
        summaryLevel.text = string.Format("{0}", level);
        rankLevel.text = string.Format("{0}", level);

        // Set level title.
        rankTitle.text = levelTitle;
        summaryShildTitle.text = levelTitle;
    }

    public void updateShilds()
    {
        initiateShilds();
    }

    public void initiatePanels()
    {
        initiateFinishedMissionPanel();
        initiateLevelUpPanel();
    }

    private void initiateLevelUpPanel()
    {
        var starsInLevel = missionLogic.getNextRankStars();
        GameObject prefab = smallPanel;
        if (starsInLevel <= 4)
        {
            // Small panel
            // initiate small panel and set finishedMission rank panel as its parent.
            prefab = smallPanel;
        }
        else
        {
            prefab = bigPanel;
        }

        GameObject newLevelRankPanel = Instantiate(prefab, newRankPanelObject.transform.position, Quaternion.identity) as GameObject;
        newLevelRankPanel.GetComponent<RectTransform>().parent = newRankPanelObject.GetComponent<RectTransform>().parent;
        newLevelRankPanel.name = newRankPanelObject.name;
		newLevelRankPanel.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
        GameObject.Destroy(newRankPanelObject);
    }

    private void initiateFinishedMissionPanel()
    {
        // TODO: initiate the panels and make room for them
        var starsInLevel = missionLogic.getRankStars().Length;
        GameObject prefab = smallPanel;
        if (starsInLevel <= 4)
        {
            // Small panel
            // initiate small panel and set finishedMission rank panel as its parent.
            prefab = smallPanel;
        }
        else
        {
            prefab = bigPanel;
        }

        GameObject finishedMissionPanel = Instantiate(prefab, finishedMissionRankPanel.transform.position, Quaternion.identity) as GameObject;
        finishedMissionPanel.GetComponent<RectTransform>().parent = finishedMissionRankPanel.GetComponent<RectTransform>().parent;
        finishedMissionPanel.name = finishedMissionRankPanel.name;
		finishedMissionPanel.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);

        GameObject.Destroy(finishedMissionRankPanel);
    }

    public void AdjustPanelsOnStart()
    {
        // find panel
        var finishedMissionPanel = GameObject.Find("MissionComplete/RankPanel/RankStarsPanel");
        // adjesting panel according to header
        var finishedMissionPanelHeight = finishedMissionPanel.GetComponent<RectTransform>().rect.height;
        var headerPos = finishedMissionHeader.GetComponent<RectTransform>().position;
        var panelAdjustedYPosition = headerPos.y - finishedMissionHeader.GetComponent<RectTransform>().rect.height / 2 - panelDistanceFromHeader - finishedMissionPanelHeight / 2;
        finishedMissionPanel.GetComponent<RectTransform>().position = new Vector3(headerPos.x, panelAdjustedYPosition, headerPos.z);

        // adjesting mission acoording to panel
        var panelPos = finishedMissionPanel.GetComponent<RectTransform>().position;
        var completedMissionAdjustedY = panelPos.y - finishedMissionPanelHeight / 2 - panelDistanceFromCompletedMission - finishedMissionPanelHeight / 2;
        finishedMissionCompletedMission.GetComponent<RectTransform>().position = new Vector3(panelPos.x, completedMissionAdjustedY, panelPos.z);

        // adjesting next btn
        var completedMissonPos = finishedMissionCompletedMission.GetComponent<RectTransform>().position;
        var nextBtnHeight = finishedMissionNextBtn.GetComponent<RectTransform>().rect.height;
        var nextBtnY = completedMissonPos.y - finishedMissionPanelHeight / 2 - panelDistanceFromCompletedMission - nextBtnHeight / 2;
        finishedMissionNextBtnPos.GetComponent<RectTransform>().position = new Vector3(completedMissonPos.x, completedMissionAdjustedY, completedMissonPos.z);
    }

    public float AdjustPanelsOnLevelUpAndReciveNewY()
    {
        // find panel
        var finishedMissionPanel = GameObject.Find("LevelUp/NewLevel/RankStarsPanel");
        // adjesting panel according to header
        var finishedMissionPanelHeight = finishedMissionPanel.GetComponent<RectTransform>().rect.height;
        var headerPos = finishedMissionHeader.GetComponent<RectTransform>().position;
        var panelAdjustedYPosition = headerPos.y - finishedMissionHeader.GetComponent<RectTransform>().rect.height / 2 - panelDistanceFromHeader - finishedMissionPanelHeight / 2;
       // finishedMissionPanel.GetComponent<RectTransform>().position = new Vector3(headerPos.x, panelAdjustedYPosition, headerPos.z);

        // adjesting mission acoording to panel
       // var panelPos = finishedMissionPanel.GetComponent<RectTransform>().position;
        var completedMissionAdjustedY = panelAdjustedYPosition - finishedMissionPanelHeight / 2 - panelDistanceFromCompletedMission - finishedMissionPanelHeight / 2;
        finishedMissionCompletedMission.GetComponent<RectTransform>().position = new Vector3(headerPos.x, completedMissionAdjustedY, headerPos.z);

        // adjesting next btn
        var completedMissonPos = finishedMissionCompletedMission.GetComponent<RectTransform>().position;
        var nextBtnHeight = finishedMissionNextBtn.GetComponent<RectTransform>().rect.height;
        var nextBtnY = completedMissonPos.y - finishedMissionPanelHeight / 2 - panelDistanceFromCompletedMission - nextBtnHeight / 2;
        finishedMissionNextBtnPos.GetComponent<RectTransform>().position = new Vector3(completedMissonPos.x, completedMissionAdjustedY, completedMissonPos.z);

        return panelAdjustedYPosition;
    }
}
