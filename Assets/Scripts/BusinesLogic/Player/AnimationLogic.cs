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

    // Trail.
    private TrailRenderer trail;
    private float trailTime = 0.5f;
    private float coolDownTime = 0.5f;
    private float startCoolDownTime = 0f;
    private bool turnDownTrail = false;

    public LayerMask whatIsGround;
	public Animator animator;
    private float groundRadius = 1f;
    bool fliping;
	void Start() {
        character = GameObject.Find("PlayerManager");
        trail = character.GetComponent<TrailRenderer>();

        animator = character.GetComponent<Animator>();
		groundCheck = GameObject.Find("PlayerManager/GroundCheck").GetComponentInChildren<Transform>();

        // Unset trail
        trail.time = 0.0f;
	}
	void Update() {
        CheckIfGrounded();
        if (turnDownTrail)
        {
            if (Time.time - startCoolDownTime > coolDownTime)
            {
                // Unset trail
                trail.time = 0.0f;
                turnDownTrail = false;
            }
        }
	}
    void LateUpdate()
    {
        //if (fliping)
        //{
        //    Flip();
        //    fliping = false;
        //}
    }
	public void OnMoveSetDirection(moveAnimationModel model) 
	{
        //Debug.Log("moving direction is positive: " + (model.direction.x > 0));
		if (model.direction.x > 0 && !faceRight)
            Flip();
		else if (model.direction.x < 0 && faceRight)
            Flip();
	}
    

	public void SetDashing() 
	{
        // Set trail
        trail.time = trailTime;
        turnDownTrail = false;

		isDashing = true;
        grounded = false;
        animator.SetBool("Grounded", grounded);
        animator.SetBool("isDashing", isDashing);
	}
	public void UnSetDashing()
	{
        // Unset trail
        turnDownTrail = true;
        startCoolDownTime = Time.time;

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
    public void playerDie()
    {
        animator.SetTrigger("Die");
    }
    internal void playerHit()
    {
        //LeanTween.color(character.gameObject, Color.white, 0);
        //LeanTween.color(character.gameObject, Color.black, hitChangeColorTIme).setLoopPingPong(2);             
    }
}

