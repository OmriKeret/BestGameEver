using UnityEngine;
using System.Collections;
using Soomla.Store;
using System;
using System.Collections.Generic;

public class RealCurrencyLogic : MonoBehaviour {
    StoreLogic storeLogic;

	// Use this for initialization
	void Start () {
        SoomlaStore.Initialize(new SoomlaPurhcableItem());
        storeLogic = this.GetComponent<StoreLogic>();

        StoreEvents.OnMarketPurchase += (PurchasableVirtualItem pvi, string payload, Dictionary<string,string> a) =>
        {
            switch (pvi.ID)
            {
                case SoomlaPurhcableItem.HUND_COIN_PACK_ID:
                    storeLogic.updatePJS(100);
                    break;
            }
        };
	}
    public void buyCurrency()
    {
        try
        {
            Debug.Log("SOOMLA IS TRYING TO BUY ITEM ID: " + SoomlaPurhcableItem.HUND_COIN_PACK_ID);
            StoreInventory.BuyItem(SoomlaPurhcableItem.HUND_COIN_PACK_ID);
        }
        catch (Exception e)
        {
            Debug.Log("SOOMLA " + e.Message);
        }
    }
}
