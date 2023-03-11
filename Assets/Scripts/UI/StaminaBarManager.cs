using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBarManager : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.AddListener("Player : ChangeStamina", OnStaminaChange);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("Player : ChangeStamina", OnStaminaChange);
    }

    BarController bar;
    private void Start()
    {
        bar = gameObject.GetComponent<BarController>();
    }

private void OnStaminaChange(object objData)
{
    CustomEventData data = (CustomEventData) objData;
    bar.SetRatio(data.ratio);
}

}
