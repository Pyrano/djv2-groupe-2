using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporationUI : SpellUIManager
{
    protected override void OnEnable() 
    {
        EventManager.AddListener("SpellCast : Teleportation", OnCast);
    }

    protected override void OnDisable()
    {
        EventManager.RemoveListener("SpellCast : Teleportation", OnCast);
    }
}
