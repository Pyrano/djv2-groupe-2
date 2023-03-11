using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AiStateDead : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Dead;
    }

    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
    }

    public void Update(AiAgent agent) 
    {
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
