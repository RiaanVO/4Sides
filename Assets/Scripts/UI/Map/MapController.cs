using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapController : MonoBehaviour
{
    void Start()
    {
        var nodes = (from node in GetComponentsInChildren<LevelNode>()
                     group node by node.Name into grp
                     select new
                     {
                         Name = grp.Key,
                         Node = grp.First()
                     }).ToDictionary(g => g.Name, g => g.Node);

        foreach (var sector in GameSession.CompletedSectors)
        {
            LevelNode node = null;
            if (nodes.TryGetValue(sector, out node))
            {
                node.Complete(false);
            }
        }
        if (!string.IsNullOrEmpty(GameSession.LastCompletedSector))
        {
            LevelNode node = null;
            if (nodes.TryGetValue(GameSession.LastCompletedSector, out node))
            {
                node.Complete(true);
            }
        }
    }
}
