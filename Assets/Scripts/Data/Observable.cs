using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observable : MonoBehaviour, IDataObserver
{
    public delegate void ChannelListener<T>(T newValue);
    public DataProvider Provider;

    private Dictionary<string, Action<object>> channelListeners =
        new Dictionary<string, Action<object>>();

    public void Bind<T>(string channel, T defaultValue, ChannelListener<T> listener)
    {
        T initialValue = defaultValue;
        if (Provider != null)
        {
            Provider.SubscribeToChannels(this, channel);
            var cachedValue = Provider.GetState(channel);
            if (cachedValue != null && cachedValue is T)
            {
                initialValue = (T)cachedValue;
            }
        }

        channelListeners[channel] = (object data) => listener((T)data);
        listener(initialValue);

        Render();
    }

    public void OnChannelUpdated(string channel, object newValue)
    {
        Action<object> listener;
        if (channelListeners.TryGetValue(channel, out listener))
        {
            listener(newValue);
            Render();
        }
    }

    public void OnEventTriggered(string eventName)
    {
    }

    protected abstract void Render();
}
