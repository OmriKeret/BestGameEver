﻿using UnityEngine;
using System.Collections;

public class PodiumController : MonoBehaviour {
    PodiumLogic podiumLogic;
    private CannonManagerLogic cannonManager;

    // Cannon parametets
    public float timeOnPodiumBeforeCannonArrives = 2.5f;
    private float timeLandedOnPodium = 1000000000; // big value so it wont initiate on begining

	// Use this for initialization
	void Awake () {
        podiumLogic = GetComponent<PodiumLogic>();
        podiumLogic.initPodium(this.gameObject);
	}

    void Start()
    {
        cannonManager = new CannonManagerLogic();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - timeLandedOnPodium > timeOnPodiumBeforeCannonArrives)
        {
            podiumLogic.playerIsCamping();
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            timeLandedOnPodium = Time.time;
            cannonManager.StartTimer();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            timeLandedOnPodium = 1000000000; // big value so it wont initiate
            podiumLogic.playerLandedOnPlatform();
            cannonManager.StopTimer();
        }
    }
}
