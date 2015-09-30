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

    protected override void Start()
    {
        base.Start();
        commet.Orign = transform.position;
    }

    public override void initVectorPaths()
    {
        _allVectorPaths = new StupidPaths();
    }

    public void KillCommet()
    {
        commet.Destroy();
    }

    public  void kaki()
    {
        base.Death();
    }

}
