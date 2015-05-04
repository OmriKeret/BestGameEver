using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class Swords  {

    public List<ClothModel> swords = new List<ClothModel> {
        new ClothModel{id = 1, description = "normal mexican sword", jemPrice = -1, name = "Mexican sword", owned = true, part = BodyPart.sword, PJPrice = 0,characterSpriteFront = "Cloth/Sword/sword", selected = true, statsImprove = new StatsImprovementModel{}},
        new ClothModel{id = 2, description = "leather cowboy sword", jemPrice = -1, name = "cowboy sword", owned = false, part = BodyPart.sword, PJPrice = 5,characterSpriteFront = "Cloth/Sword/sword2", selected = false, statsImprove = new StatsImprovementModel{}}

        };

    internal IOTotalClothModel toModel()
    {
        IOTotalClothModel item = new IOTotalClothModel();
        item.items = new List<IOClothModel>();
        foreach (var i in swords)
        {
            if (i.owned == true)
            {
                item.items.Add(new IOClothModel { id = i.id, bodyPart = i.part, selected = i.selected });
            }
        }
        return item;
    }
}
