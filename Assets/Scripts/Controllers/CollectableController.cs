using UnityEngine;
using System.Collections;

public class CollectableController : MonoBehaviour {

    CollectableLogic collactableLogic;
    public float nextGenerateTime;
    public float fixedTimeStart = 10f;
    public float minBetweenPowerUps = 5f;
    public int maxTimeBetweenPowerUps;

    // Use this for initialization
    void Start()
    {
        collactableLogic = GameObject.Find("Logic").GetComponent<CollectableLogic>();
        nextGenerateTime = Time.fixedTime;
    }

    void FixedUpdate()
    {
        if (Time.fixedTime - fixedTimeStart > nextGenerateTime)
        {
            GeneratePowerUp();
            fixedTimeStart = Time.fixedTime;
        }
    }
    void GeneratePowerUp()
    {
        collactableLogic.generatCoin();
        nextGenerateTime = (float)UnityEngine.Random.Range(minBetweenPowerUps, maxTimeBetweenPowerUps);
    }
}
