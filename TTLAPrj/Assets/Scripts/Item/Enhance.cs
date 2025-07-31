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

        Equipment data = slotItem.Data;

        EnhanceResult result = TryEnhancement(data);

        UpgradeResult(result, data);
    }

    public EnhanceResult TryEnhancement(Equipment item)
    {
        if(item.maxEnhanceLevel <= item.nowLevel) //�ִ� ���� �Ͻÿ�
        {
            return EnhanceResult.MaxLevel;
        }

        float successChance = item.chances[item.nowLevel];
    
        if(Random.value < successChance) //��ȭ ����
        {
            return EnhanceResult.Success;
        }
        else
        {
            return EnhanceResult.Fail;
        }
    }

    public void UpgradeResult(EnhanceResult result, Equipment data) //Enum����� ó��
    {
        switch (result)
        {
            case EnhanceResult.Success:
                data.nowLevel++;
                Debug.Log("��ȭ ����");
                break;
            case EnhanceResult.Fail:
                Debug.Log("��ȭ ����");
                break;
            case EnhanceResult.MaxLevel:
                Debug.Log("�ִ� ����");
                break;
        }
    }

    public void UpdateItemStat(Equipment data)
    {
        if (data.bonus == null)
        {
            Debug.Log("itemData.bonus is NULL");
            return;
        }

        data.bonus.Atk = data.appendStat.Atk * (data.nowLevel + 1);
        data.bonus.AtkSpeed = data.appendStat.Atk * (data.nowLevel + 1);
        data.bonus.Hp = data.appendStat.Atk * (data.nowLevel + 1);
        data.bonus.Speed = data.appendStat.Atk * (data.nowLevel + 1);

        Debug.Log("������ ���� ���׷��̵� �ݿ� ����!");
    }
}
