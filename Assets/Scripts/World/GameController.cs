using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IDataObserver
{
    public DataProvider Player;

    void Start()
    {
        if (Player != null)
        {
            Player.SubscribeToEvents(this, BaseHealth.EVENT_DIED);
        }
    }

    public void OnChannelUpdated(string channel, object newValue)
    {
    }

    public void OnEventTriggered(string eventName)
    {
        SceneManager.LoadScene("DeathScene");
    }
}
