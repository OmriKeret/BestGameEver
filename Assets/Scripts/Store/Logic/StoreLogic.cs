using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
public class StoreLogic : MonoBehaviour {

    public Hats hats;
    public Punchoes punchoes;
    public Swords swords;
    public int currentPJ;
    public int currentJem;
    public int index;
    private ClothModel currentDisplayed;
    private List<ClothModel> currentGroup;
    
    //gui
    private Text description;
    private Text name;
    private Text price;
    private GameObject buyButton;
   // private Image image; // itemImage
    public Sprite btnBuy;
    public Sprite btnEquiped;
    private Text moneyAmount;
    // dreser 
    private CharacterDresserLogic dresser;
	// Use this for initialization
	void Start () {
      
        index = 0;
        hats.hats = ClothLogic.clothLogic.hats.hats;
        punchoes.pounchoes = ClothLogic.clothLogic.punchoes.pounchoes;
        swords.swords = ClothLogic.clothLogic.swords.swords;
        var i = MemoryAccess.memoryAccess.LoadCurrency();
        currentPJ = i.PJ;
        currentJem = i.gems;
		dresser = GameObject.Find("CharacterDresser").GetComponent<CharacterDresserLogic>();

        //gui elements
        description = GameObject.Find("Canvas/Description/DescriptionText").GetComponent<Text>();
        price = GameObject.Find("Description/PriceText").GetComponent<Text>();
        name = GameObject.Find("Description/NameText").GetComponent<Text>();
       // image = GameObject.Find("Description/Image").GetComponent<Image>();
        buyButton = GameObject.Find("Buy button");
		ChangeItemGroup(BodyPart.sword);
        moneyAmount = GameObject.Find("Currency/CurrencyAmount").GetComponent<Text>();
        writeCurrentCurrency();
	}

    private void writeCurrentCurrency()
    {
        moneyAmount.text = string.Format("{0}",currentPJ);
        //TODO: write current gems
    }

    private List<ClothModel> clone(List<ClothModel> list)
    {
        List<ClothModel> res = new List<ClothModel>();
        foreach (var item in list)
        {
            res.Add(new ClothModel { 
                description = item.description,
                id = item.id,
                statsImprove = new StatsImprovementModel { dashDist = item.statsImprove.dashDist, dashNum = item.statsImprove.dashNum, hp = item.statsImprove.hp },
                storeImg = item.storeImg,
                characterSpriteBack = item.characterSpriteBack,
                characterSpriteFront = item.characterSpriteFront,
                jemPrice = item.jemPrice,
                PJPrice = item.PJPrice,
                name = item.name,
                owned = item.owned,
                part = item.part,
                selected = item.selected
            });
        }
        return res;
    }

    public bool buyItemWithPJ()
    {
        if (currentPJ >= currentDisplayed.PJPrice)
        {
            currentPJ = currentPJ - currentDisplayed.PJPrice;
            MemoryAccess.memoryAccess.SaveCurrency(new IOCurrencyModel { gems = currentJem, PJ = currentPJ });
            ClothLogic.clothLogic.buyItem(currentDisplayed);
            ClothLogic.clothLogic.equipItem(currentDisplayed, currentGroup);
            writeCurrentCurrency();
            return true;
        }
        return false;
    }

    public bool buyItemWithGem()
    {
        if (currentPJ >= currentDisplayed.jemPrice)
        {
            currentJem = currentJem - currentDisplayed.jemPrice;
            MemoryAccess.memoryAccess.SaveCurrency(new IOCurrencyModel { gems = currentJem, PJ = currentPJ });
            ClothLogic.clothLogic.buyItem(currentDisplayed);
            updateDisplayData();
            //TODO: rewrite gem num
            return true;
        }
        return false;
    }

    public void ChangeItemGroup(BodyPart part) {
        switch (part) {
            case (BodyPart.hat):
                switchItemsToHats();
                break;
            case (BodyPart.puncho):
                switchItemsToPonchoes();
                break;
            case (BodyPart.sword):
                switchItemsToSwords();
                break;
            default:
                switchItemsToSwords();
                break;
        }
        updateDisplayData();
    }

    public void moveLeft()
    {
        var len = currentGroup.Count;
        index = index - 1 < 0 ? len - 1 : (index - 1) % len;
        currentDisplayed = currentGroup[index];
        updateDisplayData();
    }

    public void moveRight()
    {
        var len = currentGroup.Count;
        index = (index + 1) % len;
        currentDisplayed = currentGroup[index];
        updateDisplayData();
    }

    private void updateDisplayData()
    {
        updateGuiDisplay();
        updateGuiCharacterDisplay();
    }

    private void updateGuiDisplay()
    {
        Debug.Log("current display is " + currentDisplayed.description);
        description.text = currentDisplayed.description;
        name.text = currentDisplayed.name;
        price.text = string.Format("{0} PJ", currentDisplayed.PJPrice);
        //TODO: add jem price here 

        //change image of item
        //image.sprite = (Sprite)Resources.Load(currentDisplayed.storeImg, typeof(Sprite));

        //change inage of buy button accordinly
        if (currentDisplayed.owned)
        {
            Debug.Log("item owned");
            //own item
            //change button buy/equip image
            var btn = buyButton.GetComponent<Image>();

             btn.sprite = btnEquiped;

            if (currentDisplayed.selected)
            {
                //change to disabled
                buyButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                buyButton.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            //dont own item
            var btn = buyButton.GetComponent<Image>();
            buyButton.GetComponent<Button>().interactable = true;
            btn.sprite = btnBuy;
        }
    }

    private void updateGuiCharacterDisplay()
    {
        dresser.DressCharacterForStore(currentDisplayed);
    }



    public void switchItemsToHats()
    {
       currentGroup = hats.hats;
       currentDisplayed = currentGroup.Single(e => e.selected);
       index = currentDisplayed.id - 1;
       updateDisplayData();
        //TODO: CHANGE IMAGE, DESC and PRICE  
    }
    public void switchItemsToPonchoes()
    {
        currentGroup = punchoes.pounchoes;
        currentDisplayed = currentGroup.Single(e => e.selected);
        index = currentDisplayed.id - 1;
        updateDisplayData();
        //TODO: CHANGE IMAGE, DESC and PRICE  
    }
    public void switchItemsToSwords()
    {
        currentGroup = swords.swords;
        currentDisplayed = currentGroup.Single(e => e.selected);
        index = currentDisplayed.id - 1;
        updateDisplayData();
        //TODO: CHANGE IMAGE, DESC and PRICE  
    }

    public void buyOrEquipItem()
    {
        if (currentDisplayed.owned)
        {
            //equip
            equipItem();
        }
        else
        {
            //buy
            if (!buyItemWithPJ())
            {
                //show not enough money messege or sound
            }
            //TODO: buy item with jem option
        }
        updateDisplayData();
    }

    public void equipItem()
    {
        ClothLogic.clothLogic.equipItem(currentDisplayed,currentGroup);
    }

    public void updatePJS(int PJS) {
          currentPJ = currentPJ + PJS;
            MemoryAccess.memoryAccess.SaveCurrency(new IOCurrencyModel { gems = currentJem, PJ = currentPJ });
            ClothLogic.clothLogic.buyItem(currentDisplayed);
            ClothLogic.clothLogic.equipItem(currentDisplayed, currentGroup);
            writeCurrentCurrency();
    }

}
