using UnityEngine;

public abstract class AEnemyStats : MonoBehaviour {

    public int life;
    public int BASIC_HP_DOWN;
    public int BASIC_HP;
	public float MAX_SPEED;
	public Vector2 leftSplitLocation;
	
	public void lifeDown(){
		lifeDown (BASIC_HP_DOWN);
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
}
