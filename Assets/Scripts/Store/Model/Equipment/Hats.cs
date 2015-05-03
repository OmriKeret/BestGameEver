using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class Hats {

    public List<ClothModel> hats = new List<ClothModel> {
        new ClothModel{id = 1, description = "normal mexican hat", jemPrice = -1, name = "Mexican hat", owned = true, part = BodyPart.hat, PJPrice = 0,selected = true},
        new ClothModel{id = 2, description = "leather cowboy hat", jemPrice = -1, name = "cowboy hat", owned = false, part = BodyPart.hat, PJPrice = 5, selected = false}

        };

    public IOTotalClothModel toModel()
    {
        IOTotalClothModel item = new IOTotalClothModel();
        item.items = new List<IOClothModel>();
        foreach (var i in hats)
        {
            item.items.Add(new IOClothModel {id = i.id, bodyPart = i.part, imgPath = i.img , selected = i.selected});
        }
        return item;
    }
}
