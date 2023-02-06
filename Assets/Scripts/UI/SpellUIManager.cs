using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUIManager : MonoBehaviour
{
    protected virtual void OnEnable()
    {}

    protected virtual void OnDisable()
    {}

    protected void OnCast(object data)
    {
        transform.GetComponent<SpellUIController>().OnSpellCast(data);
    }

}
