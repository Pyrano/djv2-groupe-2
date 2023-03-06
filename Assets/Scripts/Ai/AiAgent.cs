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
    public int hp;
    public Material patrolMaterial;
    public Material chaseMaterial;
    public GameObject Player;
    

    [HideInInspector] public AiSensor sensor;
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Awake()
    {
        stateMachine = new AiStateMachine(this);
        
        _animator = GetComponent<Animator>();
        stateMachine.RegisterState(new AiStatePatrol());
        stateMachine.RegisterState(new AiStateChasePlayer());
        stateMachine.RegisterState(new AiStateAttackPlayer());
        stateMachine.RegisterState(new AiStateAlarm());
        stateMachine.ChangeState(initialState);
        hp = config.hp;
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
                OnEnemyDetected();
                transform.GetChild(4).GetComponent<MeshRenderer>().material = chaseMaterial;
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
                OnLosingAggro();
                transform.GetChild(4).GetComponent<MeshRenderer>().material = patrolMaterial;
            }
        }
        
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnEnemyDetected()
    {
        stateMachine.ChangeState(AiStateId.ChasePlayer);
    }

    protected virtual void OnLosingAggro()
    {
        Debug.Log("doing patrols");
        stateMachine.ChangeState(AiStateId.Patrol);
    }
}
