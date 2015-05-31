using UnityEngine;
using System.Collections;

public class ToturialPodiumController : MonoBehaviour {

    ToturialLogic toturialLogic;
    // Use this for initialization
    void Awake()
    {
        toturialLogic = GameObject.Find("ToturialManager").GetComponent<ToturialLogic>();
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            toturialLogic.playerIsOnThePodium();
        }
    }
}
