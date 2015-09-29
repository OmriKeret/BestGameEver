using UnityEngine;
using System.Collections;

public class CurrencyLogic : MonoBehaviour {

    CurrencyData currencyDataAccess;
    public int killPerCoin = 2;
    public int goldEarned;
    void Start()
    {
        currencyDataAccess = GameObject.Find("GameManagerData").GetComponent<CurrencyData>();
        killPerCoin = 2;
    }

    public int updateCurrencyByKillsAndLoot(int kills, int coinsEarned)
    {
        int currencyToAdd = kills / killPerCoin; // + coins
        var currentCurrency = currencyDataAccess.loadCurrency();
        currentCurrency.PJ += currencyToAdd + coinsEarned;
        currencyDataAccess.saveCurrency(currentCurrency);
        return currencyToAdd;
    }

    public int getCoinsPerKill()
    {
        return killPerCoin;
    }

    public int getGoldEarned()
    {
        return goldEarned;
    }
}
