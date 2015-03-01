using UnityEngine;
using System.Collections;

public class PodiumScript : MonoBehaviour {

	Sprite[] podiums;
	public Sprite p0;
	public Sprite p1;
	public Sprite p2;
	public Sprite p3;

	int numPod;

	// Use this for initialization
	void Start () {
		podiums = new Sprite[4]{p0,p1,p2,p3};
		numPod = -1;

		GetComponent<SpriteRenderer> ().sprite = podiums [0];

	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag.Equals ("Player"))
						hitPodium ();
		}
	// Update is called once per frame
	void Update () {
	
	}

	void hitPodium(){
		numPod++;
		if (numPod < podiums.Length)
						GetComponent<SpriteRenderer> ().sprite = podiums [numPod];
				else
						Destroy (this.gameObject);
	}
}
