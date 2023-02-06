using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBarManager : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.AddListener("Player : ChangeMana", OnManaChange);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("Player : ChangeMana", OnManaChange);
    }

    BarController bar;
    private void Start()
    {
        bar = gameObject.GetComponent<BarController>();
    }

private void OnManaChange(object objData)
{
    Debug.Log("Mana !");
    CustomEventData data = (CustomEventData) objData;
    bar.SetRatio(data.ratio);
}

}
