using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAlarm : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Alarm;
    }

    public void Enter(AiAgent agent)
    {
        Vector3 alarm = Game.Instance.GetClosestAlarm(agent.transform.position);
        agent.navMeshAgent.SetDestination(alarm);
        Game.Instance.triggeredAlarm = alarm;
    }

    public void Update(AiAgent agent)
    {
        // The enemy reach the alarm
        if (!agent.navMeshAgent.hasPath)
        {
            // TO DO : Play an animation
            // Make all enemies chase player, except the one triggering the alarm
            foreach (var enemy in Game.Instance.enemies)
            {
                if (enemy != agent)
                {
                    enemy.sensor.objects.Add(Game.Instance.player);
                    enemy.stateMachine.ChangeState(AiStateId.CheckAlarm);
                }
            }
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
