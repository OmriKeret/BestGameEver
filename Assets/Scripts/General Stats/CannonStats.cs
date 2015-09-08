using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


    public class CannonStats : BasicEnemyStats
    {
        void Start()
        {
            leftSplitLocation = new Vector2(10, 0);
            BASIC_HP_DOWN = 1;
            BASIC_HP = 1;
            life = BASIC_HP;
            MAX_SPEED = 10f;
            _mode = EnemyMode.Attack;
            _type = EnemyType.Cannon;
        }

    }
