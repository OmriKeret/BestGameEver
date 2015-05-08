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
    public int dashDist = 15;
    public PowerUpType powerUpModeActive;
    public GameObject guiHP;
    public GameObject _LifeFullPrefab;
    public GameObject _LifeEmptyPrefab;
    private char hearSymbole = '♥';
	public Text comboText;
    public GameObject[] lifes;

    //GUI
    HPBarLogic HPBar;
    StaminaBarLogic staminaBar;
    //logic
    private MovmentLogic movmentLogic;
	// Use this for initialization
	void Start () {
        HPBar = this.GetComponent<HPBarLogic>();
        staminaBar = this.GetComponent<StaminaBarLogic>();
        guiHP = GameObject.Find("HpContainer");
		comboText = GameObject.Find("ComboText").GetComponent<Text>();
        _LifeFullPrefab = Resources.Load("lifeFull") as GameObject;
        _LifeEmptyPrefab = Resources.Load("lifeEmpty") as GameObject;
        firstTimeWriteHp();
        movmentLogic = this.GetComponent<MovmentLogic>();
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
     
        //lifes = new GameObject[HP];
        //for (int i = 0; i < HP; i++)
        //{
        //  //  Debug.Log("creating");
        //    Vector3 pos = guiHP.transform.position;
        //    pos.x = (float)(pos.x + 4 * i);
        //    lifes[i] = Instantiate(_LifeFullPrefab, pos, Quaternion.identity) as GameObject;
        //    lifes[i].transform.parent = guiHP.transform;
        //}
    }
    private void ReWriteHP()
    {
        HPBar.updateCurrentHP(HP);
        //for (int i = 0; i < lifes.Length; i++)
        //{
        //    if (i + 1 <= HP)
        //    {

        //    }
        //    else
        //    {
        //        Vector3 pos = guiHP.transform.position;
        //        pos.x = (float)(pos.x + 4 * i);
        //        Destroy(lifes[i].gameObject);
        //        lifes[i] = Instantiate(_LifeEmptyPrefab, pos, Quaternion.identity) as GameObject;
        //    }
        //}
      //  HPText.text = numOfHearts;
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
        movmentLogic.dashDist = dashDist;
    }

    internal void addDashHPBoost(int hpBoost)
    {
        
        HP += hpBoost;
        firstTimeWriteHp();
    }
}
