using UnityEngine;
using System.Collections;

public enum EventTypes {
    
    SuperHitDeath = 1,
    //super hit need to keep track of the maximum number of enemies on the screen so 
    //next enum must be of number > 30

    WaveOver = 40,
    GameOver,
    FinishedMission,


    TestEvent = 2048//Just for testing
}
