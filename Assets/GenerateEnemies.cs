using UnityEngine;
using System.Collections;

public class GenerateEnemies : MonoBehaviour {

	public int CREATION_FREQUENCY = 50;
	int counter;
	GameObject Enemy,Speedy;

	// Use this for initialization
	void Start () {
		Enemy = (GameObject) Resources.Load ("Enemy");
		Speedy = (GameObject) Resources.Load ("Speedy");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		counter++;
		if (counter % CREATION_FREQUENCY == 0) {
			if (Random.Range(0,4)==3)
				Instantiate(Speedy,GeneralPhysics.getRandomOuterbox(),Quaternion.identity);
			else
				Instantiate(Enemy,GeneralPhysics.getRandomOuterbox(),Quaternion.identity);
				}
	
	}
}
