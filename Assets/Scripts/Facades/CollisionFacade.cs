using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CollisionFacade {
	public CollisionLogic collisionLogic;
	public Dictionary< PairModel<string,string>,Action<CollisionModel>>  collisionDictionary;

	public CollisionFacade() {
		collisionLogic = GameObject.Find("Logic").GetComponent<CollisionLogic>();
		buildDictionray ();
	}

	private void buildDictionray(){
		collisionDictionary = new Dictionary<PairModel<string,string>,Action<CollisionModel>> 	
		{ 
			{ PairModel.New<string,string>("Enemy","Player") , collisionLogic.playerCollideWithEnemy },
			{ PairModel.New<string,string>("Enemy","Enemy") , collisionLogic.playerCollideWithEnemy },
			{ PairModel.New<string,string>("Enemy","Wall") , collisionLogic.playerCollideWithEnemy },
			{ PairModel.New<string,string>("Player","Wall") , collisionLogic.playerCollideWithEnemy },
			{ PairModel.New<string,string>("Player","Enemy") , collisionLogic.playerCollideWithEnemy },
		};


	}

	public void Collision(CollisionModel model) {
		var pair = PairModel.New<string,string> (model.mainCollider.tag, model.CollidedWith.tag);
		collisionDictionary[pair].Invoke(model);
	}

}
