using UnityEngine;
using System.Collections;

public class TouchInterpeter : MonoBehaviour {

	MovementController PlayerMovmentController;
	PhyisicsController playerPhyisicsController;
	Rigidbody2D player;
	public Vector2 startPos;
	public Vector2 direction;
	Vector2 realWorldTouch;
	// Use this for initialization
	void Start () {
		playerPhyisicsController = GameObject.Find("PlayerManager").GetComponent<PhyisicsController>();
		PlayerMovmentController = GameObject.Find("PlayerManager").GetComponent<MovementController>();
		player = GameObject.Find("PlayerManager").GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		bool directionChosen = false;
		bool holdingScreen = false;
		bool stopHover = false;
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
				realWorldTouch =  (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
				Vector2 realWorldCharPos =  player.position;

				direction = realWorldTouch - realWorldCharPos;
				Debug.Log ("touch position: " + realWorldTouch);
				Debug.Log ("player position: " + realWorldCharPos);
				Debug.Log ("direction: " + direction);
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
		if (directionChosen) {
			direction.Normalize();
			PlayerMovmentController.Move(direction, realWorldTouch);
			//playerMovmentController(direction);

		}
		if (holdingScreen) {
			PlayerMovmentController.Hover();
		}
	}
}
