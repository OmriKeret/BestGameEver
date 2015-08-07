using UnityEngine;
using System.Collections;

public class CurrencyLogic : MonoBehaviour {

    CurrencyData currencyDataAccess;
    public int deviderToScore = 1;
    public int goldEarned;
    void Start()
    {
        currencyDataAccess = GameObject.Find("GameManagerData").GetComponent<CurrencyData>();
        deviderToScore = 1;
    }

    public int updateCurrencyByScore(int score)
    {
        int currencyToAdd = score / deviderToScore; // + coins
        var currentCurrency = currencyDataAccess.loadCurrency();
        Debug.Log("Before" + currentCurrency.PJ);
        currentCurrency.PJ += currencyToAdd;
        Debug.Log("After"+currentCurrency.PJ);
        currencyDataAccess.saveCurrency(currentCurrency);
        return currencyToAdd;
    }

    public int getGoldEarned()
    {
        return goldEarned;
    }
}
