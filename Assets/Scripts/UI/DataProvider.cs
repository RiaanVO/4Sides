using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    private Dictionary<string, List<IDataObserver>> channelSubs =
        new Dictionary<string, List<IDataObserver>>();
    private Dictionary<string, List<IDataObserver>> eventSubs =
        new Dictionary<string, List<IDataObserver>>();

    public void SubscribeToChannels(IDataObserver observer, params string[] channels)
    {
        if (observer != null && channels.Length > 0)
        {
            foreach (var channel in channels)
            {
                List<IDataObserver> observers = new List<IDataObserver>();
                if (channelSubs.TryGetValue(channel, out observers))
                {
                    observers.Add(observer);
                }
                else
                {
                    channelSubs[channel] = new List<IDataObserver>
                    {
                        observer
                    };
                }
            }
        }
    }

    public void SubscribeToEvents(IDataObserver observer, params string[] events)
    {
        if (observer != null && events.Length > 0)
        {
            foreach (var channel in events)
            {
                List<IDataObserver> observers = new List<IDataObserver>();
                if (eventSubs.TryGetValue(channel, out observers))
                {
                    observers.Add(observer);
                }
                else
                {
                    eventSubs[channel] = new List<IDataObserver>
                    {
                        observer
                    };
                }
            }
        }
    }

    public void UpdateChannel(string channel, object newValue)
    {
        List<IDataObserver> observers = new List<IDataObserver>();
        if (channelSubs.TryGetValue(channel, out observers))
        {
            observers.ForEach(o => o.OnChannelUpdated(channel, newValue));
        }
    }

    public void NotifyEvent(string eventName)
    {
        List<IDataObserver> observers = new List<IDataObserver>();
        if (eventSubs.TryGetValue(eventName, out observers))
        {
            observers.ForEach(o => o.OnEventTriggered(eventName));
        }
    }
}
