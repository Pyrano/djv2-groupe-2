using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationSpell : Spell
{
    [SerializeField] private float teleportRange;
    private CharacterController cc;

    protected override void OnStart()
    {
        cc = GetComponent<CharacterController>();
    }

    protected override void OnCast()
    {
        cc.enabled = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {   
            if (Vector3.Distance(transform.position, hit.point) <= teleportRange)
            {
                transform.position = hit.point;
            }
            else
            {
                transform.position += (hit.point - transform.position).normalized * teleportRange;
            }
        }

        cc.enabled = true;
    }
}
