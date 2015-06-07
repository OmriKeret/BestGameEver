using UnityEngine;
using System.Collections;

public class ToturialTouchChecker : MonoBehaviour {

    BoxCollider touchBox;
    Camera camera;
    Rigidbody2D player;
    public bool shouldCheckTouch;
    MovementController PlayerMovmentController;
    Vector3 realWorldTouch;
    Vector3 realWorldCharPos;
    ToturialLogic toturialLogic;
	// Use this for initialization
	void Awake () {
        touchBox = this.GetComponentInChildren<BoxCollider>();
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        player = GameObject.Find("PlayerManager").GetComponent<Rigidbody2D>();
        PlayerMovmentController = GameObject.Find("PlayerManager").GetComponent<MovementController>();
        toturialLogic = this.GetComponent<ToturialLogic>();
        shouldCheckTouch = false;

	}
	
	// Update is called once per frame
	void Update () {
        if (shouldCheckTouch)
        {
            // Touch
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended)
                {

                    Ray ray = camera.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
                    RaycastHit hit;

					if (Physics.Raycast(ray, out hit,1000))
					{
						if(hit.transform.tag.Equals("TouchCheck")) {
	                        //if touched do something
	                        toturialLogic.playerTouchedTheArrow();
							Vector3 touchFixedPosition = new Vector3(touch.position.x,touch.position.y, 20f);
							realWorldTouch = (Vector2)Camera.main.ScreenToWorldPoint(touchFixedPosition);
	                        realWorldCharPos = player.position;
	                        var direction = realWorldTouch - realWorldCharPos;
	                        direction.Normalize();
	                        PlayerMovmentController.Move(direction, realWorldTouch);
	                        return;
						}
                    }
                }
            }

            // Mouse
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = Input.mousePosition;
             

				Ray ray = camera.ScreenPointToRay(mousePos);
				RaycastHit hit;
				Debug.DrawLine(ray.origin, ray.origin + ray.direction * 1000);
                    if (Physics.Raycast(ray, out hit,1000))
                    {
					if(hit.transform.tag.Equals("TouchCheck")) {
	                        mousePos.z = 20f;
					//	Debug.Log("hit");
	                        //if touched do something
	                         toturialLogic.playerTouchedTheArrow();
	                        realWorldTouch = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
	                        realWorldCharPos = player.position;
	                        var direction = realWorldTouch - realWorldCharPos;
	                        direction.Normalize();
	                        PlayerMovmentController.Move(direction, realWorldTouch);
	                        return;
					}
                    }
                
            }
 
        }
	}

    public void setShouldCheckTouch()
    {
        shouldCheckTouch = true;
    }
    public void setShouldNotCheckTouch()
    {
        shouldCheckTouch = false;
    }

    internal void MoveToNextPosition()
    {
        touchBox.transform.position = new Vector3(3, -4, 0f);
        touchBox.size = new Vector2(5f, 6f);
    }

    internal void setCheckingPosition(Vector3 vector3)
    {

        touchBox.transform.position = vector3;
        touchBox.size = new Vector2(5f, 7f);
    }
}
