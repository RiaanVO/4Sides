using System.Collections;
using System.Collections.Generic;
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
    private LevelState state = LevelState.Locked;

    void Awake()
    {
        if (NodeText != null)
        {
            NodeText.text = Name;
        }

        animator = GetComponent<Animator>();
        animator.SetInteger("State", (int)state);
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
                connector.Unlock(false);
            }
        }
        animator.SetInteger("State", (int)state);
    }

    private void OnCompletionFinished()
    {
        foreach (var connector in OutgoingConnectors)
        {
            connector.Unlock(true);
        }
    }

    public void Unlock()
    {
        state = LevelState.Unlocked;
        animator.SetInteger("State", (int)state);
    }
}
