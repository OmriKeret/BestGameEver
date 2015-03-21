using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenerateEnemies : MonoBehaviour {

	public int CREATION_FREQUENCY = 50;
	int counter, tutorialCounter;
	GameObject Enemy,Speedy,tutorialEnemy;
	GameObject[] Arrows;
	string[] tutorial;
	public Text instructions;

	// Use this for initialization
	void Start () {
		Enemy = (GameObject) Resources.Load ("Enemy");
		Speedy = (GameObject) Resources.Load ("Speedy");
		Arrows = new GameObject[]{(GameObject)Resources.Load ("Arrow1"),(GameObject)Resources.Load ("Arrow2"),
			(GameObject)Resources.Load ("Arrow3"),(GameObject)Resources.Load ("Arrow4"),(GameObject)Resources.Load ("ArrowD")};
		tutorialCounter = 0;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		counter++;
		if (GeneralPhysics.isTutorialMode) {
			switch (tutorialCounter){
			case 0 :tutorialEnemy = Instantiate(Enemy,new Vector3(4,3,0),Quaternion.identity) as GameObject;

				Destroy(Instantiate(Arrows[0]),1f);
				tutorialEnemy.GetComponent<Rigidbody2D>().isKinematic=true;
				tutorialCounter++;break;
			case 1: Time.timeScale = 1f;

				if (tutorialEnemy==null){
					tutorialCounter++;
					Destroy(Instantiate(Arrows[4],new Vector3(-5,-2,0),Quaternion.Euler(new Vector3(-1,1,0))),2);
					Destroy(Instantiate(Arrows[4],new Vector3(5,-2,0),Quaternion.identity),2);
				}
					break;
			case 2:tutorialEnemy = Instantiate(Enemy,new Vector3(-4,2,0),Quaternion.identity) as GameObject;
				Time.timeScale = 0.7f;
				Destroy(Instantiate(Arrows[1]),1f);
				tutorialEnemy.GetComponent<Rigidbody2D>().isKinematic=true;tutorialCounter++;break;
			case 3: if (tutorialEnemy==null)
				tutorialCounter++;break;
			case 4:tutorialEnemy = Instantiate(Enemy,new Vector3(5,3,0),Quaternion.identity) as GameObject;
				tutorialEnemy.GetComponent<Rigidbody2D>().isKinematic=true;
				Destroy(Instantiate(Arrows[2]),1f);
				Destroy(Instantiate(Arrows[3]),1f);
				tutorialCounter++;break;
			case 5: GeneralPhysics.isTutorialMode=false; 
				Time.timeScale=1;tutorialCounter++; break;
			}
				} else {
						if (counter % CREATION_FREQUENCY == 0) {
								if (Random.Range (0, 4) == 3)
										Instantiate (Speedy, GeneralPhysics.getRandomOuterbox (), Quaternion.identity);
								else
										Instantiate (Enemy, GeneralPhysics.getRandomOuterbox (), Quaternion.identity);
						}	
				}
	}
	
}
