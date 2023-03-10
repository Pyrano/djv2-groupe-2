using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public bool isNoisy;
    public bool isRunning;
    private float lastRClick;
    private NavMeshAgent agent;
    private Camera _camera;
    private bool isJumping;

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
        animator = GetComponentInChildren<Animator>();
        _camera = Camera.main;
        lastRClick = Time.time;
        isRunning = false;
    }

    void Update()
    {
        if (!Pause.isPaused && hp > 0)
        {
        agent.SamplePathPosition(UnityEngine.AI.NavMesh.GetAreaFromName("Jump"), 1, out var h);
        if ( h.mask ==  4)
        {
            Debug.Log(h.ToString());
            animator.SetTrigger("Jump");
            isJumping = true;
        }
        else
        {
            animator.ResetTrigger("Jump");
            isJumping = false;
        }

        if (agent.hasPath)
        {
            animator.SetFloat(Speed, agent.velocity.magnitude);

        }
        else
        {
            animator.SetFloat(Speed, 0);
        }
        if (Input.GetMouseButton(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                agent.SetDestination(hit.point);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastRClick <= 0.25)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            lastRClick = Time.time;
        }
        
        if (stamina < staminaMax && Time.time -lastRegen >= staminaRegenDelay)
            {
                ChangeStamina(1);
                lastRegen = Time.time;
            }
        

        UpdateNoise();
        if (isJumping)
        {
            agent.speed =  1.5f;
        }
        else if (isRunning)
        {
            agent.speed = 5.0f;
        }

        else
        {
            agent.speed = 3.5f;
        }
        }
    }

    public void ChangeLife(int amount)
    {
        hp += amount;
        if (hp < 0)
        {
            hp = 0;
            animator.SetTrigger("Death");
        }
        if (hp > hpMax)
        {
            hp = hpMax;
        }

        EventManager.TriggerEvent("Player : ChangeLife", new CustomEventData((float) hp/(float)hpMax));
    }

    private void UpdateNoise()
    {
        if (isRunning)
        {
            isNoisy = true;
        }

        else
        {
            isNoisy = false;
        }
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

    void OnEnable()
    {
        EventManager.AddListener("Player damage", TakeDamage);
        EventManager.AddListener("SpellCast : Shuriken", ShurikenCast);
        EventManager.AddListener("Gain Mana", ManaOnDeath);
    }

    private void ManaOnDeath(object obj) 
    {
        ChangeMana(25);
    }

    private void ShurikenCast(object data)
    {

        animator.SetTrigger("Attack");
    }

    private void TakeDamage(object data)
    {
        CustomEventData damage = (CustomEventData)data;
        ChangeLife(-damage.lifeChange);
    }
}
