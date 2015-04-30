using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ClothLogic : MonoBehaviour {

   public Hats hats;
   public Punchoes punchoes;
   public Swords swords;
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

   
    public void addItem(ClothModel item)
    {
        switch (item.part) 
        {
            case BodyPart.hat:
                hats.hats.Add(item);
                MemoryAccess.memoryAccess.SaveHats(hats.toModel());
                break;
            case BodyPart.puncho:
                punchoes.pounchoes.Add(item);
                MemoryAccess.memoryAccess.SavePonchoes(punchoes.toModel());
                break;
            case BodyPart.sword:
                swords.swords.Add(item);
                MemoryAccess.memoryAccess.SaveSwords(swords.toModel());
                break;
            default:
                Debug.Log("Error adding item");
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
        //iterate thorugh the hats and mark those who we own
        foreach (var hat in hatsFromMem.items)
        {
            var i = hats.hats.Find(e => e.id == hat.id);
            if (i != null)
            {
                i.owned = true;
                i.selected = hat.selected;
            }
            
        }
    }

    private void syncPonchoes(IOTotalClothModel phonchesFromMem)
    {
        //iterate thorugh the phonches and mark those who we own
        foreach (var phonche in phonchesFromMem.items)
        {
            var i = punchoes.pounchoes.Find(e => e.id == phonche.id);
            if (i != null)
            {
                i.owned = true;
                i.selected = phonche.selected;
            }
        }
    }

    private void syncSwords(IOTotalClothModel swordsFromMem)
    {
        //iterate thorugh the swords and mark those who we own
        foreach (var sword in swordsFromMem.items)
        {
            var i = swords.swords.Find(e => e.id == sword.id);
            if (i != null)
            {
                i.owned = true;
                i.selected = sword.selected;
            }
        }
    }

    private void initilaizeCloth()
    {
        hats = new Hats();
        punchoes = new Punchoes();
        swords = new Swords();
    }


	
}
