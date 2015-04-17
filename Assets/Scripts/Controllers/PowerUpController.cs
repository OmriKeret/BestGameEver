using UnityEngine;
using System.Collections;

public class PowerUpController : MonoBehaviour {

    PowerUpLogic powerUpLogic;
    public float nextGenerateTime;
    public float fixedTimeStart = -10f;
    public int maxTimeBetweenPowerUps;

	// Use this for initialization
	void Start () {
        powerUpLogic = GameObject.Find("Logic").GetComponent<PowerUpLogic>();
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
        powerUpLogic.generatePowerUp();
        nextGenerateTime =(float) UnityEngine.Random.Range(2, maxTimeBetweenPowerUps);
    }
}
