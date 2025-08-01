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
            Debug.Log("��ȭ ���� ��");
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
            Debug.Log("equip�� null�Դϴ�.");
            return;
        }

        Debug.Log("UpdateUpgradeUI�� ȣ����.");

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

        nameText.text = $"���� :{equip.itemData.name}";
        levelText.text = $"���� {equip.nowLevel}";
        chanceText.text = $"Ȯ�� {equip.itemData.chances[equip.nowLevel] * 100}%";

        switch(equip.itemData.equipmentType)
        {
            case EquipmentType.Weapon:
            type = "���ݷ�";
                bonusStat = equip.itemStat.Atk;
                break;

            case EquipmentType.Armor:
                type = "ü��";
                bonusStat = equip.itemStat.Hp;
                break;

            case EquipmentType.Shoes:
                type = "�̵��ӵ�";
                bonusStat = equip.itemStat.Speed;
                break;
        }
        equipBonus.text = $"{type} + {bonusStat}";

        Debug.Log("SetUpgradeUI�� ȣ����.");
    }
}
