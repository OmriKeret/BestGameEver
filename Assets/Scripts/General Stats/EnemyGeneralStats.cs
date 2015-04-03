using UnityEngine;
using System.Collections;

public class EnemyGeneralStats : MonoBehaviour {

	
	protected int life;
	protected const int BASIC_HP_DOWN = 1;
	protected const int BASIC_HP = 1;
	public float MAX_SPEED = 10f;
	public Vector2 leftSplitLocation;

	void Start () {
				leftSplitLocation = new Vector2 (10, 0);
		}


	public EnemyGeneralStats(){
		life = BASIC_HP;
	}

	public void lifeDown(){
		lifeDown (BASIC_HP_DOWN);
	}

	public void lifeDown(int i_hp){
		life -= i_hp;

	}

	public bool isDead(){
				if (life > 0)
						return false;
				return true;
		}
}
