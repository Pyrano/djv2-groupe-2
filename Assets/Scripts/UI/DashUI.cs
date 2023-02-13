using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUI : SpellUIManager
{
    protected override void OnEnable() 
    {
        EventManager.AddListener("SpellCast : Dash", OnCast);
    }

    protected override void OnDisable()
    {
        EventManager.RemoveListener("SpellCast : Dash", OnCast);
    }
}

