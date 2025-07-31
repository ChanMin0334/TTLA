using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityCards : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] AbilitySo abilitySO; //���� ID�� �ɷ� �����Ҷ� ��� ����

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

    [SerializeField] Color highlightColor = Color.black;
    Color originalColor;

    UIManager manager;

    Ability currentAbility;

    public int CardIndex { get; set; }

    bool isSelected = false;

    Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale; // ���� ũ�� ����
        transform.localScale = Vector3.zero; //�����Ҷ� ��Ȱ��ȭ

        originalColor = border.color;
    }

    public void SetAbility(Ability ability)
    {
        nameTMP.text = ability.name;
        descriptionTMP.text = ability.description;
        abilityImage.sprite = ability.sprite;
        SetBorder(ability.type);
        currentAbility = ability;

        DeSelect();
    }

    void SetBorder(Ability.abilityType type) //ī�� ��޺��� Border ����
    {
        switch (type)
        {
            case Ability.abilityType.normal: border.sprite = normalBorder; break;
            case Ability.abilityType.rare: border.sprite = rareBorder; break;
            case Ability.abilityType.unique: border.sprite = uniqueBorder; break;
            case Ability.abilityType.legend: border.sprite = legendBorder; break;
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
        //if(manager != null)
        //{
            UIManager.Instance.OnCardSelected(this);
        //}
    }

    public int GetAbilityID() //null �̸� 0 �ƴϸ� id �� ��ȯ
    {
        return currentAbility != null ? currentAbility.id : 0;
    }
}


