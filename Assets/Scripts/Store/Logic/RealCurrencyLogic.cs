using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Soomla.Store;

public class RealCurrencyLogic : MonoBehaviour
{
      StoreLogic storeLogic;
      
    // Use this for initialization
    void Start()
    {
       // SoomlaStore.Initialize(new SoomlaPurhcableItem());
        storeLogic = this.GetComponent<StoreLogic>();

        StoreEvents.OnMarketPurchase += (PurchasableVirtualItem pvi, string payload, Dictionary<string, string> a) =>
        {
            Debug.Log("purchase event success");
            switch (pvi.ID)
            {
                case SoomlaPurhcableItem.COIN_PACK_100_PRODUCT_ID:
                    storeLogic.updatePJS(100);
                    break;
            }
        };

        StoreEvents.OnMarketPurchaseCancelled += (PurchasableVirtualItem pvi) =>
        {
            Debug.Log("purchase event cancelled");

        };


        StoreEvents.OnMarketRefund += (PurchasableVirtualItem pvi) =>
        {
            Debug.Log("purchase event refund");
            //switch (pvi.ID)
            //{
            //    case SoomlaPurhcableItem.HUND_COIN_PACK_ID:
            //        storeLogic.updatePJS(100);
            //        break;
            //}
        };
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 2) // store index
        {
            SoomlaStore.StartIabServiceInBg();
            Debug.Log("Soomla starts IabService");
        }
        else
        {
            SoomlaStore.StopIabServiceInBg();
        }

    }
    public void buyCurrency()
    {
        try
        {
            Debug.Log("SOOMLA IS TRYING TO BUY ITEM ID: " + SoomlaPurhcableItem.COIN_PACK_100_PRODUCT_ID);
            StoreInventory.BuyItem(SoomlaPurhcableItem.COIN_PACK_100_PRODUCT_ID);
        }
        catch (Exception e)
        {
            Debug.Log("SOOMLA threw exception");
        }
    }

    public void buyRefundCurrency()
    {
        try
        {
            Debug.Log("SOOMLA IS TRYING TO BUY ITEM ID: " + SoomlaPurhcableItem.REFUND_TEST_PACK_PRODUCT_ID);
            StoreInventory.BuyItem(SoomlaPurhcableItem.REFUND_TEST_PACK_PRODUCT_ID);
        }
        catch (Exception e)
        {
            Debug.Log("SOOMLA " + e.Message);
        }
    }

    public void buyCancelCurrency()
    {
        try
        {
            Debug.Log("SOOMLA IS TRYING TO BUY ITEM ID: " + SoomlaPurhcableItem.CANCELED_TEST_PACK_PRODUCT_ID);
            StoreInventory.BuyItem(SoomlaPurhcableItem.CANCELED_TEST_PACK_PRODUCT_ID);
        }
        catch (Exception e)
        {
            Debug.Log("SOOMLA " + e.Message);
        }
    }
}
