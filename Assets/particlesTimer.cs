using UnityEngine;
using System.Collections;

public class particlesTimer : MonoBehaviour {
    float timeToDie = 1f;
    float spawnTime;
    ParticleSystem ps;
	// Use this for initialization
	void Start () {
        spawnTime = Time.time;
        ps = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!ps.IsAlive())
        {
            Destroy(this.gameObject);
        }
    }
}
