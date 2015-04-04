using UnityEngine;
using System.Collections;

public interface IEnemy {

	void death();

	Vector2 moveToPoint(Vector2 i_point);

	Vector2 findPlayerLocation();

	void setStats(AEnemyStats i_stats);

	void moveInDirection(Vector2 i_direction);

	void split(Vector2 i_location);

}
