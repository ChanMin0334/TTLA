using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    InventoryManager inventoryManager;
    public bool isEquipInv = true;

    public void Start()
    {
        inventoryManager = InventoryManager.Instance;
        InventoryUIPrint(this.gameObject);
    }

    public void InventoryItemAdd(Equipment So)
    {
        inventoryManager.inventory.Add(new InventoryItem(So));
    }

    public void InventoryUIPrint(GameObject obj)
    {
        InventoryUI inv = obj.GetComponent<InventoryUI>();

        if (inv == null) return;

        Action<GameObject> action = null;
        //�κ��丮 ���� ��� = �� ���� ����, �ٵ� �κ��丮�� �θ� ������ �����ؾߵ�. �װ� ��ĳ��?
        foreach (InventoryItem item in inventoryManager.inventory)
        {
            GameObject slot = Instantiate(inventoryManager.slotPrefab, inv.transform);
            SlotItem slotItem = slot.GetComponent<SlotItem>();

            if (inv.isEquipInv)
            {
                action = OnEnhanceEvent;
            }
            else
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

        Debug.Log("�� ��ȭ �κ��� ����");
    }

    public void OnEquipEvent(GameObject clickSlot)
    {
        SlotItem slotItem = clickSlot.GetComponent<SlotItem>();
        if (slotItem == null) return;

        Debug.Log("�� ���� �κ��� ����");
    }


    //UI�� ���⼭ ó���ؾ��Ѵ�. InventoryUI
    //Slot�� �����ؾ��Ѵ�.
}
