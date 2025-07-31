using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityCards : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] AbilitySo abilitySO; //추후 ID로 능력 참조할때 사용 가능

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
        originalScale = transform.localScale; // 원본 크기 저장
        transform.localScale = Vector3.zero; //시작할땐 비활성화

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

    void SetBorder(Ability.abilityType type) //카드 등급별로 Border 세팅
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
        seq.Append(transform.DOShakeScale(0.2f, 0.1f)); // 흔들리는 효과
    }

    public void Showout(float delay = 0f, float duration = 0.3f)
    {
        transform.DOScale(Vector3.zero, duration).SetEase(Ease.InOutQuad);
        DeSelect();
    }

    public void Select() //색변경
    {
        if (isSelected) return;
        isSelected = true;
        border.color = highlightColor; 
    }

    public void DeSelect() //원래색
    {
        if (!isSelected) return;
        isSelected = false;
        border.color = originalColor;
    }

    private void OnMouseEnter()
    {
        if (!isSelected)
        {
            border.color = highlightColor * 0.9f;  // 약간 연한 하이라이트
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

    public int GetAbilityID() //null 이면 0 아니면 id 값 반환
    {
        return currentAbility != null ? currentAbility.id : 0;
    }
}


