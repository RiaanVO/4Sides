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
        if (Provider != null)
        {
            Provider.SubscribeToChannels(this, channel);
        }

        channelListeners[channel] = (object data) => listener((T)data);
        listener(defaultValue);

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
