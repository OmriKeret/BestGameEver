﻿using UnityEngine;
using System.Collections;

public class darkScreenHelper : MonoBehaviour {

    ToturialLogic toturialLogic;
	// Use this for initialization
	void Start () {
        toturialLogic = this.GetComponentInParent<ToturialLogic>();
	}
    void finishedAnimation()
    {
        toturialLogic.finishedDarkScreen();
    }
}