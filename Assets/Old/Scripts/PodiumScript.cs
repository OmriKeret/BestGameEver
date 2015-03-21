using UnityEngine;
using System.Collections;

public class PodiumScript : MonoBehaviour {

	Sprite[] podiums;
	public Sprite p0;
	public Sprite p1;
	public Sprite p2;
	public Sprite p3;
	private int counter;
	public int MAX_PODIUM_COUNTER = 30;
	private bool activateCounter;

	int numPod;

	// Use this for initialization
	void Start () {
		podiums = new Sprite[4]{p0,p1,p2,p3};
		numPod = -1;
		activateCounter = false;
		counter=0;
		GetComponent<SpriteRenderer> ().sprite = podiums [0];

	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag.Equals ("Player")&&!activateCounter) {
			activateCounter = true;
						hitPodium ();
				}
		}
	// Update is called once per frame
	void FixedUpdate () {
		if (activateCounter) {
			counter++;
			if (counter==MAX_PODIUM_COUNTER){
				activateCounter=false;
				counter = 0;
			}
				}
	}

	void hitPodium(){
		numPod++;
		if (numPod < podiums.Length)
						GetComponent<SpriteRenderer> ().sprite = podiums [numPod];
				else
						Destroy (this.gameObject);
	}
}
