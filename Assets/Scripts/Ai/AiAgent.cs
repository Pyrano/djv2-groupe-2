using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public Transform[] wayPoints;
    public AiStateMachine stateMachine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public AiStateId initialState;
    void Awake()
    {
        stateMachine = new AiStateMachine(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine.RegisterState(new AiStatePatrol());
        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();
    }
}
