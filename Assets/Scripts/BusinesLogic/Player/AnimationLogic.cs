using UnityEngine;
using System.Collections;

public class AnimationLogic : MonoBehaviour
{
    public Transform groundCheck;
	public bool faceRight = true;
	public bool isDashing = false;
    public bool grounded = true;
    public bool isSlicing = false;
    public float hitChangeColorTIme = 0.3f;
	public GameObject character ;
	public GameObject playerContainer;
    public LayerMask whatIsGround;
	public Animator animator;
    private float groundRadius = 0.5f;
	void Start() {
		playerContainer = GameObject.Find("PlayerContainer");
		character = GameObject.Find("PlayerManager");
        animator = character.GetComponent<Animator>();
		groundCheck = GameObject.Find("PlayerManager/GroundCheck").GetComponentInChildren<Transform>();
	}
	void Update() {
        CheckIfGrounded();
	}
	public void OnMoveSetDirection(moveAnimationModel model) 
	{
        Debug.Log("moving direction is positive: " + (model.direction.x > 0));
		if (model.direction.x > 0 && !faceRight)
			Flip ();
		else if (model.direction.x < 0 && faceRight)
			Flip ();
	}
    

	public void SetDashing() 
	{
		isDashing = true;
        grounded = false;
        animator.SetBool("Grounded", grounded);
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
        Debug.Log("changing local scale");
		Vector3 theScale = playerContainer.transform.localScale;
		theScale.x *= -1;
		playerContainer.transform.localScale = theScale;	
	}

    internal void playerHit()
    {
        LeanTween.color(character.gameObject, Color.red, hitChangeColorTIme).setLoopPingPong(2);             
    }
}

