using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchInterpeter : MonoBehaviour {

	MovementController PlayerMovmentController;
	PhyisicsController playerPhyisicsController;
    EventSystem events;
	Rigidbody2D player;
	public Vector2 startPos;
	public Vector2 direction;
	Vector2 realWorldTouch;
    public Button pauseButton;
    public bool isMovmentDisabled;
    Camera camera;
	// Use this for initialization
	void Start () {
		playerPhyisicsController = GameObject.Find("PlayerManager").GetComponent<PhyisicsController>();
		PlayerMovmentController = GameObject.Find("PlayerManager").GetComponent<MovementController>();
		player = GameObject.Find("PlayerManager").GetComponent<Rigidbody2D>();
        pauseButton = GameObject.Find("Pause").GetComponent<Button>();
        events = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        camera = GameObject.Find("Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!isMovmentDisabled) {
		    bool directionChosen = false;
		    bool holdingScreen = false;
		    bool stopHover = false;
            Vector2 realWorldCharPos;
            if (Input.GetMouseButtonDown(0))
            {
                var touchPos = Input.mousePosition;
                touchPos.z = 20f;
                realWorldTouch = camera.ScreenToWorldPoint(touchPos);
                realWorldCharPos = player.position;
                direction = realWorldTouch - realWorldCharPos;
                directionChosen = true;
                holdingScreen = false;
                playerPhyisicsController.StopHoverPhyisics();
            }
 
		    if (Input.touchCount > 0) {
			    var touch = Input.GetTouch(0);
			    // Handle finger movements based on touch phase.
			    switch (touch.phase) {
				    // Record initial touch position.
			    case TouchPhase.Began:
				    playerPhyisicsController.fingerHoldHover();
				    directionChosen = false;
				    holdingScreen = true;
				    break;
				    // Determine if finger is touching the screen
		    //	case TouchPhase.Stationary:
			    //	startPos = touch.position;
		    //		directionChosen = false;
		    //		holdingScreen = true;
		    //		break;
				    // Determine direction by comparing the current touch position with the initial one.
			    case TouchPhase.Moved:
				    //direction = touch.position - startPos;
				    holdingScreen = true;
				    break;
				
				    // Report that a direction has been chosen when the finger is lifted.
			    case TouchPhase.Ended:
				    playerPhyisicsController.StopHoverPhyisics();
				    //touch is in pixels, we convert to unity world point
                    var touchPos = touch.position;
				    realWorldTouch =  (Vector2)camera.ScreenToWorldPoint(touch.position);
				    realWorldCharPos =  player.position;

				    direction = realWorldTouch - realWorldCharPos;
 				    directionChosen = true;
				    holdingScreen = false;
				    break;

			    case TouchPhase.Canceled:
				    directionChosen = false;
				    holdingScreen = false;
				    stopHover = true;
				    break;
			    }
		    }
            if (events.currentSelectedGameObject == pauseButton.gameObject)
            {
              //  Debug.Log("button 1 clicked");
                return;
           
            }

            if (directionChosen && !isMovmentDisabled)
            {

			    direction.Normalize();
			    PlayerMovmentController.Move(direction, realWorldTouch);
			    //playerMovmentController(direction);
			    directionChosen = false;
			    stopHover = true;

		    }
            if (holdingScreen && !isMovmentDisabled)
            {
	    //		PlayerMovmentController.Hover();
		    }
		    if (stopHover) {
		    //	playerPhyisicsController.StopHoverPhyisics();
		    }
	    }
    }

        public void SetDisableMovment()
        {
            this.isMovmentDisabled = true;
        }
        public void UnsetDisableMovment()
        {
            this.isMovmentDisabled = false;
        }
    
}
