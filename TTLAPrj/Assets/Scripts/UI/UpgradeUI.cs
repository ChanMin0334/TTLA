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

    private SlotItem GetEnhanceSlotItem()
    {
        if(enhancePlace == null)
        {
            Debug.Log("°­È­ ½½·Ô ºö");
            return null;
        }

        return enhancePlace.GetComponentInChildren<SlotItem>();
    }

    public void UpdateUpgradeUI()
    {
        SlotItem slotItem = GetEnhanceSlotItem();

        if (slotItem == null)
        {
            Debug.LogWarning("SlotItem is Null");
            SetUpgradeUI(null);
            return;
        }
            
        InventoryItem equip = slotItem.Data;

        if (equip == null)
        {
            Debug.Log("equip°¡ nullÀÔ´Ï´Ù.");
            return;
        }

        Debug.Log("UpdateUpgradeUI°¡ È£ÃúµÊ.");

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
            return;
        }

        string type = "";
        float bonusStat = 0f;

        nameText.text = $"Àåºñ¸í :{equip.itemData.name}";
        levelText.text = $"·¹º§ {equip.nowLevel}";
        chanceText.text = $"È®·ü {equip.itemData.chances[equip.nowLevel] * 100}%";

        switch(equip.itemData.equipmentType)
        {
            case EquipmentType.Weapon:
            type = "°ø°Ý·Â";
                bonusStat = equip.itemStat.Atk;
                break;

            case EquipmentType.Armor:
                type = "Ã¼·Â";
                bonusStat = equip.itemStat.Hp;
                break;

            case EquipmentType.Shoes:
                type = "ÀÌµ¿¼Óµµ";
                bonusStat = equip.itemStat.Speed;
                break;
        }
        equipBonus.text = $"{type} + {bonusStat}";

        Debug.Log("SetUpgradeUI°¡ È£ÃúµÊ.");
    }
}
