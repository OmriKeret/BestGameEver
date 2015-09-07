using UnityEngine;
using System.Collections;

public class TankEnemyLogic : BasicEnemyLogic {

    private CommetLogic commet;
    private bool switchSides = true;

    protected override void Awake()
    {
        base.Awake();
        _leftBodyPartResouce = Resources.Load("tankL") as GameObject;
        _rightBodyPartResouce = Resources.Load("tankR") as GameObject;
        commet = GetComponentInChildren<CommetLogic>();
        switchSides = true;

    }

    public override void initVectorPaths()
    {
        _allVectorPaths = new StupidPaths();
    }

    public override void Death()
    {
        commet.Destroy();
        base.Death();
    }

    protected virtual void Start()
    {
        base.Start();
    }
}
