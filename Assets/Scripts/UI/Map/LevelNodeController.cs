using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class LevelNodeController : MonoBehaviour
{
    public enum LevelState
    {
        Locked = 0,
        Unlocked = 1,
        Complete = 2
    }

    public Text NodeText;
    public string Name = "1-A";
    public LevelState State = LevelState.Locked;

    private Animator animator;

    void Awake()
    {
        if (NodeText != null)
        {
            NodeText.text = Name;
        }

        animator = GetComponent<Animator>();
        animator.SetInteger("State", (int)State);
    }

    public void Unlock()
    {
        State = LevelState.Unlocked;
        animator.SetInteger("State", (int)State);
    }

    public void CompleteLevel()
    {
        State = LevelState.Complete;
        animator.SetInteger("State", (int)State);
    }
}
