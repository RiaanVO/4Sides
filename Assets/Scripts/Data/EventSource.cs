using System.Collections.Generic;
using UnityEngine;

public class EventSource : MonoBehaviour
{
    private class Subscription
    {
        public SubscriptionHandler action;
        public bool unsubscribeAfterInvocation;
    }
    public delegate void SubscriptionHandler(EventSource source, string eventName);

    private Dictionary<string, List<Subscription>> subscriptions =
        new Dictionary<string, List<Subscription>>();

    public void Notify(string eventName)
    {
        var handlers = new List<Subscription>();
        if (subscriptions.TryGetValue(eventName, out handlers))
        {
            for (int i = 0; i < handlers.Count; i++)
            {
                var handler = handlers[i];
                handler.action.Invoke(this, eventName);
                if (handler.unsubscribeAfterInvocation)
                {
                    handlers.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public void Subscribe(string eventName, SubscriptionHandler handler,
        bool unsubscribeAfterInvocation = false)
    {
        var sub = new Subscription
        {
            action = handler,
            unsubscribeAfterInvocation = unsubscribeAfterInvocation
        };

        var handlers = new List<Subscription>();
        if (subscriptions.TryGetValue(eventName, out handlers))
        {
            handlers.Add(sub);
        }
        else
        {
            subscriptions[eventName] = new List<Subscription>
            {
                sub
            };
        }
    }
}
