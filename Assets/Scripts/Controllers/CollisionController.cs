using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour {

	public CollisionFacade collisionFacade;
	// Use this for initialization
	void Start () {
	//	Debug.Log("Trigger: " + collider2D.is);
		// Use this for initializatio
		collisionFacade = new CollisionFacade ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void OnCollisionEnter2D(Collision2D col) {
	//	Debug.Log("collision detected");
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
