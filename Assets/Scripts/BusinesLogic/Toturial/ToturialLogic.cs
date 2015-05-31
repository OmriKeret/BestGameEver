using UnityEngine;
using System.Collections;
using System;

public class ToturialLogic : MonoBehaviour {
    GameObject enemyManager;
    ToturialLogic toturialLogic;
    TouchInterpeter touch;
    ToturialTouchChecker toturialTouch;
    ToturialEnemySpwanner toturialEnemySpanner;
    ToturialIOManager IOManager;
    Camera camera;

    private float timeTouched;
    private float timeToWait = 0.75f;
    private bool timerIsOn;
    Action func;

    private Animator screenShade;
    Action funcToDoWhenFinishesToDarkScreen;

    private Animator tapAnimation;
    private GameObject fingerAnimationContainer;

    // Podium indicators
    bool shouldCheckForPlayerTouchingThePodiumFirstTime;
    bool shouldCheckForPlayerTOuchingThePodiumSecondTIme;

    // Move camera 
    private float timeToMoveCameraBack = 2f;

    // Action to do when player touches the arrow
    private Action playerTouchedTheArrowFunc;

    // Highlight
    private GameObject highlightContainer;
    private Animator highlightAnimation;


    private Action finishedBrightScreenFunc;
    private bool shouldSetBrightScreenFunction;
    // colloectable
    private GameObject PowerUpManager;
    
    // delay problems
    private GameObject tapArrowObject;
    void OnEnable()
    {
      
        toturialTouch = this.GetComponent<ToturialTouchChecker>();
        screenShade = GameObject.Find("ToturialManager/DarkScreen").GetComponent<Animator>();
        fingerAnimationContainer = GameObject.Find("ToturialManager/TapParent");
        tapArrowObject = GameObject.Find("ToturialManager/TapParent/TapArrow");
        tapAnimation = fingerAnimationContainer.GetComponentInChildren<Animator>();
        toturialEnemySpanner = this.GetComponent<ToturialEnemySpwanner>();
        touch = GameObject.Find("TouchInterpter").GetComponent<TouchInterpeter>();
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        highlightContainer = GameObject.Find("ToturialManager/HighlightContainer");
        highlightAnimation = highlightContainer.GetComponentInChildren<Animator>();
        IOManager = this.GetComponent<ToturialIOManager>();

        enemyManager = GameObject.Find("EnemyManager");
        PowerUpManager = GameObject.Find("PowerUpManager");
        enemyManager.SetActive(false);
        PowerUpManager.SetActive(false);
    }

    void Update()
    {
        if (timerIsOn && (Time.realtimeSinceStartup - timeTouched >= timeToWait))
        {
            timeToWait = 0.75f;
            timerIsOn = false;
            func.Invoke();
            func = null;
        }
    }

    private void startToturial()
    {
       setCameraPositionForToturial();
       touch.SetDisableMovment();
       toturialEnemySpanner.spawnEnemie();

        //DEBUG ONLY
        //shouldCheckForPlayerTouchingThePodiumFirstTime = true;
        //playerIsOnThePodium();
    }

    private void setCameraPositionForToturial()
    {
        camera.transform.position = new Vector3(3.8f, 0.65f, -13f);
    }

    internal void checkIfNeededToStartToturial()
    {
        var data = IOManager.loadBasicToturialInfo();
        bool shouldDoBasicToturial = true;
        if(data != null) 
        {
            shouldDoBasicToturial = !data.finishedBasicToturial;
        }

        if (shouldDoBasicToturial)
        {
            startToturial();
        }
        else
        {
            PowerUpManager.SetActive(true);
            enemyManager.SetActive(true);
            //Destroy this?
        }
    }

    internal void EnemyGotToPosition()
    {
        darkScreen(showArrowOnEnemy);
    }

    void showArrowOnEnemy()
    {
        showTap();
        //check for touch 
        toturialTouch.setShouldCheckTouch();
    }

    internal void playerKilledEnemy(int numberOfEnemy)
    {
        if (numberOfEnemy == 0)
        {
            toturialTouch.setShouldNotCheckTouch();
            toturialTouch.MoveToNextPosition();
            func = darkScreenAndPointToPodium;
            timerIsOn = true;
			timeTouched = Time.realtimeSinceStartup;
        }
        else if (numberOfEnemy == 1)
        {
            toturialTouch.setShouldNotCheckTouch();
            toturialTouch.setCheckingPosition(new Vector3(0.26f, 5f, 0f));
            hideTap();
            fingerAnimationContainer.transform.position = new Vector3(-2.26f, 5f, 0f);
            playerTouchedTheArrowFunc = showStaminaAndTouchChecker;
            func = darkScreenAndShowArrow;
            shouldSetBrightScreenFunction = true;
            timerIsOn = true;
            timeTouched = Time.realtimeSinceStartup;
            
        }
        else if (numberOfEnemy == 2)
        {
			Debug.Log("Killed second enemy");
            toturialTouch.setShouldNotCheckTouch();
            toturialTouch.MoveToNextPosition();
            func = darkScreenAndPointToPodium;
            timerIsOn = true;
			timeTouched = Time.realtimeSinceStartup;
            shouldCheckForPlayerTOuchingThePodiumSecondTIme = true;
        }
    }

