using UnityEngine;
using System.Collections;

public class CurrencyData : MonoBehaviour {

    public IOCurrencyModel loadCurrency()
    {
        return MemoryAccess.memoryAccess.LoadCurrency();
    }

    public void saveCurrency(IOCurrencyModel currency)
    {
        MemoryAccess.memoryAccess.SaveCurrency(currency);
    }
}
