using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class Hats {

    public List<ClothModel> hats = new List<ClothModel> {
        new ClothModel{id = 1, description = "normal mexican hat", jemPrice = -1, name = "Mexican hat", owned = true,characterSpriteFront = "Cloth/Hat/normal", part = BodyPart.hat, PJPrice = 0,selected = true, statsImprove = new StatsImprovementModel{}},
        new ClothModel{id = 2, description = "leather cowboy hat", jemPrice = -1, name = "cowboy hat", owned = false, part = BodyPart.hat,characterSpriteFront = "Cloth/Hat/leather", PJPrice = 5, selected = false, statsImprove = new StatsImprovementModel{}},
         new ClothModel{id = 3, description = "Chicken hat", jemPrice = -1, name = "Chicken hat", owned = false, part = BodyPart.hat,characterSpriteFront = "Cloth/Hat/chicken", PJPrice = 10, selected = false, statsImprove = new StatsImprovementModel{}}

        };

    public IOTotalClothModel toModel()
    {
        IOTotalClothModel item = new IOTotalClothModel();
        item.items = new List<IOClothModel>();
        foreach (var i in hats)
        {
            if (i.owned == true)
            {
                item.items.Add(new IOClothModel { id = i.id, bodyPart = i.part, selected = i.selected });
            }
        }
        return item;
    }
}