    void darkScreenAndPointToPodium()
    {
		Debug.Log("Got to  darkScreenAndPointToPodium");
        // To freeze character in place.
        Time.timeScale = 0;
        darkScreen(showArrowOnPodium);
    }

    void showArrowOnPodium()
    {
		if (!shouldCheckForPlayerTOuchingThePodiumSecondTIme) {
			shouldCheckForPlayerTouchingThePodiumFirstTime = true;
		}
		fingerAnimationContainer.transform.position = new Vector3 (-1, -5, 0f);
        showTap();
			toturialTouch.setShouldCheckTouch ();
		
    }

    void darkScreen(Action func)
    {
        funcToDoWhenFinishesToDarkScreen = func;
        screenShade.SetBool("darkScreen",true);
       
    }

    void brightScreen()
    {
        screenShade.SetBool("darkScreen", false);
    }

   public void finishedDarkScreen()
    {
		if (funcToDoWhenFinishesToDarkScreen != null) {

			funcToDoWhenFinishesToDarkScreen.Invoke ();
		}

    }

   internal void playerTouchedTheArrow()
   {
     //  Debug.Log("player touched the arrow
       brightScreen();
       hideTap();
       Time.timeScale = 1f;
       
       highlightAnimation.SetBool("highlight", false);   
       toturialTouch.setShouldNotCheckTouch();
   }

   private void playerTouchedTheArrowDelay()
   {
      Time.timeScale = 1f;
   }
   internal void playerIsOnThePodium()
   {
       Debug.Log("player got to poium and checker is set to " + shouldCheckForPlayerTouchingThePodiumFirstTime);
       if (shouldCheckForPlayerTouchingThePodiumFirstTime)
       {
           shouldCheckForPlayerTouchingThePodiumFirstTime = false;
           MoveCameraBackAndStartFirstToturialWave();
       }
       else if (shouldCheckForPlayerTOuchingThePodiumSecondTIme)
       {
           shouldCheckForPlayerTOuchingThePodiumSecondTIme = false;
           playerFinishedBasicToturial();
       }
   }

   private void playerFinishedBasicToturial()
   {
       touch.UnsetDisableMovment();
       IOManager.saveBasicToturialInfo(new IOBasicToturialModel {finishedBasicToturial = true });
       PowerUpManager.SetActive(true);
       enemyManager.SetActive(true);
       Debug.Log("Player finished toturial!");
   }

   private void MoveCameraBackAndStartFirstToturialWave()
   {
       LeanTween.move(camera.gameObject, new Vector3(1f, 6.07f, -20.2f), timeToMoveCameraBack).setOnComplete(
             () =>
             {
                 // Set animation and touch checker position
                 toturialTouch.setCheckingPosition(new Vector3(17.26f, 7f, 0f));
                 fingerAnimationContainer.transform.position = new Vector3(14.26f, 6f, 0f);

                 toturialEnemySpanner.spawnEnemiesForWaveTwo();
             });
   }
    
   private void darkScreenAndShowArrow()
   {
       Time.timeScale = 0;
       darkScreen(showArrowAndCheckTouch);
   }
   private void showArrowAndCheckTouch()
   {
       showTap();
       toturialTouch.setShouldCheckTouch();
   }
   private void showStaminaAndTouchChecker()
   {
		Debug.Log ("Got to showStaminaAndTouchChecker");
		Time.timeScale = 0;
       toturialTouch.setCheckingPosition(new Vector3(-10.74f, 9.31f, 0f));
       fingerAnimationContainer.transform.position = new Vector3(-10.74f, 9.31f, 0f);
       darkScreen(highlightStamina);
       //screenShade.SetBool("darkScreen", true);
      // highlightStamina();
     //  highlightStamina();
       //darkScreen
       //show stamina
       //show last enemy

   }

   private void highlightStamina()
   {
		Debug.Log ("Got to highlight stamina");
       // Set highlighter position
       highlightContainer.transform.position = new Vector3(-16.74f,12.75f,0);
       highlightAnimation.SetBool("highlight", true);
       func = pointToThirdEnemy;
       timeToWait = 3f;
		timeTouched = Time.realtimeSinceStartup;
       timerIsOn = true;
	   
      

   }

   private void pointToThirdEnemy()
   {
       Debug.Log("point to thrid enemy");
       showTap();
       highlightAnimation.SetBool("highlight", false);
       toturialTouch.setShouldCheckTouch();

   }
   internal void playerCollidedWithTouchChecker()
   {
       if (playerTouchedTheArrowFunc != null)
       {
           playerTouchedTheArrowFunc.Invoke();
           playerTouchedTheArrowFunc = null;
       }
   }


   private void showTap()
   {
       tapArrowObject.SetActive(true);
       tapArrowObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
       tapAnimation.SetBool("Tap", true);
   }

   private void hideTap()
   {
       tapArrowObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
       tapAnimation.SetBool("Tap", false);
       tapArrowObject.SetActive(false);

   }
}
