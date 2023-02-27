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
        Debug.Log("chase player");
        if(agent.sensor.Objects.Count > 0)
            player = agent.sensor.Objects[0];
    }

    public void Update(AiAgent agent)
    {
        // TO DO  : Optimize this
        agent.navMeshAgent.SetDestination(player.transform.position);
        if(agent.sensor.Objects.Count == 0)
            return;
        if (Vector3.Distance(agent.sensor.Objects[0].transform.position, agent.transform.position) <= agent.config.attackRange)
        {
            agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
