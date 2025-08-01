using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] Transform enhancePlace;

    [Header("UI Component")]
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text chanceText;
    [SerializeField] TMP_Text equipBonus;

    [SerializeField] UISlider uiSlider; 

    private SlotItem GetEnhanceSlotItem()
    {
        if(enhancePlace == null)
        {
            Debug.Log("강화 슬롯 빔");
            return null;
        }

        return enhancePlace.GetComponentInChildren<SlotItem>();
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
            Debug.Log("equip가 null입니다.");
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

        nameText.text = $"장비명 :{equip.itemData.name}";
        levelText.text = $"레벨 {equip.nowLevel}";
        chanceText.text = $"확률 {equip.itemData.chances[equip.nowLevel] * 100}%";

        switch(equip.itemData.equipmentType)
        {
            case EquipmentType.Weapon:
            type = "공격력";
                bonusStat = equip.itemStat.Atk;
                break;

            case EquipmentType.Armor:
                type = "체력";
                bonusStat = equip.itemStat.Hp;
                break;

            case EquipmentType.Shoes:
                type = "이동속도";
                bonusStat = equip.itemStat.Speed;
                break;
        }
        equipBonus.text = $"{type} + {bonusStat}";

        if (uiSlider != null)
            uiSlider.SlideOn(0.3f);
    }
}
