using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class IOMissionModel {

    public int tier = -2;
    public MissionModel[] missions;

    public string ToString() {
        return " tier is: " + this.tier + "there are " + missions.Length + " missions";
    }
}

