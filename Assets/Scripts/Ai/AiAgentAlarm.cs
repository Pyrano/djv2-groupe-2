using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgentAlarm : AiAgent
{
   protected override void OnEnemyDetected()
   {
      stateMachine.ChangeState(AiStateId.Alarm);
   }

   protected override void OnLosingAggro()
   {
      if(!navMeshAgent.hasPath)
         stateMachine.ChangeState(AiStateId.Patrol);
   }
}
