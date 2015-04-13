using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

   
    private ScoreLogic scoreLogic;
	// Use this for initialization
	void Start () {
        scoreLogic = GameObject.Find("Logic").GetComponent<ScoreLogic>();
	}
	
    
	public void addScore(AddPointModel model) {
        scoreLogic.addPoint(model);
    }
}
