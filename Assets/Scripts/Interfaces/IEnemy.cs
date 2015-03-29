using UnityEngine;
using System.Collections;

public interface IEnemy {

	void death();

	Vector2 moveToPoint(Vector2 i_point);

	Vector2 findPlayerLocation();



}
