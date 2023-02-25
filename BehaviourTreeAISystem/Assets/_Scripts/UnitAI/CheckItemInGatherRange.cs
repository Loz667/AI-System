using UnityEngine;

using BehaviourTree;

public class CheckItemInGatherRange : Node
{
    private Transform m_Transform;
    private Animator m_Anim;

    public CheckItemInGatherRange(Transform transform)
    {
        m_Transform = transform;
        m_Anim = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(m_Transform.position, target.position) <= UnitBT.gatherRange)
        {
            m_Anim.SetBool("Gathering", true);
            m_Anim.SetBool("Walking", false);

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
