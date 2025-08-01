using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public enum EnhanceResult
{
    Success,
    Fail,
    MaxLevel,
}
public class Enhance : MonoBehaviour
{
    [SerializeField] private GameObject enhanceItemSlot;
    Button enhanceButton;

    private void Start()
    {
        enhanceButton = GetComponentInChildren<Button>();
    }
    public void OnEnhanceClick()
    {
        //��ȭ ������ Ȯ��
        if(enhanceItemSlot.transform.childCount == 0)
        {
            Debug.Log("��ȭ ������x");
            return;
        }

        //���� ������ ��������
        SlotItem slotItem = enhanceItemSlot.GetComponentInChildren<SlotItem>();
        if (slotItem == null) {
            Debug.Log("slotItem ��ũ��Ʈ ����");
            return;
        }

        InventoryItem data = slotItem.Data;

        EnhanceResult result = TryEnhancement(data);

        UpgradeResult(result, data);

        UIManager.Instance.CallUpdateUI(); // UI Update
    }

    public EnhanceResult TryEnhancement(InventoryItem item)
    {
        if(item.itemData.maxEnhanceLevel <= item.nowLevel) //�ִ� ���� �Ͻÿ�
        {
            return EnhanceResult.MaxLevel;
        }

        float successChance = item.itemData.chances[item.nowLevel];
    
        if(Random.value < successChance) //��ȭ ����
        {
            return EnhanceResult.Success;
        }
        else
        {
            return EnhanceResult.Fail;
        }
    }

    public void UpgradeResult(EnhanceResult result, InventoryItem data) //Enum����� ó��
    {
        switch (result)
        {
            case EnhanceResult.Success:
                data.nowLevel++;
                UpdateItemStat(data);
                UIManager.Instance.CallUpgradeSuccess();
                Debug.Log("��ȭ ����");
                break;
            case EnhanceResult.Fail:
                Debug.Log("��ȭ ����");
                UIManager.Instance.CallUpgradeFail();
                break;
            case EnhanceResult.MaxLevel:
                Debug.Log("�ִ� ����");
                break;
        }
    }

    public void UpdateItemStat(InventoryItem data)
    {
        if (data.itemData.bonus == null)
        {
            Debug.Log("itemData.bonus is NULL");
            return;
        }
        data.itemStat.Atk = data.itemData.bonus.Atk * (data.nowLevel + 1);
        data.itemStat.AtkSpeed = data.itemData.bonus.AtkSpeed * (data.nowLevel + 1);
        data.itemStat.Hp = data.itemData.bonus.Hp * (data.nowLevel + 1);
        data.itemStat.Speed = data.itemData.bonus.Speed * (data.nowLevel + 1);

        Debug.Log("������ ���� ���׷��̵� �ݿ� ����!");
    }
}
