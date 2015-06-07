using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class Swords  {

    public List<ClothModel> swords = new List<ClothModel> {
        new ClothModel{
			id = 1, 
			description = "Bringing old school back.", 
			jemPrice = -1, 
			name = "Samurai Sword", 
			owned = true, 
			part = BodyPart.sword, 
			PJPrice = 0,
			characterSpriteFront = "Cloth/Sword/sword", 
			selected = true, 
			statsImprove = new StatsImprovementModel{} //Default (dist 0, hp 0, dmg 0, dashnum 0)
		},
		new ClothModel{
			id = 2, 
			description = "\nThis heavy sword can slice the toughest of demon skins! +DAMAGE, -SPEED", 
			jemPrice = -1, 
			name = "Heavy Sword of Kickassery", 
			owned = false, 
			part = BodyPart.sword, 
			PJPrice = 97,
			characterSpriteFront = "Cloth/Sword/sword3", 
			selected = false, 
			statsImprove = new StatsImprovementModel{dmg=1, dashDist=-2}
		},
		new ClothModel{
			id = 3, 
			description = "Now we're talkin'. +DAMAGE", 
			jemPrice = -1, 
			name = "Ninja Sword", 
			owned = true, 
			part = BodyPart.sword, 
			PJPrice = 308,
			characterSpriteFront = "Cloth/Sword/sword2", 
			selected = true, 
			statsImprove = new StatsImprovementModel{dmg = 3}
		},
		new ClothModel{
			id = 4, 
			description = "Wicked Samurai, this is turning crazy. +DAMAGE, +SPEED", 
			jemPrice = -1, 
			name = "The Black Swan", 
			owned = true, 
			part = BodyPart.sword, 
			PJPrice = 1000,
			characterSpriteFront = "Cloth/Sword/sword4", 
			selected = true, 
			statsImprove = new StatsImprovementModel{dmg = 7}
		},
		new ClothModel{
			id = 5, 
			description = "Severe Pain +Awesome Damage", 
			jemPrice = -1, 
			name = "Haragoho", 
			owned = true, 
			part = BodyPart.sword, 
			PJPrice = 3200,
			characterSpriteFront = "Cloth/Sword/sword5", 
			selected = true, 
			statsImprove = new StatsImprovementModel{dmg = 15}
		}//,
		/*new ClothModel{
			id = 6, 
			description = "In the future, they're all like that. +Massive Damage", 
			jemPrice = -1, 
			name = "It's a Secret", 
			owned = true, 
			part = BodyPart.sword, 
			PJPrice = 99999,
			characterSpriteFront = "Cloth/Sword/sword", 
			selected = true, 
			statsImprove = new StatsImprovementModel{dmg = 31}
		}*/
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
