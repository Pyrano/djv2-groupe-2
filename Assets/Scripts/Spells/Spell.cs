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

    private float _cooldownTime;

    public GameObject castEffect;

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (_cooldownTime <= 0)
            {
                _cooldownTime = cooldown;
                OnCast();
            }
        }
        OnUpdate();

        _cooldownTime -= Time.deltaTime;
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnCast()
    {
    }

    protected virtual void OnUpdate()
    {
        
    }
}
