using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatsLogic : MonoBehaviour {
    public int MAX_DASH_NUM = 5;
    public int maxHP;
    public int HP = 3;
    public int Strength = 1;
    public int combo = 0;
    public int dashNum = 5;
    public int dashDist = 5;
    public PowerUpType powerUpModeActive;
    public GameObject guiHP;
    public GameObject _LifeFullPrefab;
    public GameObject _LifeEmptyPrefab;
	public Text comboText;
    public GameObject[] lifes;

    //GUI
    HPBarLogic HPBar;
    StaminaBarLogic staminaBar;
    //logic
    private MovmentLogic movmentLogic;
    private SuperPowerLogic superPower;
	// Use this for initialization
    void OnEnable()
    {
		maxHP = HP;
        HPBar = this.GetComponent<HPBarLogic>();
        staminaBar = this.GetComponent<StaminaBarLogic>();
        guiHP = GameObject.Find("HpContainer");
		comboText = GameObject.Find("ComboText").GetComponent<Text>();
        superPower = this.GetComponent<SuperPowerLogic>();
        _LifeFullPrefab = Resources.Load("lifeFull") as GameObject;
        _LifeEmptyPrefab = Resources.Load("lifeEmpty") as GameObject;
		movmentLogic = this.GetComponent<MovmentLogic>();
        

	}
	void Start() 
	{
		firstTimeWriteHp();

		maxHP = HP;
		staminaBar.setMaximumStamina(dashNum);
	}
	
	// Update is called once per frame
	public void resetCombo(){
        combo = 0;
		reWrithCombo ();
    }

    public void addOneToCombo()
    {
        combo += 1;
        superPower.changeState(combo);
		reWrithCombo ();
    }

    public void removeOneDash()
    {
        dashNum -= 1;
        staminaBar.updateCurrentStamina(dashNum);
    }

    public void resetDash()
    {
        dashNum = MAX_DASH_NUM;
        staminaBar.updateCurrentStamina(dashNum);
    }

    //return true if dead
    public bool removeHp(int num)
    {
        HP -= num;
        ReWriteHP();
        if (HP <= 0)
        {
            return true;
        }
        return false;
    }
	public void reWrithCombo() 
	{
		if (combo != 0) {
			comboText.text = string.Format ("X{0}", combo);
		} else {
			comboText.text = "";
		}
	}

    public void firstTimeWriteHp()
    {
        HPBar.setMaximumHP(HP);
        HPBar.updateCurrentHP(HP);
    }
    private void ReWriteHP()
    {
        HPBar.updateCurrentHP(HP);

    }


    internal void addDashNumBoost(int dashNumBoost)
    {
        MAX_DASH_NUM += dashNumBoost;
        staminaBar.setMaximumStamina(dashNum);
        resetDash();
    }

    internal void addDashDistBoost(int dashDistBoost)
    {
        dashDist += dashDistBoost;
        movmentLogic.dashDist = this.dashDist;
    }

    internal void addDashHPBoost(int hpBoost)
    {    
        HP += hpBoost;
        maxHP = HP;
        firstTimeWriteHp();
    }

    internal void addDmgBoost(int dmgBoost)
    {
        Strength += dmgBoost;
    }
}
