using UnityEngine;
using System.Collections;

/**
 * Stupid - Can't attack, can be killed easely.
 * Spike - Can't attack, but has spikes that occasionaly apeare and hurt the player on touch.
 * Pion - Simple attacker
 * Ninja - Strong and quick attacks
 * Tank - Weak attacker, but need a few hits to be killed
 * 
 */
public enum EnemyType
{
    General = 0,
    Stupid = 1,
    Spike,
    Tank,
    End,// This is NOT an enemy, just means that the way have ended
    //TBD: Ninja,Pion, Boss
    
}
