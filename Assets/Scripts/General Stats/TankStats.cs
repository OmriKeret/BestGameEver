using UnityEngine;
using System.Collections;

public class TankStats : AEnemyStats
{

    void Start()
    {
        leftSplitLocation = new Vector2(10, 0);
        BASIC_HP_DOWN = 1;
        BASIC_HP = 2;
        life = BASIC_HP;
        MAX_SPEED = 10f;
        _mode = EnemyMode.None;
        _type = EnemyType.Tank;
    }
}
