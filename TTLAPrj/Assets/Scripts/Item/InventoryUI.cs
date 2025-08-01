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
        //인벤토리 내용 출력 = 즉 슬롯 생성, 근데 인벤토리의 부모 하위에 생성해야됨. 그걸 어캐암?
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

        Debug.Log("나 강화 인벤에 있음");
    }

    public void OnEquipEvent(GameObject clickSlot)
    {
        SlotItem slotItem = clickSlot.GetComponent<SlotItem>();
        if (slotItem == null) return;

        Debug.Log("나 장착 인벤에 있음");
    }


    //UI를 여기서 처리해야한다. InventoryUI
    //Slot을 관리해야한다.
}
