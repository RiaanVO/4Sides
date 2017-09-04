using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MapMarker : MonoBehaviour
{
    public Vector2 Offset = new Vector2(60, 32);
    public float AnimationDelay = 3.0f;

    RectTransform rect;
    Vector2 destination;
    float animateTimestamp;
    bool isMoving;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        isMoving = false;
    }

    void Update()
    {
        if (isMoving && Time.time - animateTimestamp > AnimationDelay)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, destination, 0.1f);
            if (Vector2.Distance(rect.anchoredPosition, destination) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    public void SetNode(LevelNode node)
    {
        var nodeRect = node.GetComponent<RectTransform>();
        rect.anchoredPosition = nodeRect.anchoredPosition + Offset;

        if (node.OutgoingConnectors.Count > 0)
        {
            var destinationNode = node.OutgoingConnectors[0].DestinationNode;
            if (destinationNode != null)
            {
                var destNodeRect = destinationNode.GetComponent<RectTransform>();
                destination = destNodeRect.anchoredPosition + Offset;

                animateTimestamp = Time.time;
                isMoving = true;
            }
        }
    }
}
