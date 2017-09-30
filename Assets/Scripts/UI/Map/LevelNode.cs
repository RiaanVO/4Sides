using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class LevelNode : MonoBehaviour
{
    public enum LevelState
    {
        Locked = 0,
        Unlocked = 1,
        JustCompleted = 2,
        PreviouslyCompleted = 3
    }

    public Text NodeText;
    public string Name = "1-A";
    public List<NodeConnector> OutgoingConnectors;

    private Animator animator;
    private MapController map;
    private LevelState state = LevelState.Locked;
    private List<string> remainingDependencies;
    private bool isSelected;

    public bool IsUnlocked
    {
        get { return state != LevelState.Locked; }
    }

    void OnValidate()
    {
        if (NodeText != null)
        {
            NodeText.text = Name;
        }
    }

    void Awake()
    {
        if (NodeText != null)
        {
            NodeText.text = Name;
        }

        animator = GetComponent<Animator>();
        animator.SetInteger("State", (int)state);
        isSelected = false;

        string[] dependencies;
        if (GameSession.SECTOR_DEPENDENCIES.TryGetValue(Name, out dependencies))
        {
            remainingDependencies = dependencies.ToList();
        }
        else
        {
            remainingDependencies.Clear();
        }
    }

    void Start()
    {
        map = GameObject.FindObjectOfType<MapController>();
    }

    public void Complete(bool justCompleted)
    {
        if (justCompleted)
        {
            state = LevelState.JustCompleted;
        }
        else
        {
            state = LevelState.PreviouslyCompleted;
            foreach (var connector in OutgoingConnectors)
            {
                connector.Unlock(false, Name);
            }
        }
        animator.SetInteger("State", (int)state);
    }

    private void OnCompletionFinished()
    {
        foreach (var connector in OutgoingConnectors)
        {
            connector.Unlock(true, Name);
        }
    }

    public void Unlock(string sourceLevel)
    {
        remainingDependencies.Remove(sourceLevel);
        if (remainingDependencies.Count > 0) return;

        state = LevelState.Unlocked;
        animator.SetInteger("State", (int)state);
    }

    public void ToggleSelectionState()
    {
        isSelected = !isSelected;
        animator.SetBool("IsSelected", isSelected);

        if (isSelected)
        {
            map.OnNodeSelected(this);
        }
        else
        {
            map.OnNodeDeselected();
        }
    }

    public void Deselect()
    {
        isSelected = false;
        animator.SetBool("IsSelected", isSelected);
    }
}
