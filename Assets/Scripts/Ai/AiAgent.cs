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

    [HideInInspector] public AiSensor sensor;
    void Awake()
    {
        stateMachine = new AiStateMachine(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine.RegisterState(new AiStatePatrol());
        stateMachine.ChangeState(initialState);
    }

    private void Start()
    {
        sensor = GetComponent<AiSensor>();
    }

    void Update()
    {
        stateMachine.Update();
    }
}
