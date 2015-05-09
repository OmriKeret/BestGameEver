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
 
    public LayerMask whatIsGround;
	public Animator animator;
    private float groundRadius = 0.5f;
    bool fliping;
	void Start() {
		character = GameObject.Find("PlayerManager");
		animator = GameObject.Find("PlayerManager").GetComponent<Animator>();
		groundCheck = GameObject.Find("PlayerManager/GroundCheck").GetComponentInChildren<Transform>();
	}
	void Update() {
        CheckIfGrounded();
	}
    void LateUpdate()
    {
        if (fliping)
        {
            Debug.Log("late update");
            Flip();
            fliping = false;
        }
    }
	public void OnMoveSetDirection(moveAnimationModel model) 
	{
        Debug.Log("moving direction is positive: " + (model.direction.x > 0));
		if (model.direction.x > 0 && !faceRight)
            Flip();
		else if (model.direction.x < 0 && faceRight)
            Flip();
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
		Vector3 theScale = character.transform.localScale;
		theScale.x *= -1;
		character.transform.localScale = theScale;	
	}

    internal void playerHit()
    {
        //LeanTween.color(character.gameObject, Color.white, 0);
        //LeanTween.color(character.gameObject, Color.black, hitChangeColorTIme).setLoopPingPong(2);             
    }
}

