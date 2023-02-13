using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenUI : SpellUIManager
{
    protected override void OnEnable() 
    {
        EventManager.AddListener("SpellCast : Shuriken", OnCast);
    }

    protected override void OnDisable()
    {
        EventManager.RemoveListener("SpellCast : Shuriken", OnCast);
    }
}
