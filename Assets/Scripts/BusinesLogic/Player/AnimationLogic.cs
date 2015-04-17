using UnityEngine;
using System.Collections;

public class AnimationLogic : MonoBehaviour
{
    public Transform groundCheck;
	public bool faceRight = true;
	public bool isDashing = false;
    public bool grounded = true;
    public bool isSlicing = false;
	public Rigidbody2D character ;
    public LayerMask whatIsGround;
	public Animator animator;
    public float groundRadius = 0.2f;
	void Start() {
		character = GameObject.Find("PlayerManager").GetComponent<Rigidbody2D>();
		animator = GameObject.Find("PlayerManager").GetComponent<Animator>();
		groundCheck = GameObject.Find("PlayerManager/GroundCheck").GetComponentInChildren<Transform>();
	}
	void Update() {

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
		isDashing = true;
        animator.SetBool("isDashing", isDashing);
	}
	public void UnSetDashing()
	{
		isDashing = false;
        animator.SetBool("isDashing", isDashing);
	}
    public void CheckIfGrounded()
    {
        grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
        animator.SetBool("Grounded",grounded);  
    }

    public void SetSlicing()
    {
        animator.SetTrigger("Slicing");
        isSlicing = true;
    }


	private void Flip()
	{
		faceRight = !faceRight;
		Vector3 theScale = character.transform.localScale;
		theScale.x *= -1;
		character.transform.localScale = theScale;
		
	}
}

