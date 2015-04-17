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
			{ PairModel.New<string,string>("Enemy","Player") , collisionLogic.EnemyCollidedWithPlayer },
			{ PairModel.New<string,string>("Enemy","Enemy") , doNothing },
            { PairModel.New<string,string>("Enemy","PowerUp") , doNothing },
			{ PairModel.New<string,string>("Enemy","Wall") , doNothing},
            { PairModel.New<string,string>("Player","PowerUp") , collisionLogic.playerCollideWithPowerUp},
			{ PairModel.New<string,string>("Player","Wall") , collisionLogic.playerCollidedWithWall },
			{ PairModel.New<string,string>("Player","Enemy") , collisionLogic.playerCollideWithEnemy },
		};


	}

	public void doNothing(CollisionModel model) {
		return;
	}
	public void Collision(CollisionModel model) {
		var pair = PairModel.New<string,string> (model.mainCollider.tag, model.CollidedWith.tag);
		if (collisionDictionary.ContainsKey (pair)) {
			collisionDictionary [pair].Invoke (model);
		}
	}
}
