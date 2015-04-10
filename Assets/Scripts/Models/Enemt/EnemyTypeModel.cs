using UnityEngine;
using System.Collections;

public class EnemyTypeModel {

    public static System.Type GetLogicType(int i_Type)
    {
        switch (i_Type)
        {
            case 1:
                return new StupidAILogic().GetType();
        }
        return null;
    }

    public static System.Type GetStatsType(int i_Type)
    {
        switch (i_Type)
        {
            case 1:
                return new StupidGeneralStats().GetType();
        }
        return null;
    }
    

}
