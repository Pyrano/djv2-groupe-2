using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestListener : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.AddListener("Test", _OnTest);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("Test", _OnTest);
    }
    
    private void _OnTest(object data)
    {
        CustomEventData dat = (CustomEventData) data;
        Debug.Log(dat.lifeChange);
    }
    
}
