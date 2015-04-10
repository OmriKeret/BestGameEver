using UnityEngine;
using System.Collections;

public class PlayerStatsLogic : MonoBehaviour {
    public int MAX_DASH_NUM = 5;
    public int HP = 3;
    public int Strength = 1;
    public int combo = 1;
    public int dashNum = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void resetCombo(){
        combo = 1;
    }

    public void addOneToCombo()
    {
        combo += 1;
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
        if (HP <= 0)
        {
            return true;
        }
        return false;
    }

}
