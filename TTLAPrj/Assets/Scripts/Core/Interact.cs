using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interact : MonoBehaviour
{
    [SerializeField] protected float value = 0f;
    protected abstract void OnEnterTrigger(Player player);
    protected abstract void OnExitTrigger(Player player);
}
