using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class Hats {

    public List<ClothModel> hats = new List<ClothModel> {
		new ClothModel{
			id = 1, 
			description = "Ponchjoe's trusty hat", 
			jemPrice = -1, 
			name = "Cowboy Hat",
			owned = true,
			characterSpriteFront = "Cloth/Hat/hat1",
			part = BodyPart.hat,
			PJPrice = 0,
			selected = true,
			statsImprove = new StatsImprovementModel{} //Default (dist 0, hp 0, dmg 0, dashnum 0)
		},
        new ClothModel{
			id = 2, 
			description = "Imbued with epic stuff. 'nuff said. +Health",
			jemPrice = -1, 
			name = "Black Sun Hat", 
			owned = false, 
			part = BodyPart.hat,
			characterSpriteFront = "Cloth/Hat/hat3", 
			PJPrice = 9700, 
			selected = false, 

			statsImprove = new StatsImprovementModel{hp=1}
		},
         new ClothModel{
			id = 3,
			description = "It's a hat that is also a chicken, deal-with-it. +More health",
			jemPrice = -1, name = "Chicken Hat",
			owned = false,
			part = BodyPart.hat,
			characterSpriteFront = "Cloth/Hat/chicken", 
			PJPrice = 30875, 
			selected = false, 
			statsImprove = new StatsImprovementModel{hp=2}
		},
		new ClothModel{
			id = 4, 
			description = "Health of a Sheep and Health of a Bee", 
			jemPrice = -1, 
			name = "The Bee-Sheep Hat", 
			owned = false, 
			part = BodyPart.hat,characterSpriteFront = "Cloth/Hat/bee hat", 
			PJPrice = 100000, 
			selected = false, 
			statsImprove = new StatsImprovementModel{hp=3}
		},
		new ClothModel{
			id = 5, 
			description = "Enough health to go around...", 
			jemPrice = -1, 
			name = "Boss's Sombrero", 
			owned = false, 
			part = BodyPart.hat,characterSpriteFront = "Cloth/Hat/hat2", 
			PJPrice = 321000, 
			selected = false, 
			statsImprove = new StatsImprovementModel{hp=4}
		},
		new ClothModel{
			id = 6, 
			description = "Dying is not an Option", 
			jemPrice = -1, 
			name = "Secret", 
			owned = false, 
			part = BodyPart.hat,characterSpriteFront = "Cloth/Hat/hat4", 
			PJPrice = 999999, 
			selected = false, 
			statsImprove = new StatsImprovementModel{hp=5}
		}
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
