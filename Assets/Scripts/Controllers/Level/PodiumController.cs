using UnityEngine;
using System.Collections;

public class PodiumController : MonoBehaviour {
    PodiumLogic podiumLogic;
	// Use this for initialization
	void Start () {
        podiumLogic = GameObject.Find("Logic").GetComponent<PodiumLogic>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            podiumLogic.playerLandedOnPlatform();
        }
    }
}
