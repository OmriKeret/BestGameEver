using UnityEngine;
using System.Collections;

public class MissionModel {

   public MissionType type;
   public EnemyType enemyType;
   public PowerUpType powerUpType;
   public int numberToAchive;
   public int currentNumberAchived;
   public string missionText;
   public bool isFinished;
   public bool needToBeCompletedInOneGame;
}
