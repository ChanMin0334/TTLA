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
    ItemManager itemManager;
    public InvType invType;
    
    public Transform InvSlotPlace;
    public Transform enhancePlace;
    public Transform equipPlace;

    public void Start()
    {
        itemManager = ItemManager.Instance;
        InventoryUIPrint();
        itemManager.OnInvenAction += InventoryReDraw;
    }

    //�������� �߰� ������ ������ �Ŵ����� ����
    //�ʿ��Ѱ�
    //�κ��丮 �׸��� �����(���� ���� �� ����), ���Ե��� ����

    public void InventoryReDraw()
    {
        InventoryClear(InvSlotPlace);
        InventoryClear(enhancePlace);
        InventoryClear(equipPlace);
        InventoryUIPrint();
    }

    public void InventoryClear(Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void InventoryUIPrint()
    {
        Action<GameObject> action = null;
        //�κ��丮 ���� ��� = �� ���� ����, �ٵ� �κ��丮�� �θ� ������ �����ؾߵ�. �װ� ��ĳ��?
        foreach (InventoryItem item in itemManager.inventory)
        {
            GameObject slot = Instantiate(itemManager.slotPrefab, InvSlotPlace);
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
            return;
        }

        //����� ���ξ��ٰ� ���� �ѱ�鼭 ���ֱ�
        slotItem.transform.SetParent(enhancePlace, false);
        slotItem.transform.localPosition = Vector3.zero;

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
        }
        else if (invType == InvType.Enhance) {
            slotItem.SetEvent(OnEnhanceEvent);
        }
        
        Debug.Log("���������ּ�");
    }


    //UI�� ���⼭ ó���ؾ��Ѵ�. InventoryUI
    //Slot�� �����ؾ��Ѵ�.
}
