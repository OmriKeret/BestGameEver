using UnityEngine;
using System.Collections;

public class HPanimationHelper : MonoBehaviour
{

    HPBarLogic HPBar;
    // Use this for initialization
    void Start()
    {
        HPBar = GameObject.Find("Logic").GetComponent<HPBarLogic>();
    }

    public void finishedAnimation()
    {
        HPBar.setShouldChange();
    }
}
