using UnityEngine;
using System.Collections;

public class CurrencyLogic : MonoBehaviour {

    CurrencyData currencyDataAccess;
    public int deviderToScore = 1;
    void Start()
    {
        currencyDataAccess = GameObject.Find("GameManagerData").GetComponent<CurrencyData>();
        deviderToScore = 1;
    }

    public int updateCurrencyByScore(int score)
    {
        int currencyToAdd = score / deviderToScore; // + coins
        var currentCurrency = currencyDataAccess.loadCurrency();
        currentCurrency.PJ += currencyToAdd;
        currencyDataAccess.saveCurrency(currentCurrency);
        return currencyToAdd;
    }
}
