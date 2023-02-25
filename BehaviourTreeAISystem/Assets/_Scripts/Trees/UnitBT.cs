using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

public class UnitBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float fovRange = 3f;
    public static float gatherRange = 0.75f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckItemInGatherRange(transform),
                new TaskGather(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckItemInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }
}
