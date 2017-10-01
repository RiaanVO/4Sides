using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectorDependencyItemController : MonoBehaviour
{
    public Texture IncompleteCheckbox;
    public Texture CompleteCheckbox;
    public Color IncompleteColor;
    public Color CompleteColor;

    public void SetContent(string sectorName, bool isComplete)
    {
        Text text = GetComponentInChildren<Text>();
        RawImage checkbox = GetComponentInChildren<RawImage>();

        if (text != null)
        {
            text.text = string.Format("Sector " + sectorName);
            text.color = isComplete ? CompleteColor : IncompleteColor;
        }
        if (checkbox != null)
        {
            checkbox.texture = isComplete ? CompleteCheckbox : IncompleteCheckbox;
        }
    }
}
