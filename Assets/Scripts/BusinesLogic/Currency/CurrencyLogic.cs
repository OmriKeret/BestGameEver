using UnityEngine;
using System.Collections;

public class CurrencyLogic : MonoBehaviour {

    CurrencyData currencyDataAccess;
    public int deviderToScore = 1/100;
    void Start()
    {
        currencyDataAccess = GameObject.Find("GameManagerData").GetComponent<CurrencyData>();
    }

    public int updateCurrencyByScore(int score)
    {
        var currencyToAdd = score / deviderToScore; // + coins
        var currentCurrency = currencyDataAccess.loadCurrency();
        currentCurrency.PJ += currencyToAdd;
        currencyDataAccess.saveCurrency(currentCurrency);
        return currencyToAdd;
    }
}
