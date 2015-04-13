using UnityEngine;
using System.Collections;

public class PlayerStatsController : MonoBehaviour {

    PlayerStatsLogic statsLogic;

    void Start()
    {
        statsLogic = GameObject.Find("Logic").GetComponent<PlayerStatsLogic>();
    }

    public int GetHp()
    {
        return statsLogic.HP;
    }

    public int GetCombo()
    {
        return statsLogic.combo;
    }

    public int GetStrength()
    {
        return statsLogic.Strength;
    }

    public int GetDashNumber()
    {
        return statsLogic.dashNum;
    }
}
