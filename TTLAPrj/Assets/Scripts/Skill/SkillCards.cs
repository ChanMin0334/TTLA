using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillCards : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] SkillDataBase skillDataBase; //���� ID�� �ɷ� �����Ҷ� ��� ����

    [Header("UI Components")]
    [SerializeField] SpriteRenderer border;
    [SerializeField] SpriteRenderer abilityImage;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text descriptionTMP;

    [Header("Grade Borders")]
    [SerializeField] Sprite normalBorder;
    [SerializeField] Sprite rareBorder;
    [SerializeField] Sprite uniqueBorder;
    [SerializeField] Sprite legendBorder;

    [SerializeField] Color highlightColor = new Color(1f, 1f, 1f, 0.5f);
    Color originalColor;

    UIManager manager;

    public Skill currentSkill;

    public int CardIndex { get; set; }

    bool isSelected = false;

    Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale; // ���� ũ�� ����
        transform.localScale = Vector3.zero; //�����Ҷ� ��Ȱ��ȭ

        originalColor = border.color;
    }

    public void SetSkill(Skill skill)
    {
        nameTMP.text = skill.skillName;
        descriptionTMP.text = skill.skillDescription;
        abilityImage.sprite = skill.skillIcon;
        SetBorder(skill.type);
        currentSkill = skill;

        DeSelect();
    }

    void SetBorder(Skill.skillType type) //ī�� ��޺��� Border ����
    {
        switch (type)
        {
            case Skill.skillType.normal: border.sprite = normalBorder; break;
            case Skill.skillType.rare: border.sprite = rareBorder; break;
            case Skill.skillType.unique: border.sprite = uniqueBorder; break;
            case Skill.skillType.legend: border.sprite = legendBorder; break;
        }
    }

    public void ShowIn(float duration = 0.3f)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(originalScale, duration).SetEase(Ease.OutBack));
        seq.Append(transform.DOShakeScale(0.2f, 0.1f)); // ��鸮�� ȿ��
    }

    public void Showout(float delay = 0f, float duration = 0.3f)
    {
        transform.DOScale(Vector3.zero, duration).SetEase(Ease.InOutQuad);
        DeSelect();
    }

    public void Select() //������
    {
        if (isSelected) return;
        isSelected = true;
        border.color = highlightColor;
    }

    public void DeSelect() //������
    {
        if (!isSelected) return;
        isSelected = false;
        border.color = originalColor;
    }

    private void OnMouseEnter()
    {
        if (!isSelected)
        {
            border.color = highlightColor * 0.9f;  // �ణ ���� ���̶���Ʈ
        }
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            border.color = originalColor;
        }
    }

    private void OnMouseDown()
    {
        UIManager.Instance.OnCardSelected(this);
    }

    public int GetSkillID() //null �̸� 0 �ƴϸ� id �� ��ȯ
    {
        return currentSkill != null ? currentSkill.id : 0;
    }
}


