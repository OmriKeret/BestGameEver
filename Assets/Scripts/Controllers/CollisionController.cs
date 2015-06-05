using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour {

	public CollisionFacade collisionFacade;
    private float intervalBetweenCollisionWithPodium = 0.3f;
    private float lastCollisionWithPodium;
	// Use this for initialization
	void Start () {
	//	Debug.Log("Trigger: " + collider2D.is);
		// Use this for initializatio
		collisionFacade = new CollisionFacade ();
	}
	
    void FixedUpdate()
    {

    }
	public void OnCollisionEnter2D(Collision2D col) {
	//	Debug.Log("collision detected");
        if(col.collider.tag.Equals("Wall")) 
        {
            if (Time.fixedTime - lastCollisionWithPodium < intervalBetweenCollisionWithPodium)
            {
                //return;
            }
            lastCollisionWithPodium = Time.fixedTime;
        }
       
		CollisionModel model = new CollisionModel{ mainCollider = this.gameObject, CollidedWith = col.gameObject};
		onCollisionFacade (model);
	}

    public void OnCollisionEnter2DManual(GameObject col)
    {
        //	Debug.Log("collision detected");
        CollisionModel model = new CollisionModel { mainCollider = this.gameObject, CollidedWith = col.gameObject };
        onCollisionFacade(model);
    }

	public void onCollisionFacade(CollisionModel model) {
		collisionFacade.Collision (model);
	}
}
