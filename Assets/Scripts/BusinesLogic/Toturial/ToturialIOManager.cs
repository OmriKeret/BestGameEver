using UnityEngine;
using System.Collections;

public class ToturialIOManager : MonoBehaviour {


    public IOBasicToturialModel loadBasicToturialInfo()
    {
        return MemoryAccess.memoryAccess.LoadBasicToturial();
    }

    public bool saveBasicToturialInfo(IOBasicToturialModel data)
    {
        return MemoryAccess.memoryAccess.SaveBasicToturial(data);
    }
}
