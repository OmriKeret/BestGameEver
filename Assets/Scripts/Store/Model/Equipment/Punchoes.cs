using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class Punchoes {

    public List<ClothModel> pounchoes = new List<ClothModel> {
        new ClothModel{id = 1, description = "normal mexican pouncho", jemPrice = -1, name = "Mexican pouncho", owned = true, part = BodyPart.puncho, PJPrice = 0, selected = true},
        new ClothModel{id = 2, description = "leather cowboy pouncho", jemPrice = -1, name = "cowboy pouncho", owned = false, part = BodyPart.puncho, PJPrice = 5, selected = false}

        };

    public IOTotalClothModel toModel()
    {
        IOTotalClothModel item = new IOTotalClothModel();
        item.items = new List<IOClothModel>();
        foreach (var i in pounchoes)
        {
            item.items.Add(new IOClothModel { id = i.id, bodyPart = i.part, imgPath = i.img, selected = i.selected });
        }
        return item;
    }
    
}
