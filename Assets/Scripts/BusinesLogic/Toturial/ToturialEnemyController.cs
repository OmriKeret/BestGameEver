using UnityEngine;
using System.Collections;

public class ToturialEnemyController : MonoBehaviour {
    ToturialController toturialController;
	// Use this for initialization
	void Awake () {
        toturialController = GameObject.Find("ToturialManager").GetComponent<ToturialController>();
	}

	public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag.Equals("Player"))
        {
            toturialController.playerHitEnemy();
        }
	}



}
