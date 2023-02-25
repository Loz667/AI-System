using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

public class UnitBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 5f;

    protected override Node SetupTree()
    {
        Node root = new TaskPatrol(transform, waypoints);
        return root;
    }
}
