using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera _camera;

    public int hp;
    public int hpMax;
    void Start()
    {
        hp = hpMax;
        agent = GetComponent<NavMeshAgent>();
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            agent.SetDestination(hit.point);
        }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            ChangeLife(-10);
        }

    }

    public void ChangeLife(int amount)
    {
        hp += amount;
        if (hp < 0)
        {
            hp = 0;
        }
        if (hp > hpMax)
        {
            hp = hpMax;
        }

        EventManager.TriggerEvent("Player : ChangeLife", new CustomEventData((float) hp/(float)hpMax));
    }
}
