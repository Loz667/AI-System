using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TaskPatrol : Node
{
    private Transform m_Transform;
    private Transform[] m_waypoints;

    private Animator m_Anim;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        m_Transform = transform;
        m_waypoints = waypoints;
        m_Anim = transform.GetComponent<Animator>();
    }

    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;

    private bool Waiting = false;

    public override NodeState Evaluate()
    {
        if (Waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime) 
            { 
                Waiting = false;
                m_Anim.SetBool("Walking", true);
            }
        }
        else
        {
            Transform point = m_waypoints[currentWaypointIndex];
            if (Vector3.Distance(m_Transform.position, point.position) < 0.01f)
            {
                m_Transform.position = point.position;
                waitCounter = 0f;
                Waiting = true;
                m_Anim.SetBool("Walking", false);

                currentWaypointIndex = (currentWaypointIndex + 1) % m_waypoints.Length;
            }
            else
            {
                m_Transform.position = Vector3.MoveTowards(m_Transform.position, point.position, UnitBT.speed * Time.deltaTime);
                m_Transform.LookAt(point.position);
            }
        }
        
        state = NodeState.RUNNING;
        return state;
    }
}
