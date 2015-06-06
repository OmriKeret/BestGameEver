using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class Punchoes {

    public List<ClothModel> pounchoes = new List<ClothModel> {
        new ClothModel{
			id = 1, 
			description = "Given to him at birth, it has no special new powers, but it is beautiful.", 
			jemPrice = -1, 
			name = "Classic Poncho", 
			owned = true, 
			part = BodyPart.puncho, 
			PJPrice = 0,
			characterSpriteFront = "Cloth/Poncho/poncho",
			characterSpriteBack = "Cloth/Poncho/poncho back", 
			selected = true, 
			statsImprove = new StatsImprovementModel{} //Default (dist 0, hp 0, dmg 0, dashnum 0)
		},

		new ClothModel{
			id = 2, 
			description = "Oh man, more dashes!", 
			jemPrice = -1, 
			name = "Black Sun Poncho", 
			owned = false, 
			part = BodyPart.puncho, 
			PJPrice = 9700,
			characterSpriteFront = "Cloth/Poncho/poncho3",
			characterSpriteBack = "Cloth/Poncho/poncho back", 
			selected = false, 
			statsImprove = new StatsImprovementModel{dashNum=1, dashDist=1}
		},
		new ClothModel{
			id = 3, 
			description = "Remember the last one? This is better.", 
			jemPrice = -1, 
			name = "Sexy Poncho", 
			owned = false, 
			part = BodyPart.puncho, 
			PJPrice = 30875,
			characterSpriteFront = "Cloth/Poncho/poncho2",
			characterSpriteBack = "Cloth/Poncho/poncho back", 
			selected = false, 
			statsImprove = new StatsImprovementModel{dashNum=2, dashDist=1}
		},
		new ClothModel{
			id = 4, 
			description = "Fast, fast, fast, fast, fast", 
			jemPrice = -1, 
			name = "Do you have that in purple?", 
			owned = false, 
			part = BodyPart.puncho, 
			PJPrice = 100000,
			characterSpriteFront = "Cloth/Poncho/poncho4",
			characterSpriteBack = "Cloth/Poncho/poncho back", 
			selected = false, 
			statsImprove = new StatsImprovementModel{dashNum=2, dashDist=2}
		}/*,
		new ClothModel{
			id = 5, 
			description = "Imbued with epic stuff. 'nuff said.", 
			jemPrice = -1, 
			name = "Swift Eye", 
			owned = false, 
			part = BodyPart.puncho, 
			PJPrice = 321000,
			characterSpriteFront = "Cloth/Poncho/poncho5",
			characterSpriteBack = "Cloth/Poncho/poncho back", 
			selected = false, 
			statsImprove = new StatsImprovementModel{dashNum=3, dashDist=4}
		}
		new ClothModel{
			id = 6, 
			description = "They say The Flash wears that when he wants more speed", 
			jemPrice = -1, 
			name = "Secret", 
			owned = false, 
			part = BodyPart.puncho, 
			PJPrice = 999999,
			characterSpriteFront = "Cloth/Poncho/poncho6",
			characterSpriteBack = "Cloth/Poncho/poncho back", 
			selected = false, 
			statsImprove = new StatsImprovementModel{dashNum=3, dashDist=5}
		}*/
        };

    public IOTotalClothModel toModel()
    {
        IOTotalClothModel item = new IOTotalClothModel();
        item.items = new List<IOClothModel>();
        foreach (var i in pounchoes)
        {
            if (i.owned == true)
            {
                item.items.Add(new IOClothModel { id = i.id, bodyPart = i.part, selected = i.selected });
            }
        }
        return item;
    }
    
}
