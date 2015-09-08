using UnityEngine;
using System.Collections;

public class StupidEnemyLogic : BasicEnemyLogic {

    protected override void Awake()
    {
        base.Awake();
        _leftBodyPartResouce = Resources.Load("stupidL") as GameObject;
        _rightBodyPartResouce = Resources.Load("stupidR") as GameObject;
    }

    public override void initVectorPaths()
    {
        _allVectorPaths = new StupidPaths();
    }

    protected virtual void Start()
    {
        base.Start();
    }
}
