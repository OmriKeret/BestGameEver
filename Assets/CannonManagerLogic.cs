using UnityEngine;
using System.Collections;

public class CannonManagerLogic : MonoBehaviour
{

    private static bool timerOn = false;
    private const int waitBeforeShoot = (int)(2.5*2);
    private int currentTime = 0;
    private GameObject cannonPrefab;
    private bool oldTimer;
    
    
    // Use this for initialization
	void Start () {
        timerOn = false;
        currentTime = 0;
	    cannonPrefab = Resources.Load<GameObject>("cannonEnemy");
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (oldTimer ^ timerOn)
	    {
	        Debug.Log("Switch to "+timerOn);
	    }
	    oldTimer = timerOn;
        if (timerOn)
	    {
	        Debug.Log(string.Format("{0}/{1}",currentTime,waitBeforeShoot));
            currentTime++;
	    }
	    
	    if (currentTime>=waitBeforeShoot)
	    {
	        currentTime = 0;
	        timerOn = false;
	        bool playerOnRight = GameObject.Find("PlayerManager").transform.position.x > 0;
	        if (playerOnRight)
	        {
	            Instantiate(cannonPrefab,new Vector3(-25,-5,0),Quaternion.identity);
	        }
	        else
	        {
                Instantiate(cannonPrefab, new Vector3(25, -5, 0), Quaternion.identity);
	        }
	    }
	
	}

    //public void StartTimer()
    //{
    //    timerOn = true;
    //    Debug.Log("Timer "+timerOn);
        
    //}

    //public void StopTimer()
    //{
    //    if (timerOn)
    //    {
    //        timerOn = false;
    //        currentTime = 0;
    //    }
    //}
}
