using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ClothLogic : MonoBehaviour {
   //all items
   public Hats hats;
   public Punchoes punchoes;
   public Swords swords;

   //current equiped items
   public ClothModel equipedHat;
   public ClothModel equipedPoncho;
   public ClothModel equipedSword;
   static public ClothLogic clothLogic;

    void Awake()
    {
        if (clothLogic == null)
        {
            DontDestroyOnLoad(gameObject);
            clothLogic = this;
        }
        else if (clothLogic != this)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        initilaizeCloth();
        syncronizeWithData();
	}

   
    public void buyItem(ClothModel item)
    {
        switch (item.part) 
        {
            case BodyPart.hat:
                hats.hats.Single(e => e.id == item.id).owned = true;
               // saveToMemory(item); going to mem when equiping
                break;
            case BodyPart.puncho:
                punchoes.pounchoes.Single(e => e.id == item.id).owned = true;
         //       saveToMemory(item);
                break;
            case BodyPart.sword:
                swords.swords.Single(e => e.id == item.id).owned = true;
            //    saveToMemory(item);
                break;
            default:
                Debug.Log("Error adding item");
                break;
        }
    }

    private  void saveToMemory(ClothModel item) {
        switch (item.part)
        {
            case BodyPart.hat:
                MemoryAccess.memoryAccess.SaveHats(hats.toModel());
                break;
            case BodyPart.puncho:
                MemoryAccess.memoryAccess.SavePonchoes(punchoes.toModel());
                break;
            case BodyPart.sword:
                MemoryAccess.memoryAccess.SaveSwords(swords.toModel());
                break;
            default:
                Debug.Log("Error saving to mem");
                break;
        }
    }
    private void syncronizeWithData()
    {
        //get data from mem
       IOTotalClothModel hatsFromMem = MemoryAccess.memoryAccess.LoadHats();
       IOTotalClothModel ponchoesFromMem = MemoryAccess.memoryAccess.LoadPonchoes();
       IOTotalClothModel swordsFromMem = MemoryAccess.memoryAccess.LoadSwords();

        //sync the data from mem
       syncHats(hatsFromMem);
       syncPonchoes(ponchoesFromMem);
       syncSwords(swordsFromMem);
    }

    private void syncHats(IOTotalClothModel hatsFromMem)
    {
        if (hatsFromMem != null) {
			//iterate thorugh the hats and mark those who we own
			foreach (var hat in hatsFromMem.items) {
				var i = hats.hats.Find (e => e.id == hat.id);
				if (i != null) {
					i.owned = true;
					i.selected = hat.selected;
					if (i.selected) {
						equipedHat = i;
					}
				}
            
			}
		} else {
			var i = hats.hats.Find (e => e.selected == true);
			equipedHat = i;
		}
    }

    private void syncPonchoes(IOTotalClothModel phonchesFromMem)
    {
        if (phonchesFromMem != null)
        {
            //iterate thorugh the phonches and mark those who we own
            foreach (var phonche in phonchesFromMem.items)
            {
                var i = punchoes.pounchoes.Find(e => e.id == phonche.id);
                if (i != null)
                {
                    i.owned = true;
                    i.selected = phonche.selected;
                    if (i.selected)
                    {
                        equipedPoncho = i;
                    }
                }
            }
        }
        else
        {
            var i = punchoes.pounchoes.Find(e => e.selected == true);
            equipedPoncho = i;
        }
    }

    private void syncSwords(IOTotalClothModel swordsFromMem)
    {
        if (swordsFromMem != null)
        {
            //iterate thorugh the swords and mark those who we own
            foreach (var sword in swordsFromMem.items)
            {
                var i = swords.swords.Find(e => e.id == sword.id);
                if (i != null)
                {
                    i.owned = true;
                    i.selected = sword.selected;
                    if (i.selected)
                    {
                        equipedSword = i;
                    }
                }
            }
        }
        else
        {
            var i = swords.swords.Find(e => e.selected == true);
            equipedSword = i;
        }
    }

    private void initilaizeCloth()
    {
        hats = new Hats();
        punchoes = new Punchoes();
        swords = new Swords();

    }






    internal void equipItem(ClothModel currentDisplayed, List<ClothModel> currentGroup)
    {
        foreach (var e in currentGroup)
        {
            if (e.id == currentDisplayed.id)
            {
                e.selected = true;
            }
            else
            {
                e.selected = false;
            }
        }
        switch (currentDisplayed.part)
        {
            case BodyPart.hat:
                equipedHat = currentDisplayed;
                break;
            case BodyPart.puncho:
                equipedPoncho = currentDisplayed;
                break;
            case BodyPart.sword:
                equipedSword = currentDisplayed;
                break;
            default:
                Debug.Log("Error equiping");
                break;
        }
        saveToMemory(currentDisplayed);

    }



    internal int getHPBoost()
    {
        int result = 0;
        result += equipedHat.statsImprove.hp;
        result += equipedSword.statsImprove.hp;
        result += equipedPoncho.statsImprove.hp;
        return result;
    }

    internal int getDashNumBoost()
    {
        int result = 0;
        result += equipedHat.statsImprove.dashNum;
        result += equipedSword.statsImprove.dashNum;
        result += equipedPoncho.statsImprove.dashNum;
        return result;
    }

    internal int getDashDistBoost()
    {
        int result = 0;
        result += equipedHat.statsImprove.dashDist;
        result += equipedSword.statsImprove.dashDist;
        result += equipedPoncho.statsImprove.dashDist;
        return result;
    }

    internal int getDashDmgBoost()
    {
        int result = 0;
        result += equipedHat.statsImprove.dmg;
        result += equipedSword.statsImprove.dmg;
        result += equipedPoncho.statsImprove.dmg;
        return result;
    }
}
