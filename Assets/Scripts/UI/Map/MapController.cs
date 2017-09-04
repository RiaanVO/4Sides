using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private LevelNodeController[] nodes;

    void Start()
    {
        nodes = GetComponentsInChildren<LevelNodeController>();
        nodes[0].CompleteLevel();
        nodes[1].Unlock();
    }
}
