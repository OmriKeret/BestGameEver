using UnityEngine;
using System.Collections;

public class PowerUpTrigger : MonoBehaviour {

    public PowerUpType type;
    public float timeToAppear = 1f;
    public float timeToDisapear;
    PowerUpLogic powerUpLogic;
	// Use this for initialization
	void Awake () {
        powerUpLogic = GameObject.Find("Logic").GetComponent<PowerUpLogic>();
	}
	

    public void Set()
    {
        LeanTween.alpha(this.gameObject, 1f, timeToAppear).setOnComplete(() =>
        {
            LeanTween.alpha(this.gameObject, 0f, 1f).setDelay(timeToDisapear).setOnComplete(() =>
            {
                LeanTween.cancel(this.gameObject);
                Destroy(this);
            });
        }); ;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            powerUpLogic.GotPowerUp(type);
            LeanTween.cancel(this.gameObject);
            Destroy(this.gameObject);
        }
    }

}
