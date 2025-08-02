using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum InvType
{
    Equip,
    Enhance
}

public class InventoryUI : MonoBehaviour
{
    ItemManager inventoryManager;
    public InvType invType;
    //�������� �߰� ������ ������ �Ŵ����� ����
    public Transform InvSlotPlace;
    public Transform enhancePlace;
    public Transform equipPlace;

    //test
    [SerializeField] Player player;

    public void Start()
    {
        inventoryManager = ItemManager.Instance;
        InventoryUIPrint();
    }

    public void InventoryUIPrint()
    {
        Action<GameObject> action = null;
        //�κ��丮 ���� ��� = �� ���� ����, �ٵ� �κ��丮�� �θ� ������ �����ؾߵ�. �װ� ��ĳ��?
        foreach (InventoryItem item in inventoryManager.inventory)
        {
            GameObject slot = Instantiate(inventoryManager.slotPrefab, InvSlotPlace);
            SlotItem slotItem = slot.GetComponent<SlotItem>();

            if (invType == InvType.Enhance)
            {
                action = OnEnhanceEvent;
            }
            else if(invType == InvType.Equip)
            {
                action = OnEquipEvent;
            }
            slotItem.SetItem(item, action);
        }
    }

    public void OnEnhanceEvent(GameObject clickSlot)
    {
        SlotItem slotItem = clickSlot.GetComponent<SlotItem>();
        if (slotItem == null) return;

        if(enhancePlace.childCount > 0)
        {
            //�̹� ��ȭĭ�� ���� �÷���

            UIManager.Instance.CallUpdateUI();
            return;
        }

        //����� ���ξ��ٰ� ���� �ѱ�鼭 ���ֱ�
        slotItem.transform.SetParent(enhancePlace, false);
        slotItem.transform.localPosition = Vector3.zero;

        UIManager.Instance.CallUpdateUI();
        slotItem.SetEvent(OnReturnEvent);    
        Debug.Log("�� ��ȭ �κ��� ����");
    }

    public void OnEquipEvent(GameObject clickSlot)
    {
        SlotItem slotItem = clickSlot.GetComponent<SlotItem>();
        if (slotItem == null) return;

        if (equipPlace.childCount > 0) {
            return;
        }

        //���� �߰��� �ʿ��� ������ Ÿ�� �����;��Ѵ�.
        //if (slotItem.Data.itemData.equipmentType == EquipmentType.Weapon)

        slotItem.transform.SetParent(equipPlace, false);
        slotItem.transform.localPosition = Vector3.zero;

        slotItem.SetEvent(OnReturnEvent);

        player.AddStats(slotItem.Data.itemStat); // �׽�Ʈ
        UIManager.Instance.CallShowCharacterInfo();

        Debug.Log("�� ���� �κ��� ����");
    }

    public void OnReturnEvent(GameObject clickSlot)
    {
        SlotItem slotItem = clickSlot.GetComponent<SlotItem>();
        if (slotItem == null) return;

        slotItem.transform.SetParent(InvSlotPlace, false);

        if (invType == InvType.Equip)
        {
            slotItem.SetEvent(OnEquipEvent);
            player.RemoveStats(slotItem.Data.itemStat); // �׽�Ʈ
            UIManager.Instance.CallShowCharacterInfo();
        }
        else if (invType == InvType.Enhance) {
            slotItem.SetEvent(OnEnhanceEvent);
        }

        UIManager.Instance.CallUpdateUI();
        Debug.Log("���������ּ�");
    }


    //UI�� ���⼭ ó���ؾ��Ѵ�. InventoryUI
    //Slot�� �����ؾ��Ѵ�.
}
