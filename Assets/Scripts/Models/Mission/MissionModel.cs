using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class MissionModel {

   public MissionType type;
   public EnemyType enemyType;
   public PowerUpType powerUpType;
   public CollectableTypes collectableType;
   public int numberToAchive;
   public int currentNumberAchived;
   public string missionText;
   public bool isFinished;
   public bool needToBeCompletedInOneGame;
}
