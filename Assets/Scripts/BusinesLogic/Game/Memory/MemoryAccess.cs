using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class MemoryAccess : MonoBehaviour {
    private static string scoreFileName = "/playerInfoScore.dat";
    private static string missionsFileName = "/playerInfoMission.dat";//missions
    private static string currencyFileName = "/playerInfoCurrency.dat";
    private static string clothHatsFileName = "/playerInfoClothHats.dat";
    private static string clothPonchoesFileName = "/playerInfoClothPonchoes.dat";
    private static string clothSwordsFileName = "/playerInfoClothSwords.dat";
    private static string toturialBasicFileName = "/playerInfoToturialBasic.dat";
   // private static string toturialBasicFileName = "/playerInfoToturialBasic.dat";
    public static MemoryAccess memoryAccess;

	// Use this for initialization
	void Awake() {
        if (memoryAccess == null)
        {
            DontDestroyOnLoad(gameObject);
            memoryAccess = this;
        }
        else if (memoryAccess != this)
        {
            Destroy(gameObject);
        }
	}


    public void SaveScore(IOScoreModel score)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + scoreFileName);
            bf.Serialize(file, score);
            file.Close();
        }
        catch
        {
        }
    }
    public IOScoreModel LoadScore()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + scoreFileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + scoreFileName, FileMode.Open);
                IOScoreModel score = (IOScoreModel)bf.Deserialize(file);
                file.Close();
                return score;
            }
        }
        catch
        {
            return null;
        }
        return null;
    }

    public void SaveCurrency(IOCurrencyModel currency)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + currencyFileName);
            bf.Serialize(file, currency);
            file.Close();
        }
        catch
        {
        }
    }

    public IOCurrencyModel LoadCurrency()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + currencyFileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + currencyFileName, FileMode.Open);
                IOCurrencyModel currency = (IOCurrencyModel)bf.Deserialize(file);
                file.Close();
                return currency;
            }
        }
        catch
        {
            return new IOCurrencyModel();
        }
        return new IOCurrencyModel();
    }

    public void SaveMissions(IOMissionModel missions)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + missionsFileName);
            bf.Serialize(file, missions);
            file.Close();
        }
        catch
        {
        }
    }

    public IOMissionModel LoadMission()
    {
        try
        {

            if (File.Exists(Application.persistentDataPath + missionsFileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + missionsFileName, FileMode.Open);
                IOMissionModel mission = (IOMissionModel)bf.Deserialize(file);
                file.Close();
                return mission;
            }
        }
        catch
        {
            Debug.Log("didn't load missions");
            return null;
        }
          Debug.Log("didn't load missions");
        return null;
        
    }

    //equipment
    //hats
    public IOTotalClothModel LoadHats()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + clothHatsFileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + clothHatsFileName, FileMode.Open);
                IOTotalClothModel cloth = (IOTotalClothModel)bf.Deserialize(file);
                file.Close();
                return cloth;
            }
        }
        catch
        {
            return null;
        }
        return null;
    }

    public void SaveHats(IOTotalClothModel hats)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + clothHatsFileName);
            bf.Serialize(file, hats);
            file.Close();
        }
        catch
        {
        }
    }

    //punchos
    public IOTotalClothModel LoadPonchoes()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + clothPonchoesFileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + clothPonchoesFileName, FileMode.Open);
                IOTotalClothModel cloth = (IOTotalClothModel)bf.Deserialize(file);
                file.Close();
                return cloth;
            }
            
        }
        catch
        {
            return null;
        }
        return null;
    }

    public void SavePonchoes(IOTotalClothModel hats)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + clothPonchoesFileName);
            bf.Serialize(file, hats);
            file.Close();
        }
        catch
        {
        }
    }

    //Swords
    public IOTotalClothModel LoadSwords()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + clothSwordsFileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + clothSwordsFileName, FileMode.Open);
                IOTotalClothModel cloth = (IOTotalClothModel)bf.Deserialize(file);
                file.Close();
                return cloth;
            }
        }
        catch
        {
            return null;
        }
        return null;
    }

    public void SaveSwords(IOTotalClothModel Swords)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + clothSwordsFileName);
            bf.Serialize(file, Swords);
            file.Close();
        }
        catch
        {
        }
    }

    public IOBasicToturialModel LoadBasicToturial()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + toturialBasicFileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + toturialBasicFileName, FileMode.Open);
                IOBasicToturialModel data = (IOBasicToturialModel)bf.Deserialize(file);
                file.Close();
                return data;
            }
            else
            {
                Debug.Log("couldn't find toturial data file ");
            }
            
        }
        catch
        {
            return null;
        }
        return null;
    }

    public bool SaveBasicToturial(IOBasicToturialModel data)
    {
        Debug.Log("trying to save toturial info: finished basicToturial = " + data.finishedBasicToturial);
        var success = false;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + toturialBasicFileName);
            bf.Serialize(file, data);
            file.Close();
            success = true;
        }
        catch
        {
            Debug.Log("couldn't save toturial data");
        }
        return success;
    }
}
