using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, CustomEvent> _events;
    private static EventManager _eventManager;

    public static EventManager instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                else
                    _eventManager.Init();
            }

            return _eventManager;
        }
    }
    
    void Init()
    {
        if (_events == null)
        {
            _events = new Dictionary<string, CustomEvent>();
        }
    }

    public static void AddListener(string eventName, UnityAction<CustomEventData> listener)
    {
        CustomEvent evt = null;
        if (instance._events.TryGetValue(eventName, out evt))
        {
            evt.AddListener(listener);
        }
        else
        {
            evt = new CustomEvent();
            evt.AddListener(listener);
            instance._events.Add(eventName, evt);
        }
    }

    public static void RemoveListener(string eventName, UnityAction<CustomEventData> listener)
    {
        if (_eventManager == null) return;
        CustomEvent evt = null;
        if (instance._events.TryGetValue(eventName, out evt))
            evt.RemoveListener(listener);
    }

    public static void TriggerEvent(string eventName, CustomEventData data)
    {
        CustomEvent evt = null;
        if (instance._events.TryGetValue(eventName, out evt))
            evt.Invoke(data);        
    }
}

public class CustomEventData
{
    public int lifeChange;

    public CustomEventData(int lifeChange)
    {
        this.lifeChange = lifeChange;
    }
}

[System.Serializable]
public class CustomEvent : UnityEvent<CustomEventData> {}