using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "skillEffect/AtkSpeedIncrease")]
public class AtkSpeedIncrease : SkillAct
{
    public float atkSpeedAmount;
    public override void SkillEffect(Player player)
    {
        player.Stats.AtkSpeed += atkSpeedAmount;
    }
}
