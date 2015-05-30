using UnityEngine;
using System.Collections;

public class ToturialIOManager : MonoBehaviour {


    public IOBasicToturialModel loadBasicToturialInfo()
    {
        return MemoryAccess.memoryAccess.LoadBasicToturial();
    }

    public void saveBasicToturialInfo(IOBasicToturialModel data)
    {
        MemoryAccess.memoryAccess.SaveBasicToturial(data);
    }
}
