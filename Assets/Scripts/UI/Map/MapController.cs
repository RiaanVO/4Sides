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
    public List<Image> DifficultyNodes;
    public Transform DependenciesContainer;

    public Color DifficultyNodeActiveColor;
    public Color DifficultyNodeInactiveColor;
    public SectorDependencyItemController DependencyItem;

    private SceneNavigation nav;
    private LevelNode[] allNodes;
    private LevelNode selectedLevel = null;

    void Start()
    {
        nav = GetComponent<SceneNavigation>();

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
        if (string.IsNullOrEmpty(GameSession.LastCompletedSector))
        {
            var firstSector = GameSession.SECTOR_DEPENDENCIES.ElementAt(0).Key;
            LevelNode node = null;
            if (nodes.TryGetValue(firstSector, out node))
            {
                node.Unlock(null);
            }
        }
        else
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
        selectedLevel = selected;

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
        if (DifficultyNodes != null)
        {
            int nodeCount = Math.Min(DifficultyNodes.Count, selected.DifficultyRating);
            for (int i = 0; i < DifficultyNodes.Count; i++)
            {
                var node = DifficultyNodes[i];
                node.color = i < nodeCount ? DifficultyNodeActiveColor :
                    DifficultyNodeInactiveColor;
            }
        }
        if (DependenciesContainer != null)
        {
            // clear existing dependencies
            for (int i = 0; i < DependenciesContainer.childCount; i++)
            {
                var child = DependenciesContainer.GetChild(i);
                Destroy(child.gameObject);
            }

            if (!selected.IsUnlocked && DependencyItem != null)
            {
                // populate new dependencies
                string[] dependencies;
                if (GameSession.SECTOR_DEPENDENCIES.TryGetValue(selected.Name, out dependencies))
                {
                    for (int i = 0; i < dependencies.Length; i++)
                    {
                        // create dependency item
                        var dependency = dependencies[i];
                        var child = Instantiate(DependencyItem);
                        bool isCompleted = GameSession.LastCompletedSector == dependency ||
                            GameSession.CompletedSectors.Contains(dependency);
                        child.SetContent(dependency, isCompleted);

                        // position item
                        var rect = child.GetComponent<RectTransform>();
                        child.transform.SetParent(DependenciesContainer);
                        rect.pivot = new Vector2(0, 1);
                        rect.anchoredPosition = new Vector2(0, i * -32);
                        rect.anchorMax = Vector2.one;
                    }
                }
            }
        }
    }

    public void OnNodeDeselected()
    {
        selectedLevel = null;

        // update details pane
        if (DetailsPane != null)
        {
            DetailsPane.SetBool("IsLevelSelected", false);
        }
    }

    public void GoToSelectedLevel()
    {
        if (selectedLevel != null)
        {
            nav.GoToSector(selectedLevel.Name);
        }
    }
}
