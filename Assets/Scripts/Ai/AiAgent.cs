using System;
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
    public AiAgentConfig config;

    [HideInInspector] public AiSensor sensor;
    void Awake()
    {
        stateMachine = new AiStateMachine(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine.RegisterState(new AiStatePatrol());
        stateMachine.RegisterState(new AiStateChasePlayer());
        stateMachine.ChangeState(initialState);
    }

    private void Start()
    {
        sensor = GetComponent<AiSensor>();
        navMeshAgent.speed = config.speed;
    }

    void Update()
    {
        stateMachine.Update();
        if (sensor.Objects.Count > 0 && stateMachine.currentState != AiStateId.ChasePlayer)
        {
            stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }
}
