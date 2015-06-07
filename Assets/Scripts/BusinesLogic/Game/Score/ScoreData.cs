using UnityEngine;
using System.Collections;

public class ScoreData : MonoBehaviour {

    public int highScore;
	// Use this for initialization
	void Start () {
        loadHighScore();
	}

    public void loadHighScore()
    {
        try
        {
            var score = MemoryAccess.memoryAccess.LoadScore();
            highScore = score.scroe;
        }
        catch {
            highScore = 0;
            }
    }
    public void saveHighScore(int score)
    {
        try
        {
            MemoryAccess.memoryAccess.SaveScore(new IOScoreModel { scroe = highScore });
        }
        catch
        {
           
        }

    }
    public void updateHighScore(int score)
    {
        if (highScore < score)
        {
            highScore = score;
            saveHighScore(score);
        }
    }

}
