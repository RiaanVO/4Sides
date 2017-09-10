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

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("State", (int)state);
    }

    public void Unlock(bool justUnlocked)
    {
        state = justUnlocked ? ConnectorState.JustUnlocked : ConnectorState.PreviouslyUnlocked;
        animator.SetInteger("State", (int)state);
    }

    private void OnUnlockFinished()
    {
        if (DestinationNode != null)
        {
            DestinationNode.Unlock();
        }
    }
}

