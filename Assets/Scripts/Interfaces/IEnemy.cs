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

    void StartOrderPath(int i_speed, int i_WaveNumber);

    float calculateTime(float speed);

    Vector3[][] initPaths();

    void FinishedMoving();

    void selectRandomPath(out Vector3[] i_path);

    void selectOrderPath(out Vector3[] i_path, int i_WaveNumber);

    EnemyMode GetEnemyMode();

    void playSpawnSound();

    void playDeathSound();

    void goRight();
}
