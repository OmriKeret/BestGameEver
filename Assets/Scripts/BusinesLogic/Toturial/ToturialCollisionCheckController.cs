using UnityEngine;
using System.Collections;

public class ToturialCollisionCheckController : MonoBehaviour {

    ToturialLogic toturialLogic;
    // Use this for initialization
    void Awake()
    {
        toturialLogic = GameObject.Find("ToturialManager").GetComponent<ToturialLogic>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collision with player and touch");
        if (col.gameObject.tag.Equals("Player"))
        {
            toturialLogic.playerCollidedWithTouchChecker();
        }
    }
}
