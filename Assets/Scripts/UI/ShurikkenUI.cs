using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikkenUI : SpellUIManager
{
    protected override void OnEnable() 
    {
        EventManager.AddListener("SpellCast : Shurikken", OnCast);
    }

    protected override void OnDisable()
    {
        EventManager.RemoveListener("SpellCast : Shurikken", OnCast);
    }
}
