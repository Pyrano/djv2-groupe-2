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

    public int stamina;
    public int staminaMax;
    public float staminaRegenDelay;
    private float lastRegen;

    public int mana;
    public int manaMax;
    private Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Start()
    {
        hp = hpMax;
        mana = manaMax;
        stamina = staminaMax;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
        if (agent.hasPath)
        {
            animator.SetFloat(Speed, agent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat(Speed, 0);
        }
        
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            agent.SetDestination(hit.point);
        }
        }
        
        if (stamina < staminaMax && Time.time -lastRegen >= staminaRegenDelay)
            {
                ChangeStamina(1);
                lastRegen = Time.time;
            }
        
        if (Input.GetMouseButtonDown(2))
        {
            ChangeStamina(-10);
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

    public void ChangeStamina(int amount)
    {
        stamina += amount;
        if (stamina < 0)
        {
            hp = 0;
        }
        if (stamina > staminaMax)
        {
            stamina = staminaMax;
        }

        EventManager.TriggerEvent("Player : ChangeStamina", new CustomEventData((float) stamina/(float)staminaMax));
    }

    public void ChangeMana(int amount)
    {
        mana += amount;
        if (mana < 0)
        {
            mana = 0;
        }
        if (mana > manaMax)
        {
            mana = manaMax;
        }

        EventManager.TriggerEvent("Player : ChangeMana", new CustomEventData((float) mana/(float)manaMax));
    }
}
