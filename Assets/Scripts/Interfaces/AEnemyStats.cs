using UnityEngine;
using System.Collections.Generic;

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
    private float scale;
    private bool flip = false,toRight;

	public void lifeDown(){
		lifeDown (BASIC_HP_DOWN);
	}

    void Start()
    {
        scale = transform.localScale.x;
        flip = false;
        toRight = false;
    }

    void Update()
    {
        
        

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
