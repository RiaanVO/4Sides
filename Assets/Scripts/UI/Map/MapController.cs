using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public Animator DetailsPane;
    public Text SectorNameText;

    private LevelNode[] allNodes;

    void Start()
    {
        allNodes = GetComponentsInChildren<LevelNode>();
        var nodes = (from node in allNodes
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

    public void OnNodeSelected(LevelNode selected)
    {
        // deselect all other nodes
        foreach (var node in allNodes)
        {
            if (node != selected)
            {
                node.Deselect();
            }
        }

        // update details pane
        if (DetailsPane != null)
        {
            DetailsPane.SetBool("IsLevelSelected", true);
            DetailsPane.SetBool("IsLevelUnlocked", selected.IsUnlocked);
        }
        if (SectorNameText != null)
        {
            SectorNameText.text = selected.Name;
        }
    }

    public void OnNodeDeselected()
    {
        // update details pane
        if (DetailsPane != null)
        {
            DetailsPane.SetBool("IsLevelSelected", false);
        }
    }
}
