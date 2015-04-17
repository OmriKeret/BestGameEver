using UnityEngine;
using System.Collections;

public class PartOfEnemyFadeOut : MonoBehaviour {
    public float timeToFadeOut = 1.0f;
	public float amountOfForceOnX = 5f;
	public float amountOfForceOnY = 7f;
    public float timeBeforeFadeOut = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FadeAndDestoryUp()
    {
		Random rand = new Random ();
		var x = UnityEngine.Random.Range (2, amountOfForceOnX + 1);
		var y = UnityEngine.Random.Range (2, amountOfForceOnY + 1);
		this.GetComponent<Rigidbody2D> ().AddForce (new Vector2(x, y),ForceMode2D.Impulse);
        //iTween.FadeTo(this.gameObject, iTween.Hash(
        //    "alpha", 0,
        //   "time", timeToFadeOut,
        //   "oncomplete", "DestoryThis"
        //));
        LeanTween.alpha(this.gameObject, 0f, timeToFadeOut).setDelay(timeBeforeFadeOut).setOnComplete(() =>
        {
            DestoryThis();
        });
    }

    public void FadeAndDestoryDown()
    {
		var x = UnityEngine.Random.Range (2, amountOfForceOnX + 1);
		var y = UnityEngine.Random.Range (2, amountOfForceOnY + 1);
		this.GetComponent<Rigidbody2D> ().AddForce (new Vector2(-x, -y),ForceMode2D.Impulse);
        LeanTween.alpha(this.gameObject, 0f, timeToFadeOut).setDelay(timeBeforeFadeOut).setOnComplete(() =>
        {
            DestoryThis();
        });
    }

    public void DestoryThis()
    {
        Destroy(this.gameObject);
    }




    
}
