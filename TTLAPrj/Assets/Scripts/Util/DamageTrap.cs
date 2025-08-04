using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : Interact
{
    //private bool isPlayerEntered = false;
    protected override void Awake()
    {
        base.Awake();
        if (value <= 0f)
        {
            value = 2f;
        }
    }

    public override void OnEnterTrigger(Player player)
    {
        //InteractAnimation();
        GameManager.Instance.player.Damaged(value);
    }
    public override void OnExitTrigger(Player player)
    {
        throw new System.NotImplementedException();
    }
}
