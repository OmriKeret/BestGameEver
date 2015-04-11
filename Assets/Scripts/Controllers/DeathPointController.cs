using UnityEngine;
using System.Collections;

public class DeathPointController : MonoBehaviour {
    DeathLogic deathLogic;
	// Use this for initialization
	void Start () {
        deathLogic = GameObject.Find("Logic").GetComponent<DeathLogic>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            deathLogic.DeathByFall();
        }
    }
}
