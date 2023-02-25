using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TaskPatrol : Node
{
    private Transform m_Transform;
    private Transform[] m_waypoints;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        m_Transform = transform;
        m_waypoints = waypoints;
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
