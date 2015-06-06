using UnityEngine;
using System.Collections;

public class CoinTrigger : MonoBehaviour {

    public CollectableTypes type;
    public float timeToAppear = 1f;
    public float timeToDisapear;
    CollectableLogic collectableLogic;

    GameObject particles;
    // Use this for initialization
    void Awake()
    {
        collectableLogic = GameObject.Find("Logic").GetComponent<CollectableLogic>();
        particles = Resources.Load("Collectables/CoinParticles") as GameObject;
    }


    public void Set()
    {
        LeanTween.alpha(this.gameObject, 1f, timeToAppear).setOnComplete(() =>
        {
            LeanTween.alpha(this.gameObject, 0f, 1f).setDelay(timeToDisapear).setOnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }); ;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            collectableLogic.GotCollactable(type);
            LeanTween.cancel(this.gameObject);
            Instantiate(particles, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
