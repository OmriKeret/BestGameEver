using UnityEngine;
using System.Collections;
using System;
public class TimeActionModel {
	public Vector2 impactForce;
	public GameObject subject;
	public GameObject collidedWith;
	public float fixedTimeStart;
	public float durationTime;
	public Action<SuperHitModel> stopingFunc;
	public Func<MoveAfterCollisionModel,MoveAfterCollisionModel> whileGoingDo;
}
