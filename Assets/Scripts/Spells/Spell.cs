using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField]
    protected float cooldown;

    public KeyCode key;
    public int manaCost;
    public int staminaCost;

    private float _cooldownTime;

    public GameObject castEffect;

    public PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        OnStart();
    }

    private void Update()
    {   
        if(!Pause.isPaused)
        {
            if (Input.GetKeyUp(key))
            {
                OnKeyUp();
                if (_cooldownTime <= 0 && manaCost <= controller.mana && staminaCost <= controller.stamina)
                {
                    _cooldownTime = cooldown;
                    controller.ChangeMana(-manaCost);
                    controller.ChangeStamina(-staminaCost);
                    OnCast();
                }
            }
            if (Input.GetKeyDown(key))
            {
                OnKeyDown();
            }
            if (Input.GetKey(key))
            {
                OnPreCast();
            }
            OnUpdate();

            _cooldownTime -= Time.deltaTime;
        }
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnCast()
    {
    }

    protected virtual void OnKeyDown()
    {
    }

    protected virtual void OnKeyUp()
    {
    }

    protected virtual void OnPreCast()
    {
    }

    protected virtual void OnUpdate()
    {
        
    }
}
