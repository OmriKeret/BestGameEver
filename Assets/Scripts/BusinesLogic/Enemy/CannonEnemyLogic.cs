using UnityEngine;
using System.Collections;

public class CannonEnemyLogic : BasicEnemyLogic {


    // Cannon params
    Vector3 playerPosition;
    bool isinitilized;
    private float timeToFinishJump = 5f;

    protected override void Awake()
    {
        base.Awake();
        playerPosition = GameObject.Find("PlayerManager").transform.position;

    }

    public void finishedCharge()
    {
        //TODO: set enemy path, dont forget u already got player position!
        if (!isinitilized)
        {
            isinitilized = true;
            Vector3 playerLocation = playerPosition;
            Vector3[] path = playerLocation.x > 0 ? CannonPaths.flyFromLeft : CannonPaths.flyFromRight;
            path[2].x = 2 * (path[0].x - playerLocation.x) / 4;
            path[2].y = playerLocation.y + 8f;
            path[1].x = 3
                * (path[3].x - playerLocation.x) / 4;
            path[1].y = playerLocation.y + 8f;

            LeanTween.move(this.gameObject, path, timeToFinishJump).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() =>
            {
                FinishedMoving();
            });
        }

    }

    private void RotateToDirection(Vector2 dir)
    {
        Vector2 dashAnimationVector;
        var x = dir.normalized.x;
        if (x > 0)
        {
            dashAnimationVector = new Vector2(1f, 0.5f);
        }
        else
        {
            dashAnimationVector = new Vector2(-1f, 0.5f);
        }
        //  Quaternion newRotation = Quaternion.LookRotation(dir);

        this.transform.rotation = Quaternion.FromToRotation(dashAnimationVector, dir);
    }

    public override void initVectorPaths()
    {
        _allVectorPaths = new StupidPaths();
    }

}
