using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAttackPlayer : AiState
{
    private float timeToAttack;
    private static readonly int Attack = Animator.StringToHash("Attack");

    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }

    public void Enter(AiAgent agent)
    {
        timeToAttack = 0;
        //agent._animator.SetBool(IsAttacking, true);
    }

    public void Update(AiAgent agent)
    {
        timeToAttack -= Time.deltaTime;
        
        if (agent.sensor.Objects.Count == 0|| Vector3.Distance(agent.sensor.Objects[0].transform.position, agent.transform.position) > agent.config.attackRange)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
            return;
        }
        if (timeToAttack <= 0)
        {
            Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position
                , 30);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent<PlayerController>(out var player))
                {
                    // UI
                    EventManager.TriggerEvent("Player damage", new CustomEventData((int)agent.config.attackDamage));
                    agent._animator.SetTrigger(Attack);
                    
                }
            }
            timeToAttack = agent.config.attackSpeed;
        }
    }

    public void Exit(AiAgent agent)
    {
        //agent._animator.SetBool(IsAttacking, false);
    }
}
