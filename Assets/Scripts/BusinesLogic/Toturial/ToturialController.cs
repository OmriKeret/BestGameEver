﻿using UnityEngine;
using System.Collections;

public class ToturialController : MonoBehaviour {

    ToturialLogic toturialLogic;
    int numberOfEnemy;
    void Start()
    {
        toturialLogic = this.GetComponent<ToturialLogic>();
        toturialLogic.checkIfNeededToStartToturial();
        numberOfEnemy = 0;
    }



    internal void playerHitEnemy()
    {
        toturialLogic.playerKilledEnemy(numberOfEnemy);
        numberOfEnemy++;
    }
}
