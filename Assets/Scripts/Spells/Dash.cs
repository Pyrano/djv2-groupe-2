using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Spell
{
    Vector3 moveDirection;
 
    public float maxDashTime = 1.0f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    public float dashSpeed = 6;
    
    float currentDashTime;
    CharacterController controller;

    protected override void OnStart()
    {
        controller = GetComponent<CharacterController>();
        currentDashTime = maxDashTime;
    }

    protected override void OnCast()
    {
        currentDashTime = 0; 
    }

    protected override void OnUpdate()
    {
        if(currentDashTime < maxDashTime)
        {
            moveDirection = transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        controller.Move(moveDirection * (Time.deltaTime * dashSpeed));
    }
}
