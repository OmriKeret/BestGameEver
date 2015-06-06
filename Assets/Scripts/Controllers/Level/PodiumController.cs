using UnityEngine;
using System.Collections;

public class PodiumController : MonoBehaviour {
    PodiumLogic podiumLogic;
	// Use this for initialization
	void Awake () {
        podiumLogic = GetComponent<PodiumLogic>();
        podiumLogic.initPodium(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            podiumLogic.playerLandedOnPlatform();
        }
    }
}
