using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	public MovmentLogic PlayerMovmentLogic;
	public bool hover;
	// Use this for initialization
	void Start () {
		PlayerMovmentLogic = GameObject.Find("Logic").GetComponent<MovmentLogic>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hover(){
		MoveCharacterModel model = new MoveCharacterModel
		{ 
			Direction = new Vector2(),
			player = this.gameObject.GetComponent<Rigidbody2D>()
		};
		PlayerMovmentLogic.HoverCharacter (model);

	}

	public void StopHover(){
		MoveCharacterModel model = new MoveCharacterModel
		{ 
			Direction = new Vector2(),
			player = this.gameObject.GetComponent<Rigidbody2D>()
		};
		PlayerMovmentLogic.StopHoverCharacter (model);
		
	}
	
	public void Move(Vector2 dir){
		MoveCharacterModel model = new MoveCharacterModel
						{ 
						Direction = dir,
						player = this.gameObject.GetComponent<Rigidbody2D>()
						};
		PlayerMovmentLogic.MoveCharacter (model);
	}
}
