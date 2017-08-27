using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSource : MonoBehaviour
{
    private Dictionary<string, List<Action>> subscriptions = new Dictionary<string, List<Action>>();

    public void Notify(string eventName)
    {
        var handlers = new List<Action>();
        if (subscriptions.TryGetValue(eventName, out handlers))
        {
            handlers.ForEach(h => h.Invoke());
        }
    }

    public void Subscribe(string eventName, Action eventHandler)
    {
        var handlers = new List<Action>();
        if (subscriptions.TryGetValue(eventName, out handlers))
        {
            handlers.Add(eventHandler);
        }
        else
        {
            subscriptions[eventName] = new List<Action>
            {
                eventHandler
            };
        }
    }
}
