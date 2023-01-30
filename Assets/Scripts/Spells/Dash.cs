using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dash : Spell
{
    Vector3 _moveDirection;
 
    public float maxDashTime = 1.0f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    public float dashSpeed = 6;
    private NavMeshAgent _agent;
    
    float _currentDashTime;

    protected override void OnStart()
    {
        _currentDashTime = maxDashTime;
        _agent = GetComponent<NavMeshAgent>();
    }

    protected override void OnCast()
    {
        _currentDashTime = 0; 
    }

    protected override void OnUpdate()
    {
        if (!(_currentDashTime < maxDashTime)) return;
        _moveDirection = transform.forward * dashDistance;
        _currentDashTime += dashStoppingSpeed;
        _agent.Move(_moveDirection * (Time.deltaTime * dashSpeed));
        _agent.SetDestination(_agent.nextPosition + _moveDirection * (Time.deltaTime * dashSpeed));
    }
}
