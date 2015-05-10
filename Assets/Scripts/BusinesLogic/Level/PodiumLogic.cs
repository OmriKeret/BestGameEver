﻿using UnityEngine;
using System.Collections;

public class PodiumLogic : MonoBehaviour {
    
    //platrofm down and up parameters
    public float timeToGoDown;
    public float timeToComeBackUp;
    public float timeToShake;
    public float timeToWaitBeforeShaking = 1f;
    public float shakingTime = 0.1f;
    public float angleToShakeTo = 4;

    //podium general parameters
    public Vector3 originalLocation;
    public Vector3 downLocation;

    //platform time managment
    public float immunityTimeFromBeginning;
//    private float timeFromGoingDown = 100000f;
    public float timeToWaitDown;

    private bool goingDown = false;
    private bool firstJump = true;
    private bool secondJump = true;
    GameObject podium;
	// Use this for initialization
	void Start () {
        immunityTimeFromBeginning = 0.1f;
        goingDown = false;
        firstJump = true;
        secondJump = true;
	}
    void FixedUpdate()
    {
    }

    public void initPodium(GameObject i_Podium)
    {
        podium = i_Podium;
        originalLocation = podium.transform.position;
        downLocation = originalLocation - new Vector3(0, 20, 0);
        podium.transform.position = downLocation;
        startGoUp();
    }

    private void startGoUp()
    {
        goingDown = false;
        firstJump = true;
        secondJump = true;
        LeanTween.cancel(podium,false);
        resetRotation();
        LeanTween.move(podium, originalLocation, timeToComeBackUp).setDelay(timeToWaitDown);

    }

    private void resetRotation()
    {
        podium.transform.rotation = Quaternion.identity;
    }


    internal void playerLandedOnPlatform()
    {
        startGoDown();
    }

    private void startGoDown()
    {

        //if you are still immune return
        //Debug.Log("fixed time is : " + Time.fixedTime);
        //Debug.Log("immunityTimeFromBeginning: " + immunityTimeFromBeginning);
        if (firstJump)
        {
            firstJump = false;
            return; 
        }
        if (secondJump)
        {
            secondJump = false;
            return;
        }
        if (goingDown)
        {
            return;
        }
        else
        {
            goingDown = true;
        }
       Debug.Log("podium go down ");
       LeanTween.rotateZ(podium, angleToShakeTo, shakingTime).setDelay(timeToWaitBeforeShaking).setOnComplete(
          () =>
          {
              shakingPingPong();
          });
       LeanTween.move(podium, downLocation, timeToGoDown).setDelay(timeToShake + timeToWaitBeforeShaking).setOnComplete(
          () =>
          {
              startGoUp();
          });
    }

    public void downForGood()
    {
        Debug.Log("podium go down and out");
        LeanTween.rotateZ(podium, angleToShakeTo, shakingTime).setDelay(timeToWaitBeforeShaking).setOnComplete(
           () =>
           {
               shakingPingPong();
           });
        LeanTween.move(podium, downLocation, timeToGoDown).setDelay(timeToShake + timeToWaitBeforeShaking).setOnComplete(
           () =>
           {
               Destroy(podium);
           });
    }
    private void shakingPingPong()
    {
        LeanTween.rotateZ(podium, angleToShakeTo * -1, shakingTime).setLoopPingPong();
    }
}
