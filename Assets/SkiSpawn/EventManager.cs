using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private Dictionary<string, Action<GameObject, int>> eventDictionary;
    private static EventManager eventManager;
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    private void Init()
    {
        if(eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<GameObject,int>>();
        }
    }

    public static void StartListening(string eventName, Action<GameObject,int> listener)
    {

        Action<GameObject, int> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
        }
        else
        {
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<GameObject,int> listener)
    {
        if (eventManager == null) return;
        Action<GameObject,int> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
        }
    }

    public static void TriggerEvent(string eventName, GameObject ob, int amount)
    {
        Action<GameObject, int> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(ob, amount);
        }
        else
        {
            Debug.LogWarning(string.Format("Don't have any event with this name: {0}", eventName));
        }
    }


}
