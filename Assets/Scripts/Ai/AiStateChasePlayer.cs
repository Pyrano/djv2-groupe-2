using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateChasePlayer : AiState
{
    private GameObject player;
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
    }

    public void Exit(AiAgent agent)
    {
    }
}
