using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SkillData", menuName = "skill/Data")]
public class Skill : ScriptableObject
{
    [Header("Skill Info")]
    public int id;
    public string skillName;
    public Sprite skillIcon;
    [TextArea(3, 5)]
    //public TextAreaAttribute description;
    public string skillDescription;

    public enum skillType { normal, rare, unique, legend }; //�׽�Ʈ
    public skillType type; //�׽�Ʈ

    [Header("Skill Effect")]
    public SkillAct[] skills;

    public void ApplySkill(Player player)
    {
        foreach (SkillAct skill in skills)
        {
            skill.SkillEffect(player);
        }
    }
}

[CreateAssetMenu(fileName = "SkillDataBase", menuName = "skill/SkillDataBase")]
public class SkillDataBase : ScriptableObject
{
    public Skill[] skills;

}

