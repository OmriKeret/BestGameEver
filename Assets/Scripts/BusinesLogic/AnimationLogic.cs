using UnityEngine;
using System.Collections;

public class AnimationLogic : MonoBehaviour
{
	public bool faceRight = true;
	public Rigidbody2D character ;

	void Start() {
		character = GameObject.Find("PlayerManager").GetComponent<Rigidbody2D>();
	}

	public void OnMoveSetDirection(moveAnimationModel model) 
	{
		if (model.direction.x > 0 && !faceRight)
			Flip ();
		else if (model.direction.x < 0 && faceRight)
			Flip ();
	}


	private void Flip()
	{
		faceRight = !faceRight;
		Vector3 theScale = character.transform.localScale;
		theScale.x *= -1;
		character.transform.localScale = theScale;
		
	}
}

