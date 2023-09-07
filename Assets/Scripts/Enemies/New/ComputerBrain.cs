using Ai;
using UnityEngine;

public class ComputerBrain : Brain
{
    protected Context _context;
    protected BehaviourTree _behaviourTree;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        _behaviourTree.Process(Time.deltaTime, _context);
    }
}
