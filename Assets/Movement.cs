﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

	public Text text;
	public Text dynamicText;//Dynamic version 1.1.0
	public bool DEBUG_MODE;
	public float DASH_FORCE = 400f;
	public int MAX_DASH_TIME = 50;
	int dashTime;
	public int MAX_DASH_NUM = 2;
	private int dashNum;
	public static int score = 0;
	private static int streak = 0;
	private int SCORE_MULTIPLIER = 3;
	private GameObject bomb, bombPrefab;

	// Use this for initialization
	void Start () {
		DEBUG_MODE = false;
		dashNum = MAX_DASH_NUM;
		score = 0;
		streak = 0;
		dashTime = dashTime = MAX_DASH_TIME + 1;
		bombPrefab = Resources.Load ("bomb4") as GameObject;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		this.rigidbody2D.gravityScale = 1;
		dashTime = MAX_DASH_TIME + 1;
		if (coll.gameObject.tag.Equals ("Enemy")||coll.gameObject.tag.Equals ("Speedy")) {
						score += 10;
						streak++;
						if (coll.gameObject.tag.Equals ("Speedy"))
								score += 20;
						

				}
		score += ((streak / SCORE_MULTIPLIER) * dashNum)*10;
		dashNum = MAX_DASH_NUM;
		if (DEBUG_MODE) {
			Debug.Log ("Hit " + coll.gameObject.tag + "\nDash number reser to 0!");
		}
		if (streak == 10)
						bomb = Instantiate (bombPrefab) as GameObject;

	}

	// Update is called once per frame
	void Update () {
		if (transform.position.y < GeneralPhysics.BOTTOM_EDGE-2) {
			death();
				}
		if (Input.GetButtonDown ("Fire1")) {
						dash(getPressDirection ());

				}
		if (dashTime < MAX_DASH_TIME)
						dashTime++;
				else if (dashTime == MAX_DASH_TIME) {
			dashTime++;
			//rigidbody2D.velocity = new Vector2(0,0);
			rigidbody2D.gravityScale = 1;
				}

		text.text = "Score: "+score.ToString()+"\nStreak: "+streak.ToString();


			if (Input.GetKey("escape"))
				Application.Quit();
			

		
		//Dynamic version 1.1.0
		if(Input.GetKey("up"))
		   MAX_DASH_TIME++;
		if(Input.GetKey("down"))
			MAX_DASH_TIME--;
		if (Input.GetKey ("left"))
						DASH_FORCE--;
		if (Input.GetKey ("right"))
			DASH_FORCE++;

		dynamicText.text = "Dash Force: " + DASH_FORCE + "\nDash Time: " + MAX_DASH_TIME;


		//Invisible walls
		if (transform.position.x < GeneralPhysics.LEFT_EDGE)		
			transform.position = new Vector3 (GeneralPhysics.RIGHT_EDGE - 0.1f, transform.position.y, transform.position.z);
		else if (transform.position.x > GeneralPhysics.RIGHT_EDGE) 	
			transform.position = new Vector3 (GeneralPhysics.LEFT_EDGE + 0.1f, transform.position.y, transform.position.z);
	}

	Vector3 getPressDirection()
	{
		Vector3 location = new Vector3(0,0,-500);

		if (Input.GetButtonDown("Fire1")) {
			location = -transform.position;
			location += Camera.main.ScreenToWorldPoint(Input.mousePosition);
			location.z = transform.position.z;
			location = location.normalized;
		}
		if (DEBUG_MODE)
			Debug.Log ("getPressLocation: press in " + location);
		return location;
	}

	void dash(Vector3 location)
	{
		dashTime = 0;
		if (dashNum == 1)
						streak = 0;
		if (dashNum == 0) {
			if (DEBUG_MODE)
				Debug.Log("dash: Passed maximum ammount of dashes");
			return;
				}
		this.rigidbody2D.gravityScale = 0;
		rigidbody2D.velocity = new Vector3 (0, 0, 0);
		rigidbody2D.AddForce (location*DASH_FORCE);
		dashNum--;
		if (streak == 0 && bomb != null)
			Destroy (bomb);
	}

	void death(){
		Application.LoadLevel (0);
		}
}