using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataObserver
{
    void OnChannelUpdated(string channel, object newValue);
}
