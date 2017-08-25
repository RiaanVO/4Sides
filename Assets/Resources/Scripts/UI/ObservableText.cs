using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ObservableText : Observable
{
    public string TextChannel;
    public string DefaultText = string.Empty;

    private Text text;
    private string value;

    void Start()
    {
        text = GetComponent<Text>();

        Bind<string>(TextChannel, DefaultText, newValue => value = newValue);
    }

    protected override void Render()
    {
        text.text = value;
    }
}