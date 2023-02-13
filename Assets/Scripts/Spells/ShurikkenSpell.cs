using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ShurikkenSpell : Spell
{
    public GameObject shurikken;

    protected override void OnStart()
    {

    }

    protected override void OnCast()
    {
        CooldownInfo info = new CooldownInfo(Time.time);
        EventManager.TriggerEvent("SpellCast : Shurikken", new CustomEventData(info));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {   
            Vector3 direction = new Vector3(hit.point.x - transform.position.x, 0, hit.point.z - transform.position.z).normalized;
            Instantiate(shurikken, transform.position + new Vector3(direction.x, 0.8f, direction.z) * 2f , Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up));
        }
    }
}
