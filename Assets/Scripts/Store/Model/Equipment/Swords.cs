using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class Swords  {

    public List<ClothModel> swords = new List<ClothModel> {
        new ClothModel{id = 1, description = "Bringing old school back.", jemPrice = -1, name = "Samurai Sword", owned = true, part = BodyPart.sword, PJPrice = 0,characterSpriteFront = "Cloth/Sword/sword", selected = true, statsImprove = new StatsImprovementModel{}},
		new ClothModel{id = 2, description = "This sword can slice the toughest of demon skins. also really good for sandwiches!", jemPrice = -1, name = "Sword of Kickassery", owned = false, part = BodyPart.sword, PJPrice = 0,characterSpriteFront = "Cloth/Sword/sword2", selected = false, statsImprove = new StatsImprovementModel{}}

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
