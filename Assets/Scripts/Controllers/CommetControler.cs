using UnityEngine;
using System.Collections;

public class CommetControler : MonoBehaviour
{

    private CommetLogic logic;
    private bool defend;

	// Use this for initialization
	void Start ()
	{
	    logic = GetComponent<CommetLogic>();
	    defend = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (defend)
	    {
	        logic.defend();
	    }
	    else
	    {
	        logic.moveNormal();
	    }
	
	}
}
