using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAttackPlayer : AiState
{
    private float timeToAttack;
    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }

    public void Enter(AiAgent agent)
    {
        timeToAttack = agent.config.attackSpeed;
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
                    player.hp -=(int)agent.config.attackDamage;
                    EventManager.TriggerEvent("Player damage", new CustomEventData((int)agent.config.attackDamage));
                    Debug.Log("attacking");
                }
            }
            timeToAttack = agent.config.attackSpeed;
        }
    }

    public void Exit(AiAgent agent)
    {
        timeToAttack = agent.config.attackSpeed;
    }
}
