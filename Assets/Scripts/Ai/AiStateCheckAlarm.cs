using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateCheckAlarm : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.CheckAlarm;
    }

    public void Enter(AiAgent agent)
    {
        Vector3 centerPoint = Game.Instance.triggeredAlarm;
        float radius = 2f;
        Vector3 randomPosition = GetRandomPositionAroundPoint(centerPoint, radius);
        agent.navMeshAgent.SetDestination(randomPosition);
    }
    
    public Vector3 GetRandomPositionAroundPoint(Vector3 center, float radius)
    {
        Vector3 randomPos = center + Random.insideUnitSphere * radius;
        return randomPos;
    }

    public void Update(AiAgent agent)
    {
        if(!agent.navMeshAgent.hasPath)
            agent.stateMachine.ChangeState(AiStateId.Patrol);
    }

    public void Exit(AiAgent agent)
    {
    }
}
