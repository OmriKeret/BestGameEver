using UnityEngine;
using System.Collections;
using System;

public class PodiumLogic : MonoBehaviour {
    
    //platrofm down and up parameters
    public float timeToGoDown;
    public float timeToComeBackUp;
    private float delayToGoDown = 0f;
    public float angleToShakeTo = 4;

    //podium general parameters
    public Vector3 originalLocation;
    public Vector3 downLocation;

    //platform time managment
    public float immunityTimeFromBeginning;
//    protected float timeFromGoingDown = 100000f;
    public float timeToWaitDown = 2f;

    
    private bool shouldCountForBreak;
    private bool shouldCountForBuild;
    private float timeStartedCounting;
    protected bool goingUp = false;
    protected bool goingDown = false;
    protected bool firstJump = true;
    protected bool secondJump = true;
    protected GameObject podium;
    public float PodiumSpeed;

    private CannonManagerLogic cannonManager;

    // action to do when finishing to break (destroy or go down)
    Action actionToDoWhenFinisheToBreak;
    //animation
    Animator animation;
    Collider2D collider;
	// Use this for initialization
	void Start () {
        animation = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        immunityTimeFromBeginning = 0.1f;
        goingDown = false;
        firstJump = true;
        secondJump = true;
	    cannonManager = FindObjectOfType<CannonManagerLogic>();
	}
    void FixedUpdate()
    {
        if (Time.time - timeStartedCounting > delayToGoDown && shouldCountForBreak)
        {
            breakPodium();
			shouldCountForBreak = false;
        }
        else if (Time.time - timeStartedCounting > timeToWaitDown && shouldCountForBuild)
        {
            buildPodium();
            shouldCountForBuild = false;
       
        }
        
    }

    public void initPodium(GameObject i_Podium)
    {
        podium = i_Podium;
        originalLocation = podium.transform.position;
        downLocation = originalLocation - new Vector3(0, 20, 0);
        podium.transform.position = downLocation;
        startGoUp();
    }

    public void Move(Vector3[] i_Path)
    {
        startGoUp();
        if (!i_Path.Equals(PodiumPaths.NotMoveing))
        {
            LeanTween.move(podium, i_Path, 5).setEase(LeanTweenType.linear);
        }
        
    }

    protected void startGoUp()
    {
        goingUp = true;
        goingDown = false;
        firstJump = true;
        secondJump = true;
        LeanTween.cancel(podium,false);
        resetRotation();
        LeanTween.move(podium, originalLocation, timeToComeBackUp).setOnComplete(() => {goingUp = false;});
    }

    protected void resetRotation()
    {
        podium.transform.rotation = Quaternion.identity;
    }


    internal void playerLandedOnPlatform()
    {
        startGoDownTrigger();
    }

    protected void startGoDownTrigger()
    {

        //if you are still immune return
        //Debug.Log("fixed time is : " + Time.fixedTime);
        //Debug.Log("immunityTimeFromBeginning: " + immunityTimeFromBeginning);
        if (firstJump)
        {
            firstJump = false;
            return; 
        }
        //if (secondJump)
        //{
        //    secondJump = false;
        //    return;
        //}
        if (goingDown)
        {
            return;
        }
        else
        {
            goingDown = true;
        }
       
        timeStartedCounting = Time.time;
		shouldCountForBreak = true;
    }

    public void breakPodium() {
       collider.enabled = false;
       animation.SetTrigger("Break");
       
    }

    private void buildPodium()
    {
        collider.enabled = true;
        animation.SetTrigger("Build");
    }

    public void finishedBreaking()
    {
        if (actionToDoWhenFinisheToBreak == null)
        {
            this.transform.position = downLocation;
            shouldCountForBuild = true;
            timeStartedCounting = Time.time;
        }
        else
        {
            actionToDoWhenFinisheToBreak.Invoke();
        }
    }

    public void downForGood()
    {
        breakPodium();
        actionToDoWhenFinisheToBreak = ()=>{Destroy(this.gameObject);};
    }

    internal void playerIsCamping()
    {
        //TODO: initiaite CANNON
    }
}
