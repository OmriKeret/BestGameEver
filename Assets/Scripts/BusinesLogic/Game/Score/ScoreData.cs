using UnityEngine;
using System.Collections;

public class ScoreData : MonoBehaviour {

    int highScore;
	// Use this for initialization
	void Start () {
        loadHighScore();
	}

    public void loadHighScore()
    {
        var score = MemoryAccess.memoryAccess.LoadScore();
        highScore = score.scroe;
    }
    public void saveHighScore(int score)
    {
        MemoryAccess.memoryAccess.SaveScore(new IOScoreModel { scroe = highScore });
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
