using UnityEngine;

using BehaviourTree;

public class TaskGather : Node
{
    private Transform m_LastTarget;
    private ItemManager m_ItemManager;
    private Animator m_Anim;

    private float gatherTime = 1f;
    private float gatherCounter = 0f;

    public TaskGather(Transform transform) 
    { 
        m_Anim = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != m_LastTarget)
        {
            m_ItemManager = target.GetComponent<ItemManager>();
            m_LastTarget = target;
        }

        gatherCounter += Time.deltaTime;
        if (gatherCounter >= gatherTime)
        {
            bool ItemDepleted = m_ItemManager.Take();
            if (ItemDepleted)
            {
                ClearData("target");
                m_Anim.SetBool("Gathering", false);
                m_Anim.SetBool("Walking", true);
            }
            else
            {
                gatherCounter = 0f;
            }
        }        

        state = NodeState.RUNNING;
        return state;
    }
}
