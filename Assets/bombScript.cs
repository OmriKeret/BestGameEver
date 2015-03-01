using UnityEngine;
using System.Collections;

public class bombScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")
		    &&GeneralPhysics.onSpot(transform.position,getPressDirection(),9f)) {
						GameObject[] reds = GameObject.FindGameObjectsWithTag ("Enemy");
						GameObject[] speedys = GameObject.FindGameObjectsWithTag ("Speedy");
						foreach (GameObject red in reds) {
								Destroy (red);
				Movement.score +=30;
			}
						foreach (GameObject speedy in speedys){
								Destroy (speedy);
				Movement.score +=40;
			}
			Destroy(this.gameObject);
				}
	
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
		return location;
	}
}
