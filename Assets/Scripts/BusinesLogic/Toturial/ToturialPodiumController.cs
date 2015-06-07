using UnityEngine;
using System.Collections;

public class ToturialPodiumController : MonoBehaviour {

    PodiumLogic podiumLogic;

    ToturialLogic toturialLogic;
    // Use this for initialization
    void Awake()
    {
        podiumLogic = GetComponent<PodiumLogic>();
        toturialLogic = GameObject.Find("ToturialManager").GetComponent<ToturialLogic>();
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            toturialLogic.playerIsOnThePodium();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            podiumLogic.playerLandedOnPlatform();
        }
    }

}
