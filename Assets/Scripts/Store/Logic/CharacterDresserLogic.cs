using UnityEngine;
using System.Collections;

public class CharacterDresserLogic : MonoBehaviour {

    PlayerStatsLogic playerStats;
    
    //guiParts
    GameObject sword;
    GameObject ponchoeFront;
    GameObject ponchoeFront2;
    GameObject ponchoeBack;
    GameObject hat;
    

    void Start()
    
    {
		var stats = GameObject.Find ("Logic");
        if (stats != null) {
			playerStats = stats.GetComponent<PlayerStatsLogic>();
		}
       
        sword = GameObject.Find("PlayerManager/arm/glove/SWORD");
        ponchoeFront = GameObject.Find("PlayerManager/PONCHO 1");
        ponchoeFront2 = GameObject.Find("PlayerManager/PONCHO 1/PONCHO 2");
        ponchoeBack = GameObject.Find("PlayerManager/PONCHO 1/poncho back");
        hat = GameObject.Find("PlayerManager/head/HAT");
        DressCharacter();
    }

    public void DressCharacter()
    {
        updateStats();
        updateCharacterGui();
    }

    private void updateCharacterGui()
    {
        updateSword();
        updateHat();
        updatePonchoe();
    }

    private void updatePonchoe()
    {
        //Debug.Log("punchoe is :" + ClothLogic.clothLogic.equipedPoncho.description);
        var itemSpriteFront = (Sprite)Resources.Load(ClothLogic.clothLogic.equipedPoncho.characterSpriteFront, typeof(Sprite));
        ponchoeFront.GetComponent<SpriteRenderer>().sprite = itemSpriteFront;
        ponchoeFront2.GetComponent<SpriteRenderer>().sprite = itemSpriteFront;
		var itemSpriteBack = (Sprite)Resources.Load(ClothLogic.clothLogic.equipedPoncho.characterSpriteBack, typeof(Sprite));
        ponchoeBack.GetComponent<SpriteRenderer>().sprite = itemSpriteBack;


    }

    private void updatePonchoe(ClothModel currentDisplayed)
    {
        var itemSpriteFront = (Sprite)Resources.Load(currentDisplayed.characterSpriteFront, typeof(Sprite));
        ponchoeFront.GetComponent<SpriteRenderer>().sprite = itemSpriteFront;
        ponchoeFront2.GetComponent<SpriteRenderer>().sprite = itemSpriteFront;
        var itemSpriteBack = (Sprite)Resources.Load(currentDisplayed.characterSpriteBack, typeof(Sprite));
        ponchoeBack.GetComponent<SpriteRenderer>().sprite = itemSpriteBack;


    }

    private void updateHat()
    {
        var itemSprite = (Sprite)Resources.Load(ClothLogic.clothLogic.equipedHat.characterSpriteFront, typeof(Sprite));
        hat.GetComponent<SpriteRenderer>().sprite = itemSprite;
    }

    private void updateHat(ClothModel currentDisplayed)
    {
        var itemSprite = (Sprite)Resources.Load(currentDisplayed.characterSpriteFront, typeof(Sprite));
        hat.GetComponent<SpriteRenderer>().sprite = itemSprite;
    }

    private void updateSword()
    {
        var itemSprite = (Sprite)Resources.Load(ClothLogic.clothLogic.equipedSword.characterSpriteFront, typeof(Sprite));
        sword.GetComponent<SpriteRenderer>().sprite = itemSprite;
      
    }
    private void updateSword(ClothModel currentDisplayed)
    {
        var itemSprite = (Sprite)Resources.Load(currentDisplayed.characterSpriteFront, typeof(Sprite));
        sword.GetComponent<SpriteRenderer>().sprite = itemSprite;
    }
    private void updateStats()
    {
        int dashNumBoost = ClothLogic.clothLogic.getDashNumBoost();
        int hpBoost = ClothLogic.clothLogic.getHPBoost();
        int dashDistBoost = ClothLogic.clothLogic.getDashDistBoost();
        int dmgBoost = ClothLogic.clothLogic.getDashDmgBoost();
        if (!Application.loadedLevelName.Equals("Store"))
        {
            playerStats.addDashNumBoost(dashNumBoost);
            playerStats.addDashDistBoost(dashDistBoost);
            playerStats.addDmgBoost(dmgBoost);
            playerStats.addDashHPBoost(hpBoost);
        }
    }





    internal void DressCharacterForStore(ClothModel currentDisplayed)
    {
        updateCharacterGui();
        switch (currentDisplayed.part)
        {
            case BodyPart.hat:
                updateHat(currentDisplayed);
                break;
            case BodyPart.puncho:
				updatePonchoe(currentDisplayed);
				break;
            case BodyPart.sword:
				updateSword(currentDisplayed);  
                break;
            default:
                Debug.Log("Error equiping");
                break;
        }
    }






}
