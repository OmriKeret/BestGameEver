using UnityEngine;
using System.Collections;

/**
 * Stupid - Can't attack, can be killed easely.
 * Spike - Can't attack, but has spikes that occasionaly apeare and hurt the player on touch.
 * Tank - Weak attacker, but need a few hits to be killed
 * 
 */
using System;
[Serializable]
public enum EnemyType
{
    General = 0,
    Stupid = 1,
    Spike = 2,
    Tank = 3,
    Hawk = 4,
    Cannon = 5,
    End = 6,// This is NOT an enemy, just means that the way have ended
    
}
