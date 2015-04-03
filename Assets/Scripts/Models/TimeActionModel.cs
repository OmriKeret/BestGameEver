using UnityEngine;
using System.Collections;
using System;
public class TimeActionModel {
	public Vector2 impactForce;
	public Rigidbody2D subject;
	public float fixedTimeStart;
	public float durationTime;
	public Action<StopAfterCollisionModel> stopingFunc;
	public Func<MoveAfterCollisionModel,MoveAfterCollisionModel> whileGoingDo;
}
