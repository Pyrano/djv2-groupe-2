using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AiStatePatrol : AiState
{
    private Transform[] _waypoints;
    private int _nextwaypoint;
    public bool onAlert = false;
    private float _lookAroundCooldown = 100;

    public AiStateId GetId()
    {
        return AiStateId.Patrol;
    }

    public void Enter(AiAgent agent)
    {
        _waypoints = agent.wayPoints;
        Debug.Log("waypoitns " +_waypoints.Length);
    }

    public void Update(AiAgent agent)
    {
        if (onAlert)
        {   
            _lookAroundCooldown -= Time.deltaTime;
            if (_lookAroundCooldown <= 0)
            {
                agent.sensor.lookAround(agent);
                _lookAroundCooldown = 100;
            }
        }
        if (agent.navMeshAgent.hasPath) return;
        agent.navMeshAgent.SetDestination(_waypoints[_nextwaypoint].position);
        _nextwaypoint = _nextwaypoint == (_waypoints.Length - 1) ? 0 : _nextwaypoint + 1;
    }

    public void Exit(AiAgent agent)
    {
    }
}
