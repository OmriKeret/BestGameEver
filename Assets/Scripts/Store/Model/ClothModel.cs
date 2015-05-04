using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class ClothModel {

   public int id;
   public BodyPart part;
   public int PJPrice;
   public int jemPrice;
   public string storeImg;
   public string characterSpriteFront;
   public string characterSpriteBack;
   public string name;
   public string description;
   public bool owned;
   public bool selected;
   public StatsImprovementModel statsImprove;
}
