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
    public float podiumOriginalX = 2f;
    public float podiumOriginalY = -7.22f;

    //platform time managment
    public float immunityTimeFromBeginning;
//    private float timeFromGoingDown = 100000f;
    public float timeToWaitDown;

    private bool goingDown = false;
    GameObject podium;
	// Use this for initialization
	void Start () {
        podium = GameObject.Find("Podium");
	}
    void FixedUpdate()
    {
    }

    private void startGoUp()
    {
        goingDown = false;
        LeanTween.cancel(podium,false);
        resetRotation();
        LeanTween.move(podium, new Vector2(podiumOriginalX, podiumOriginalY), timeToComeBackUp).setDelay(timeToWaitDown);

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
        Debug.Log("fixed time is : " + Time.fixedTime);
        Debug.Log("immunityTimeFromBeginning: " + immunityTimeFromBeginning);
        if (Time.fixedTime - immunityTimeFromBeginning < 0)
        {
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
        Debug.Log("podium go down");
        LeanTween.rotateZ(podium, angleToShakeTo, shakingTime).setDelay(timeToWaitBeforeShaking).setOnComplete(
           () =>
           {
               shakingPingPong();
           });
        LeanTween.move(podium, new Vector2(podiumOriginalX, podiumOriginalY - 20), timeToGoDown).setDelay(timeToShake + timeToWaitBeforeShaking).setOnComplete(
           () =>
           {  
               startGoUp();
           }); ;
    }
    private void shakingPingPong()
    {
        LeanTween.rotateZ(podium, angleToShakeTo * -1, shakingTime).setLoopPingPong();
    }
}
