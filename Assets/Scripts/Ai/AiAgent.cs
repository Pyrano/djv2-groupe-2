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
    private float detectTime;

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
        detectTime = config.detectionSpeed;
    }

    void Update()
    {
        stateMachine.Update();
        // TO DO : add time to detect
        if (sensor.Objects.Count > 0 && stateMachine.currentState != AiStateId.ChasePlayer)
        {
            detectTime -= Time.deltaTime;
            stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }
}
