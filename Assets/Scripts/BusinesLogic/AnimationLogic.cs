using UnityEngine;
using System.Collections;

public class AnimationLogic : MonoBehaviour
{
	public bool faceRight = true;
	public bool isDashing = false;
	public Rigidbody2D character ;
	public Animator animator;
	void Start() {
		character = GameObject.Find("PlayerManager").GetComponent<Rigidbody2D>();
		animator = GameObject.Find("PlayerManager").GetComponent<Animator>();
	}
	void Update() {
		animator.SetFloat("YVelocity");
	}
	public void OnMoveSetDirection(moveAnimationModel model) 
	{
		if (model.direction.x > 0 && !faceRight)
			Flip ();
		else if (model.direction.x < 0 && faceRight)
			Flip ();
	}

	public void SetDashing() 
	{
		animator.SetBool("s",);
		isDashing = true;
	}

	public void UnSetDashing()
	{
		isDashing = false;
	}

	private void Flip()
	{
		faceRight = !faceRight;
		Vector3 theScale = character.transform.localScale;
		theScale.x *= -1;
		character.transform.localScale = theScale;
		
	}
}

