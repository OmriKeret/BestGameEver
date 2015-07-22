using UnityEngine;
using System.Collections;

public class CameraShakeLogic : MonoBehaviour {

    private static CameraShakeLogic I;

    public float ShakeSpeed = 50f;
    public Vector3 ShakeRange = new Vector3(1, 1, 1);
    private float ShakerTimer = 0f;
    public float ShakerTime = 0.15f;

    private bool Shake = false;

    private Vector3 OriginalPosition;

    public static CameraShakeLogic GetInstance
    {
        get
        {
            return I;
        }
    }

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    //test
        if (Input.GetKeyDown(KeyCode.K))
        {
            CameraShake();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Shake = false;
        }

        if (Shake)
        {
            if (ShakerTimer > ShakerTime * Time.timeScale)
            {
                ShakerTimer = 0;
                Shake = false;
                Time.timeScale = 1;

                Camera.main.transform.position = new Vector3(OriginalPosition.x, OriginalPosition.y, OriginalPosition.z);
                ShakeSpeed *= -1;
                ShakeRange = new Vector3(ShakeRange.x, ShakeRange.y);
            }

            else
            {
                ShakerTimer += Time.deltaTime;
                Camera.main.transform.position = OriginalPosition + Vector3.Scale((Vector2)SmoothRandom.GetVector3(ShakeSpeed--), ShakeRange);

                ShakeSpeed *= -1;
                ShakeRange = new Vector3(ShakeRange.x * -1, ShakeRange.y*-1);
            }
        }
	}

    public void CameraShake()
    {
        OriginalPosition = Camera.main.transform.position;
        ShakeSpeed = 50;
        Time.timeScale = 0.5f;
        Shake = true;
    }
}
