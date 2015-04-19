using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class MemoryAccess : MonoBehaviour {
    private static string scoreFileName = "/playerInfoScore.dat";
    private static string missionsFileName = "/playerInfoMissions.dat";
    public static MemoryAccess memoryAccess;
	// Use this for initialization
	void Awake () {
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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + scoreFileName);
        bf.Serialize(file, score);
        file.Close();
    }
    public IOScoreModel LoadScore()
    {
        if (File.Exists(Application.persistentDataPath + scoreFileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + scoreFileName, FileMode.Open);
            IOScoreModel score = (IOScoreModel)bf.Deserialize(file);
            file.Close();
            return score;
        }
        return new IOScoreModel();
    }

    public void SaveMissions(IOMissionModel missions)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + missionsFileName);
        bf.Serialize(file, missions);
        file.Close();
    }

    public IOMissionModel LoadMission()
    {
        if (File.Exists(Application.persistentDataPath + missionsFileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + missionsFileName, FileMode.Open);
            IOMissionModel score = (IOMissionModel)bf.Deserialize(file);
            file.Close();
            return score;
        }
        return new IOMissionModel();
    }
}
