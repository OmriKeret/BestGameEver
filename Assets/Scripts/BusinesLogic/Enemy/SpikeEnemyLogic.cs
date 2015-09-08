using UnityEngine;
using System.Collections;

public class SpikeEnemyLogic : BasicEnemyLogic {

    private int stateCounter = 0;
    public int normalTime = 90;
    public int spikeTime = 150;

    protected override void Awake()
    {
        base.Awake();
        _leftBodyPartResouce = Resources.Load("spikeL") as GameObject;
        _rightBodyPartResouce = Resources.Load("spikeR") as GameObject;
        
        
    }

    public override void initVectorPaths()
    {
        _allVectorPaths = new StupidPaths();
    }

    protected virtual void Start()
    {
        base.Start();
    }

    public override EnemyMode GetEnemyMode()
    {
        return _stats._mode;
    }

    // Animation driven
    public void spikesOut()
    {
        _stats._mode = EnemyMode.Attack;
    }

    public void spikesIn()
    {
        _stats._mode = EnemyMode.None;
    }
}
