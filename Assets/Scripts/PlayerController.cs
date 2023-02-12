using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera _camera;
    private Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _camera = Camera.main;
    }

    void Update()
    {
        if (agent.hasPath)
        {
            animator.SetFloat(Speed, agent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat(Speed, 0);
        }
        if (!Input.GetMouseButton(0))
            return;
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            agent.SetDestination(hit.point);
        }

    }
}
