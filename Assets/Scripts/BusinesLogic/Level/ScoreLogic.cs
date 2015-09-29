using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreLogic : MonoBehaviour {
    private MissionLogic missionLogic;
    Text scoreText;
    ScoreData scoreDataAccess;
    public int kills = 0;
	// Use this for initialization
	void Start () {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
        scoreDataAccess = GameObject.Find("GameManagerData").GetComponent<ScoreData>();
	}
	
	public void addPoint(AddPointModel model) {
        //var scoreToAdd = 0;
        //scoreToAdd += (int)model.type * 100;
        //for(int i = 0; i < model.combo; i ++) {
        //    scoreToAdd += (int)model.type * 1327;
        //}
        //score += scoreToAdd;
        kills++;
        var scoreTxt = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                 "{0:0,0}", kills);
        scoreText.text = string.Format("{0}\nKILLS", scoreTxt);
        missionLogic.gotScoreOf(kills);
    }


    internal void saveScoreData()
    {
        scoreDataAccess.updateHighScore(kills);
    }

    public int getHighScore()
    {
        scoreDataAccess.loadHighScore();
        return scoreDataAccess.highScore;
    }
}
