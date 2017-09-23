using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NodeConnector : MonoBehaviour
{
    public enum ConnectorState
    {
        Locked = 0,
        JustUnlocked = 1,
        PreviouslyUnlocked = 2
    }

    public LevelNode DestinationNode;

    private Animator animator;
    private ConnectorState state = ConnectorState.Locked;
    private string sourceLevel;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("State", (int)state);
    }

    public void Unlock(bool justUnlocked, string sourceLevel)
    {
        this.sourceLevel = sourceLevel;

        if (justUnlocked)
        {
            state = ConnectorState.JustUnlocked;
        }
        else
        {
            state = ConnectorState.PreviouslyUnlocked;
            if (DestinationNode != null)
            {
                DestinationNode.Unlock(sourceLevel);
            }
        }
        animator.SetInteger("State", (int)state);
    }

    private void OnUnlockFinished()
    {
        if (DestinationNode != null)
        {
            DestinationNode.Unlock(sourceLevel);
        }
    }
}

