using UnityEngine;
using System.Collections;

public class CoinTrigger : MonoBehaviour {

    public CollectableTypes type;
    public float timeToAppear = 1f;
    public float timeToDisapear;
    CollectableLogic collectableLogic;
    // Use this for initialization
    void Awake()
    {
        collectableLogic = GameObject.Find("Logic").GetComponent<CollectableLogic>();
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
            collectableLogic.GotCollactable(type);
            LeanTween.cancel(this.gameObject);
            //TODO: create particals
            Destroy(this.gameObject);
        }
    }
}
