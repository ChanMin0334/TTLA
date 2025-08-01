using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] Transform enhancePlace;

    [Header("UI Component")]
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text chanceText;
    [SerializeField] TMP_Text equipBonus;

    [SerializeField] ParticleSystem successEffect;
    [SerializeField] ParticleSystem failEffect;

    [SerializeField] UISlider uiSlider; 

   

    private SlotItem GetEnhanceSlotItem()
    {
        if(enhancePlace == null)
        {
            Debug.Log("��ȭ ���� ��");
            return null;
        }

        return enhancePlace.GetComponentInChildren<SlotItem>();
    }

    private Image GetEnhanceImage()
    {
        var slotItem = GetEnhanceSlotItem();
        if (slotItem == null)
            return null;

        return slotItem.GetComponentInChildren<Image>();
    }

    public void UpdateUpgradeUI()
    {
        SlotItem slotItem = GetEnhanceSlotItem();

        if (slotItem == null)
        {
            Debug.Log("SlotItem is Null");
            SetUpgradeUI(null);
            return;
        }
            
        InventoryItem equip = slotItem.Data;

        if (equip == null)
        {
            Debug.Log("equip�� null�Դϴ�.");
            return;
        }
        SetUpgradeUI(equip);
    }

    private void SetUpgradeUI(InventoryItem equip)
    {
        if (equip == null)
        {
            nameText.text = "";
            levelText.text = "";
            chanceText.text = "";
            equipBonus.text = "";

            if (uiSlider != null)
                uiSlider.SlideOff(0.3f);
            return;
        }

        string type = "";
        float bonusStat = 0f;

        nameText.text = $"��� : {equip.itemData.itemName}";
        levelText.text = $"���� : {equip.nowLevel}";
        chanceText.text = $"Ȯ�� : {equip.itemData.chances[equip.nowLevel] * 100}%";

        switch(equip.itemData.equipmentType)
        {
            case EquipmentType.Weapon:
            type = "���ݷ�";
                bonusStat = equip.itemData.bonus.Atk * (equip.nowLevel) + equip.itemData.appendStat.Atk;
                break;

            case EquipmentType.Armor:
                type = "ü��";
                bonusStat = equip.itemData.bonus.Hp * (equip.nowLevel) + equip.itemData.appendStat.Hp;
                break;

            case EquipmentType.Shoes:
                type = "�̵��ӵ�";
                bonusStat = equip.itemData.bonus.Speed * (equip.nowLevel) + equip.itemData.appendStat.Speed;
                break;
        }
        equipBonus.text = $"{type} + {bonusStat}";

        if (uiSlider != null)
            uiSlider.SlideOn(0.3f);
    }


    public void UpgradeSuccess() //�ӽ�
    {
        var target = GetEnhanceImage();
        Sequence seq = DOTween.Sequence();

        target.color = Color.white;

        seq.Append(target.transform.DOShakeScale(0.3f, 0.3f, 10, 90))
            .Join(target.transform.DOScale(1.2f, 0.15f).SetLoops(2, LoopType.Yoyo))
            .Append(target.DOFade(0.5f, 0.2f).SetLoops(2, LoopType.Yoyo));

        successEffect.Play();

        seq.OnComplete(() =>
        {
            Debug.Log("��ȭ ���� ��");
        });
    }
    public void UpgradeFail() //�ӽ�
    {
        Sequence seq = DOTween.Sequence();
        var target = GetEnhanceImage();

        target.color = Color.white;

        seq.Append(target.transform.DOShakeRotation(0.3f, new Vector3(0, 0, 30), 10, 90))
            .Join(target.DOFade(0.3f, 0.2f).SetLoops(2, LoopType.Yoyo));

            failEffect.Play();

            seq.OnComplete(() =>
            {
                Debug.Log("��ȭ ����");
            });
    }
}
