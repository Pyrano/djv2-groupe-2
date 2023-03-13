using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateChasePlayer : AiState
{
    

    private GameObject player;
    public int lookAroundCooldown = 1;
    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        Debug.Log("chase player");

        if (agent.sensor.Objects.Count > 0)
            player = agent.sensor.Objects[0];
        else
            player = Game.Instance.player;

    }

    public void Update(AiAgent agent)
    {
        // TO DO  : Optimize this        
        if(agent.sensor.Objects.Count == 0)
        {
            agent.sensor.lookAround(agent);
            return;
        }
        agent.navMeshAgent.SetDestination(player.transform.position);
        if (Vector3.Distance(agent.sensor.Objects[0].transform.position, agent.transform.position) <= agent.config.attackRange)
        {
            agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
