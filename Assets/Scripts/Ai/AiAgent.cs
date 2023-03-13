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
    public GameObject questionMark;

    public bool isDead;
    

    [HideInInspector] public AiSensor sensor;
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Awake()
    {
        stateMachine = new AiStateMachine(this);
        
        _animator = GetComponentInChildren<Animator>();
        stateMachine.RegisterState(new AiStatePatrol());
        stateMachine.RegisterState(new AiStateChasePlayer());
        stateMachine.RegisterState(new AiStateAttackPlayer());
        stateMachine.RegisterState(new AiStateAlarm());
        stateMachine.RegisterState(new AiStateCheckAlarm());
        stateMachine.RegisterState(new AiStateDead());
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
        isDead = false;

    }

    void Update()
    {
        if (!isDead && !Pause.isPaused)
        {
                stateMachine.Update();
            if(navMeshAgent.hasPath)
                _animator.SetFloat(Speed, navMeshAgent.velocity.magnitude);
            else
                _animator.SetFloat(Speed, 0);
            
        
            //Detect enemy : 
            if (sensor.Objects.Count > 0 && stateMachine.currentState != AiStateId.ChasePlayer && stateMachine.currentState != AiStateId.AttackPlayer)
            {
                questionMark.SetActive(true);
                detectionDuration = (detectionDuration - Time.deltaTime < 0) ? 0 : detectionDuration - Time.deltaTime;
                // If the enemy detect the player long enough he start chasing him
                if (detectionDuration <= 0)
                {
                    OnEnemyDetected();
                    transform.GetChild(0).GetComponent<MeshRenderer>().material = chaseMaterial;
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
                    questionMark.SetActive(false);
                    transform.GetChild(0).GetComponent<MeshRenderer>().material = patrolMaterial;
                }
            }
        }
        
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            isDead = true;
            stateMachine.ChangeState(AiStateId.Dead);
            _animator.SetTrigger("Death");
            EventManager.TriggerEvent("Gain Mana", new CustomEventData());
            Invoke("DestroyThis", 2);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

    protected virtual void OnEnemyDetected()
    {
        stateMachine.ChangeState(AiStateId.ChasePlayer);
    }

    protected virtual void OnLosingAggro()
    {
        if (!isDead) stateMachine.ChangeState(AiStateId.Patrol);
    }
}
