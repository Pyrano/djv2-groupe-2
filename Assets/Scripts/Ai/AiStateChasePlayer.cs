using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateChasePlayer : AiState
{
    private GameObject player;
    private float aggroTimeLeft;
    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        player = agent.sensor.Objects[0];
    }

    public void Update(AiAgent agent)
    {
        // TO DO  : Optimize this
        agent.navMeshAgent.SetDestination(player.transform.position);
        if (agent.sensor.Objects.Count == 0)
        {
            
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
