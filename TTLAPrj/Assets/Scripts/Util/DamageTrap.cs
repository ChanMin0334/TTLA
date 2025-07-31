using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : Interact
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnEnterTrigger(Player player)
    {
        InteractAnimation();
        player.Stats.Hp -= value;
    }
    public override void OnExitTrigger(Player player)
    {
        throw new System.NotImplementedException();
    }
}
