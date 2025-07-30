using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "skill/Data")]
public class Skill : ScriptableObject
{
    [Header("Skill Info")]
    public string skillName;
    public Sprite skillIcon;
    [TextArea(3,5)]
    public TextAreaAttribute description;

    [Header("Skill Effect")]
    public SkillAct[] skills;

    public void ApplySkill(Player player)
    {
        foreach (SkillAct skill in skills) {
            skill.SkillEffect(player);
        }
    }
}
