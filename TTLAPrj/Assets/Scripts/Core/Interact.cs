using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interact : MonoBehaviour
{
    [SerializeField] protected float value = 0f;

    private static readonly int IsInteract = Animator.StringToHash("IsInteract");
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void InteractAnimation()
    {
        animator.SetTrigger(IsInteract);
    }

    public abstract void OnEnterTrigger(Player player);
    public abstract void OnExitTrigger(Player player);
}
