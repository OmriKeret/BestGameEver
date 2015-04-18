using UnityEngine;
using System.Collections;

public interface IEnemy {

	void Death();

	Vector2 MoveToPoint(Vector2 i_point);

	Vector2 FindPlayerLocation();

	void SetStats(AEnemyStats i_stats);

	void MoveInDirection(Vector2 i_direction);

	void Split(Vector2 i_location);

    bool lifeDown(int str);
    
    bool isDead();

    void StartRandomPath(int speed);
}
