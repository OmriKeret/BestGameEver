using UnityEngine;
using System.Collections;

public class GeneralPhysics : MonoBehaviour {

	public static float RIGHT_EDGE = 9.75f;
	public static float LEFT_EDGE = -RIGHT_EDGE;
	public static float TOP_EDGE = 5.8f;
	public static float BOTTOM_EDGE = -TOP_EDGE;
	public static float DELTA_FLOAT = 0.5f;
	public static bool isTutorialMode = true;
	public static int highScore=0,highStreak=0;
	public static int DASH_FORCE=1300, DASH_TIME=20;

	public static Vector3 getRandomLocation(){
		return new Vector3 (Random.Range (LEFT_EDGE, RIGHT_EDGE), Random.Range (BOTTOM_EDGE, TOP_EDGE));
	}

	public static Vector3 getRandomOuterbox(){
		int rnd = Random.Range(0,4);
		switch (rnd) {
		case 0: return new Vector3 (LEFT_EDGE, Random.Range (BOTTOM_EDGE, TOP_EDGE));
		case 1: return new Vector3 (RIGHT_EDGE, Random.Range (BOTTOM_EDGE, TOP_EDGE));
		case 2: return new Vector3 (Random.Range (LEFT_EDGE, RIGHT_EDGE), TOP_EDGE);
		case 3: return new Vector3 (Random.Range (LEFT_EDGE, RIGHT_EDGE), BOTTOM_EDGE);
				}
		Debug.Log ("getRandomOuterbox: Out of bound");
		return new Vector3(0,0,0);
	}


	public static bool onSpot(Vector3 v1, Vector3 v2,float delta)
	{
		if (Mathf.Abs (v1.x - v2.x) < delta)
			if (Mathf.Abs (v1.y - v2.y) < delta)
				return true;
		return false;
		}

	public static bool onSpot(Vector3 v1, Vector3 v2)
	{
		return onSpot (v1, v2, DELTA_FLOAT);
	}

	public static void placeWalls(){

		GameObject wall = Resources.Load ("Wall") as GameObject;
		Instantiate (wall, new Vector3(RIGHT_EDGE,0,0), Quaternion.identity);
		Instantiate (wall, new Vector3(LEFT_EDGE,0,0), Quaternion.identity);

		}
}
