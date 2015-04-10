using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreLogic : MonoBehaviour {
    private MissionLogic missionLogic;
    Text scoreText;
    int score = 0;
	// Use this for initialization
	void Start () {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        missionLogic = this.gameObject.GetComponent<MissionLogic>();
	}
	
	public void addPoint(AddPointModel model) {
		var scoreToAdd = 0;
		for(int i = 0; i < model.combo; i ++) {
			scoreToAdd += (int)model.type * 1327;
		}
		score += scoreToAdd;
        scoreText.text = string.Format("Score: {0}", score);
        missionLogic.gotScoreOf(score);
    }
}
