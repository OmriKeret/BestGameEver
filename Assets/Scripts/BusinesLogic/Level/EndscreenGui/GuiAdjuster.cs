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

        //newMissions armor.
    Text newMissionShildTitle;
    Text newMission;
    Image newMissionArmor;

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

        //newMissions armor.
        newMissionShildTitle = GameObject.Find("Canvas/NewMissions/Armor/Title").GetComponent<Text>();
        newMission = GameObject.Find("Canvas/NewMissions/Armor/Level").GetComponent<Text>();
        newMissionArmor = GameObject.Find("Canvas/NewMissions/Armor").GetComponent<Image>();

        initiatePanels();
        initiateShilds();
	}

    public void initiateShilds()
    {
        //TODO: check player level and set shilds accordingly
        var level = missionLogic.getTier();
        var levelTitle = missionLogic.getTierTitle();
       
        Debug.Log("level is :" + level);
        if (level <= 3)
        {
            // Bronze shild.
            summaryLevelArmor.sprite = bronzeShild;
            finishedMissionArmor.sprite = bronzeShild;
            newMissionArmor.sprite = bronzeShild;
        }
        else if (level <= 8)
        {
            // Silver shild.
            summaryLevelArmor.sprite = silverShild;
            finishedMissionArmor.sprite = silverShild;
            newMissionArmor.sprite = silverShild;
        }
        else
        {
            // Gold shild.
            summaryLevelArmor.sprite = goldShild;
            finishedMissionArmor.sprite = goldShild;
            newMissionArmor.sprite = goldShild;
        }

        // Set level number
        summaryLevel.text = string.Format("{0}", level);
        rankLevel.text = string.Format("{0}", level);
        newMission.text = string.Format("{0}", level);

        // Set level title.
        rankTitle.text = levelTitle;
        summaryShildTitle.text = levelTitle;
        newMissionShildTitle.text = levelTitle;
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
		newLevelRankPanel.transform.parent = finishedMissionRankPanel.transform.parent;
		newLevelRankPanel.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
		newLevelRankPanel.transform.parent = newRankPanelObject.transform.parent;
        newLevelRankPanel.name = newRankPanelObject.name;
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
		finishedMissionPanel.transform.parent = finishedMissionRankPanel.transform.parent;
        finishedMissionPanel.name = finishedMissionRankPanel.name;
		finishedMissionPanel.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);

        GameObject.Destroy(finishedMissionRankPanel);
    }

    public void AdjustPanelsOnStart()
    {
        // find panel
        var finishedMissionPanel = GameObject.Find("MissionComplete/RankPanel/RankStarsPanel");

        // adjesting panel according to header
        var finishedMissionPanelHeight = getHeight(finishedMissionPanel.GetComponent<RectTransform>());
        var headerPos = finishedMissionHeader.transform.position;
        var panelAdjustedYPosition = headerPos.y - getHeight(finishedMissionHeader.GetComponent<RectTransform>()) / 2 - finishedMissionPanelHeight / 2 - panelDistanceFromHeader;
        finishedMissionPanel.transform.position = new Vector3(headerPos.x, panelAdjustedYPosition, headerPos.z);

        // adjesting mission acoording to panel
        var finishedMissionHeigt = getHeight(finishedMissionCompletedMission.GetComponent<RectTransform>());
        var finishedMissionAdjustedY = panelAdjustedYPosition - finishedMissionHeigt / 2 - finishedMissionPanelHeight / 2 - panelDistanceFromCompletedMission;
        finishedMissionCompletedMission.transform.position = new Vector3(headerPos.x, finishedMissionAdjustedY, headerPos.z);

        // adjesting next btn
        var finishedMissionNextBtny = finishedMissionAdjustedY - getHeight(finishedMissionNextBtn.GetComponent<RectTransform>()) / 2 - finishedMissionHeigt / 2 - nextBtnDistanceFromCompletedMission;
        finishedMissionNextBtnPos.transform.position = new Vector3(headerPos.x, finishedMissionNextBtny, headerPos.z);

        Debug.Log("panel start Y is: " + panelAdjustedYPosition);
    }

    public float AdjustPanelsOnLevelUpAndReciveNewY()
    {
        // find panel
        var finishedMissionPanel = GameObject.Find("LevelUp/NewLevel/RankStarsPanel");
        var tempPanelPos = finishedMissionPanel.transform.position;

        // adjesting panel according to header
        var finishedMissionPanelHeight = getHeight(finishedMissionPanel.GetComponent<RectTransform>());
        var headerPos = finishedMissionHeader.transform.position;
        var panelAdjustedYPosition = headerPos.y - getHeight(finishedMissionHeader.GetComponent<RectTransform>()) / 2 - finishedMissionPanelHeight / 2 - panelDistanceFromHeader;
        finishedMissionPanel.transform.position = new Vector3(headerPos.x, panelAdjustedYPosition, headerPos.z);

        // adjesting mission acoording to panel
        var finishedMissionHeigt = getHeight(finishedMissionCompletedMission.GetComponent<RectTransform>());
        var finishedMissionAdjustedY = panelAdjustedYPosition - finishedMissionHeigt / 2 - finishedMissionPanelHeight / 2 - panelDistanceFromCompletedMission;
        finishedMissionCompletedMission.transform.position = new Vector3(headerPos.x, finishedMissionAdjustedY, headerPos.z);

        // adjesting next btn
        var finishedMissionNextBtny = finishedMissionAdjustedY - getHeight(finishedMissionNextBtn.GetComponent<RectTransform>()) / 2 - finishedMissionHeigt / 2 - nextBtnDistanceFromCompletedMission;
        finishedMissionNextBtnPos.transform.position = new Vector3(headerPos.x, finishedMissionNextBtny, headerPos.z);

        finishedMissionPanel.transform.position = tempPanelPos;

        Debug.Log("panel adjusted Y is: " + panelAdjustedYPosition);
        return panelAdjustedYPosition;
    }

    // Helper method to get height.
    public float getHeight(RectTransform guiObject)
    {
        Vector3[] corners = new Vector3[4] { new Vector3(), new Vector3(), new Vector3(), new Vector3() };
        guiObject.GetWorldCorners(corners);
        return Mathf.Abs(corners[0].y - corners[2].y);
    }
}
