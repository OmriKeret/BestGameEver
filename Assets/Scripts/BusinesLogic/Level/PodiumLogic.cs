using UnityEngine;
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
//    protected float timeFromGoingDown = 100000f;
    public float timeToWaitDown;

    protected bool goingDown = false;
    protected bool firstJump = true;
    protected bool secondJump = true;
    protected GameObject podium;
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

    protected void startGoUp()
    {
        goingDown = false;
        firstJump = true;
        secondJump = true;
        LeanTween.cancel(podium,false);
        resetRotation();
        LeanTween.move(podium, originalLocation, timeToComeBackUp).setDelay(timeToWaitDown);

    }

    protected void resetRotation()
    {
        podium.transform.rotation = Quaternion.identity;
    }


    internal void playerLandedOnPlatform()
    {
        startGoDown();
    }

    protected void startGoDown()
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
    protected void shakingPingPong()
    {
        LeanTween.rotateZ(podium, angleToShakeTo * -1, shakingTime).setLoopPingPong();
    }
}
