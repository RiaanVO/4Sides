using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private LevelNode[] nodes;

    void Start()
    {
        nodes = GetComponentsInChildren<LevelNode>();
        nodes[0].Complete(true);
    }
}
