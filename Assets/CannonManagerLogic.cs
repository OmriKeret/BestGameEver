using UnityEngine;
using System.Collections;

public class CannonManagerLogic : MonoBehaviour
{

    private bool timerOn = false;
    private const int waitBeforeShoot = (int)(2.5*500);
    private int currentTime = 0;
    private GameObject cannonPrefab;
    
    // Use this for initialization
	void Start () {
        timerOn = false;
        currentTime = 0;
	    cannonPrefab = Resources.Load<GameObject>("cannonEnemy");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (timerOn)
	    {
	        currentTime++;
	    }
	    if (currentTime>=waitBeforeShoot)
	    {
	        currentTime = 0;
	        timerOn = false;
	        bool playerOnRight = FindObjectOfType<PlayerStatsController>().gameObject.transform.position.x > 0;
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

    public void StartTime()
    {
        timerOn = true;
    }
}
