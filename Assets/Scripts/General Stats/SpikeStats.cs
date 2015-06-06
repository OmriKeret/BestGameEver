using UnityEngine;
using System.Collections;

public class SpikeStats : AEnemyStats
{

    void Start()
    {
        leftSplitLocation = new Vector2(10, 0);
        BASIC_HP_DOWN = 1;
        BASIC_HP = 1;
        life = BASIC_HP;
        MAX_SPEED = 10f;
        _mode = EnemyMode.None;
        _type = EnemyType.Stupid;
        initAnimation();
    }




}
