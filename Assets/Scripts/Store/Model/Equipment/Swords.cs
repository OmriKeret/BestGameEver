using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class Swords  {

    public List<ClothModel> swords = new List<ClothModel> {
        new ClothModel{id = 1, description = "normal mexican sword", jemPrice = -1, name = "Mexican sword", owned = true, part = BodyPart.sword, PJPrice = 0, selected = true},
        new ClothModel{id = 2, description = "leather cowboy sword", jemPrice = -1, name = "cowboy sword", owned = false, part = BodyPart.sword, PJPrice = 5, selected = false}

        };

    internal IOTotalClothModel toModel()
    {
        IOTotalClothModel item = new IOTotalClothModel();
        item.items = new List<IOClothModel>();
        foreach (var i in swords)
        {
            item.items.Add(new IOClothModel { id = i.id, bodyPart = i.part, imgPath = i.img, selected = i.selected });
        }
        return item;
    }
}
