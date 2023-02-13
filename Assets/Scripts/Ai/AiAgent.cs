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
    private float detectionDuration;
    private float aggroDuration;
    public Animator _animator;
    

    [HideInInspector] public AiSensor sensor;
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Awake()
    {
        stateMachine = new AiStateMachine(this);
        
        _animator = GetComponent<Animator>();
        stateMachine.RegisterState(new AiStatePatrol());
        stateMachine.RegisterState(new AiStateChasePlayer());
        stateMachine.RegisterState(new AiStateAttackPlayer());
        stateMachine.ChangeState(initialState);
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        sensor = GetComponent<AiSensor>();
        navMeshAgent.speed = config.speed;
        detectionDuration = config.detectionSpeed;
        aggroDuration = config.aggroTime;

    }

    void Update()
    {
        stateMachine.Update();
        if(navMeshAgent.hasPath)
            _animator.SetFloat(Speed, navMeshAgent.velocity.magnitude);
        else
            _animator.SetFloat(Speed, 0);
        
       
        //Detect enemy : 
        if (sensor.Objects.Count > 0 && stateMachine.currentState != AiStateId.ChasePlayer && stateMachine.currentState != AiStateId.AttackPlayer)
        {
            detectionDuration = (detectionDuration - Time.deltaTime < 0) ? 0 : detectionDuration - Time.deltaTime;
            // If the enemy detect the player long enough he start chasing him
            if (detectionDuration <= 0)
            {
                stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }
        // If no player is detected
        else
        {
            detectionDuration = (detectionDuration + Time.deltaTime > config.detectionSpeed) ? config.detectionSpeed : detectionDuration + Time.deltaTime;
        }
        
        // Managing enemy aggro
        if (stateMachine.currentState != AiStateId.Patrol)
        {
            if (sensor.Objects.Count > 0)
            {
                aggroDuration = (aggroDuration + Time.deltaTime > config.aggroTime) ? 
                    config.aggroTime : aggroDuration + Time.deltaTime;
            }
            else
            {
                aggroDuration = (aggroDuration - Time.deltaTime < 0) ? 0 : aggroDuration - Time.deltaTime;
            }

            if (aggroDuration <= 0)
            {
                stateMachine.ChangeState(AiStateId.Patrol);
            }
        }
        
    }
}
