using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    public int teleportRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Teleport();
        }
    }

    void Teleport()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
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
    }
}
