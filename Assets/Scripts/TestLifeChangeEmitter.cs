using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLifeChangeEmitter : MonoBehaviour
{
    CustomEventData data;
    private void Start()
    {
        data = new CustomEventData(10);
        EventManager.TriggerEvent("Test", data);
    }
}
