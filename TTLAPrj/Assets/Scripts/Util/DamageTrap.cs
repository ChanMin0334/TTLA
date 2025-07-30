using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : Interact
{
    protected override void OnEnterTrigger(Player player)
    {
        player.Stats.Hp -= value;
    }
    protected override void OnExitTrigger(Player player)
    {
        throw new System.NotImplementedException();
    }
}
