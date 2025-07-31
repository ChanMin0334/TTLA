using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "skillEffect/AtkIncrease")]
public class AtkIncrease : SkillAct
{
    public float atkAmount;
    public override void SkillEffect(Player player)
    {
        player.Stats.Atk += atkAmount;
    }
}
