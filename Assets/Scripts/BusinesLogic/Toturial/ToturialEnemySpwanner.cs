using UnityEngine;
using System.Collections;

public class ToturialEnemySpwanner : MonoBehaviour {

    GameObject stupid;
    public LeanTweenPath ltPath;
    public LeanTweenPath wave2Path1;
    public LeanTweenPath wave2Path2;
    public float timeToGetToPoint = 2.5f;
    ToturialLogic toturialLogic;
    
    void Awake()
    {
        stupid = Resources.Load("toturialEnemy") as GameObject;
        toturialLogic = this.GetComponent<ToturialLogic>();

    }

    public void spawnEnemie()
    {
        Vector3 enemyLocation = new Vector3(23f, -9f, 0f);
        GameObject currentEnemy = Instantiate(stupid, enemyLocation, Quaternion.identity) as GameObject;
        BasicEnemyLogic currentEnemyLogic = currentEnemy.GetComponent<BasicEnemyLogic>();
        try
        {
            currentEnemyLogic.playSpawnSound();
        }
        catch (System.Exception e)
        {
            Debug.Log("Failed playing sound!");
        }
        
        LeanTween.move(currentEnemy, ltPath.vec3, timeToGetToPoint).setOnComplete(
            () => {
                toturialLogic.EnemyGotToPosition();
            });
    }

    internal void spawnEnemiesForWaveTwo()
    {
        Vector3 enemyLocation = new Vector3(0f, -25f, 0f);
        GameObject currentEnemy = Instantiate(stupid, enemyLocation, Quaternion.identity) as GameObject;
        BasicEnemyLogic currentEnemyLogic = currentEnemy.GetComponent<BasicEnemyLogic>();
        currentEnemyLogic.playSpawnSound();
        LeanTween.move(currentEnemy, wave2Path1.vec3, timeToGetToPoint);

        currentEnemy = Instantiate(stupid, enemyLocation, Quaternion.identity) as GameObject;
        currentEnemyLogic = currentEnemy.GetComponent<BasicEnemyLogic>();
        currentEnemyLogic.playSpawnSound();
        LeanTween.move(currentEnemy, wave2Path2.vec3, timeToGetToPoint).setOnComplete(() =>
        {
            toturialLogic.EnemyGotToPosition();
        });
    }
}
