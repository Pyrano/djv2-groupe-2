using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if(isPaused)
        {
            PauseGame();
        }
        else
        {
            UnPauseGame();
        }
    }

    void PauseGame() 
    {
        Time.timeScale = 0;
        EventManager.TriggerEvent("Pause", new CustomEventData());
    }

    void UnPauseGame() 
    {
        Time.timeScale = 1;
        EventManager.TriggerEvent("UnPause", new CustomEventData());
    }
}
