using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TeleportationSpell : Spell
{
    [SerializeField] private float teleportRange;

    private NavMeshAgent _agent;

    protected override void OnStart()
    {
        _agent = GetComponent<NavMeshAgent>();

    }

    protected override void OnCast()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {   
            if (Vector3.Distance(transform.position, hit.point) <= teleportRange)
            {

                _agent.SetDestination(hit.point);
                _agent.nextPosition = hit.point;
            }
            else
            {
                var nextPosition = _agent.nextPosition;
                nextPosition += (hit.point - transform.position).normalized * teleportRange;
                _agent.nextPosition = nextPosition;
                _agent.SetDestination(nextPosition);
            }
        }


    }
}
