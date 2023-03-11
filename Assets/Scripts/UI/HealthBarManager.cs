using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.AddListener("Player : ChangeLife", OnLifeChange);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("Player : ChangeLife", OnLifeChange);
    }

    BarController bar;
    private void Start()
    {
        bar = gameObject.GetComponent<BarController>();
    }

private void OnLifeChange(object objData)
{

    CustomEventData data = (CustomEventData) objData;
    bar.SetRatio(data.ratio);
}

}
