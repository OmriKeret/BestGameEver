using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatsLogic : MonoBehaviour {
    public int MAX_DASH_NUM = 5;
    public int HP = 3;
    public int Strength = 1;
    public int combo = 0;
    public int dashNum = 5;
    public Text HPText;
    private char hearSymbole = '♥';
	public Text comboText;
	// Use this for initialization
	void Start () {
        HPText = GameObject.Find("HP").GetComponent<Text>();
		comboText = GameObject.Find("ComboText").GetComponent<Text>();
		ReWriteHP();
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
    }

    public void resetDash()
    {
        dashNum = MAX_DASH_NUM;
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

    private void ReWriteHP()
    {
        string numOfHearts ="";
        for (int i = 0; i < HP; i++)
        {
            numOfHearts += hearSymbole; 
        }
        HPText.text = numOfHearts;
    }

}
