using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSource : MonoBehaviour
{
    public delegate void Subscription(EventSource source, string eventName);

    private Dictionary<string, List<Subscription>> subscriptions =
        new Dictionary<string, List<Subscription>>();

    public void Notify(string eventName)
    {
        var handlers = new List<Subscription>();
        if (subscriptions.TryGetValue(eventName, out handlers))
        {
            handlers.ForEach(h => h.Invoke(this, eventName));
        }
    }

    public void Subscribe(string eventName, Subscription handler)
    {
        var handlers = new List<Subscription>();
        if (subscriptions.TryGetValue(eventName, out handlers))
        {
            handlers.Add(handler);
        }
        else
        {
            subscriptions[eventName] = new List<Subscription>
            {
                handler
            };
        }
    }

    public void Unsubscribe(string eventName, Subscription handler)
    {
        var handlers = new List<Subscription>();
        if (subscriptions.TryGetValue(eventName, out handlers))
        {
            handlers.Remove(handler);
        }
    }
}
