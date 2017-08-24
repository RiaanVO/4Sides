using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ProgressBar : MonoBehaviour, IDataObserver
{
    public DataProvider Provider;
    public string ValueChannel;
    public string MaxValueChannel;
    public float DefaultValue = 0.5f;
    public float DefaultMaxValue = 1.0f;

    private Image image;
    private float maxValue;
    private float value;

    void Start()
    {
        image = GetComponent<Image>();

        value = DefaultValue;
        maxValue = DefaultMaxValue;
        Render();

        if (Provider != null)
        {
            Provider.SubscribeToChannels(this, ValueChannel, MaxValueChannel);
        }
    }

    public void OnChannelUpdated(string channel, object newValue)
    {
        if (channel == MaxValueChannel && newValue is float)
        {
            maxValue = (float)newValue;
            Render();
        }
        else if (channel == ValueChannel && newValue is float)
        {
            value = (float)newValue;
            Render();
        }
    }

    public void OnEventTriggered(string eventName)
    {
    }

    private void Render()
    {
        image.fillAmount = value / maxValue;
    }
}
