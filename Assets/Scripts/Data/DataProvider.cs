using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    private Dictionary<string, List<IDataObserver>> subscriptions =
        new Dictionary<string, List<IDataObserver>>();
    private Dictionary<string, object> currentState =
        new Dictionary<string, object>();

    public void SubscribeToChannels(IDataObserver observer, params string[] channels)
    {
        if (observer != null && channels.Length > 0)
        {
            foreach (var channel in channels)
            {
                List<IDataObserver> observers = new List<IDataObserver>();
                if (subscriptions.TryGetValue(channel, out observers))
                {
                    observers.Add(observer);
                }
                else
                {
                    subscriptions[channel] = new List<IDataObserver>
                    {
                        observer
                    };
                }
            }
        }
    }

    public void UpdateChannel(string channel, object newValue)
    {
        currentState[channel] = newValue;

        List<IDataObserver> observers = new List<IDataObserver>();
        if (subscriptions.TryGetValue(channel, out observers))
        {
            observers.ForEach(o => o.OnChannelUpdated(channel, newValue));
        }
    }

    public object GetState(string channel)
    {
        object value = null;
        currentState.TryGetValue(channel, out value);
        return value;
    }
}
