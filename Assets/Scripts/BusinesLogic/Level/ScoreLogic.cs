using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreLogic : MonoBehaviour {
    private MissionLogic missionLogic;
    Text scoreText;
    ScoreData scoreDataAccess;
    public int score = 0;
	// Use this for initialization
	void Start () {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        scoreDataAccess = GameObject.Find("GameManagerData").GetComponent<ScoreData>();
	}
	
	public void addPoint(AddPointModel model) {
		var scoreToAdd = 0;
		scoreToAdd += (int)model.type * 100;
		for(int i = 0; i < model.combo; i ++) {
			scoreToAdd += (int)model.type * 1327;
		}
		score += scoreToAdd;
        var scoreTxt = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                 "{0:0,0}", score);
        scoreText.text = string.Format("{0}\nPOINTS", scoreTxt);
        missionLogic.gotScoreOf(score);
    }

    internal int multiplyScoreAfterFinishingMissions(int tier)
    {
        score = (int)(score * tier * 1.5);
        return score;
    }

    internal void saveScoreData()
    {
        scoreDataAccess.updateHighScore(score);
    }

    public int getHighScore()
    {
        scoreDataAccess.loadHighScore();
        return scoreDataAccess.highScore;
    }
}
