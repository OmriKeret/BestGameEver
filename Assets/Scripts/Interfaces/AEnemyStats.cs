using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;

public abstract class AEnemyStats : MonoBehaviour {

    public int life;
    public int BASIC_HP_DOWN;
    public int BASIC_HP;
	public float MAX_SPEED;
	public Vector2 leftSplitLocation;
	public EnemyMode _mode;
	public EnemyType _type;
    //TODO: now it switches sprite, later change to animation
	public Dictionary <EnemyMode,Sprite> _AnimationState;
    public Sprite[] AllSprites;
    private float priorLocation;
    private bool movesToRight;
    private Vector3 prevLocation, currLocation;
    private float xVelocity;

	public void lifeDown(){
		lifeDown (BASIC_HP_DOWN);
	}

    void Start()
    {
        currLocation = gameObject.transform.position;
        
    }

    public void initFlip()
    {
        movesToRight = this.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0;
   
    }

    public Vector3 Direction
    {
        get { return currLocation - prevLocation; }
    }

    void FixedUpdate()
    {
        prevLocation = currLocation;
        currLocation = gameObject.transform.position;
        xVelocity = currLocation.x - prevLocation.x;
        if (movesToRight && xVelocity < 0 ||
            !movesToRight && xVelocity > 0)
        {
          
            movesToRight = !movesToRight;
            flip();
        }
        

    }

    private void flip()
    {
        Vector3 theScale = this.gameObject.transform.localScale;
        theScale.x *= -1;
        this.gameObject.transform.localScale = theScale;
    }

    public void lifeDown(int i_hp){
        life = life - i_hp;	
	}
	
	public bool isDead(){
        if (life > 0)
        {
            return false;
        }
		return true;
	}

    public void initAnimation()
    {
        _AnimationState = new Dictionary<EnemyMode, Sprite>()
        {
            {EnemyMode.None, AllSprites[0]},
            {EnemyMode.Attack, AllSprites[1]}
        };

    }

    public Sprite GetCurrentAnimation()
    {
        Sprite sprite = null;
        _AnimationState.TryGetValue(_mode, out sprite);
        return sprite;
    }
}
