using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckItemInFOVRange : Node
{
    private Transform m_transform;
    private Animator m_Anim;

    private static int itemLayerMask = 1 << 6;

    public CheckItemInFOVRange(Transform transform)
    {
        m_transform = transform;
        m_Anim = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                m_transform.position, UnitBT.fovRange, itemLayerMask);

            if (colliders.Length > 0 )
            {
                parent.parent.SetData("target", colliders[0].transform);
                m_Anim.SetBool("Walking", true);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
