using UnityEngine;
using System.Collections;

public class PodiumController : MonoBehaviour {
    PodiumLogic podiumLogic;
    private CannonManagerLogic cannonManager;

    // Cannon parametets
    public float timeOnPodiumBeforeCannonArrives = 2.5f;
    private float timeLandedOnPodium = 1000000000; // big value so it wont initiate on begining
    private bool shouldCheckTime;

	// Use this for initialization
	void Awake () {
        podiumLogic = GetComponent<PodiumLogic>();
        podiumLogic.initPodium(this.gameObject);
	}

    void Start()
    {
        shouldCheckTime = false;// cannonManager = new CannonManagerLogic();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - timeLandedOnPodium > timeOnPodiumBeforeCannonArrives && shouldCheckTime)
        {
            shouldCheckTime = false;
            podiumLogic.playerIsCamping();
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {

            timeLandedOnPodium = Time.time;
           // cannonManager.StartTimer();
            shouldCheckTime = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            timeLandedOnPodium = 1000000000; // big value so it wont initiate
            podiumLogic.playerLandedOnPlatform();
            shouldCheckTime = false;
            //cannonManager.StopTimer();
        }
    }
}
