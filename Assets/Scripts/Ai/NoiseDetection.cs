using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseDetection : MonoBehaviour
{
    private GameObject enemy;
    public AiSensor sensor;

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject;
        sensor = enemy.GetComponent<AiSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            if (player.isNoisy)
            {
                if (sensor.objects.Count == 0)
                {
                    sensor.objects.Add(other.gameObject);
                    Debug.Log("Add");
                }
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            Debug.Log("Exit");
            sensor.objects.Clear();
        }
    }
}
