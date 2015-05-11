using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class Hats {

    public List<ClothModel> hats = new List<ClothModel> {
		new ClothModel{id = 1, description = "Ponchjoe's trusty hat, no big deal.", jemPrice = -1, name = "Cowboy Hat", owned = true,characterSpriteFront = "Cloth/Hat/normal", part = BodyPart.hat, PJPrice = 0,selected = true, statsImprove = new StatsImprovementModel{}},
        new ClothModel{id = 2, description = "Imbued with epic stuff. 'nuff said.", jemPrice = -1, name = "Black Sun Hat", owned = false, part = BodyPart.hat,characterSpriteFront = "Cloth/Hat/leather", PJPrice = 0, selected = false, statsImprove = new StatsImprovementModel{}},
         new ClothModel{id = 3, description = "It's a hat that is also a chicken, deal-with-it.", jemPrice = -1, name = "Chicken Hat", owned = false, part = BodyPart.hat,characterSpriteFront = "Cloth/Hat/chicken", PJPrice = 0, selected = false, statsImprove = new StatsImprovementModel{}},
		new ClothModel{id = 4, description = "Half sheep, half bee, all hat.", jemPrice = -1, name = "The Bee-Sheep Hat", owned = false, part = BodyPart.hat,characterSpriteFront = "Cloth/Hat/bee hat", PJPrice = 0, selected = false, statsImprove = new StatsImprovementModel{}}

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
