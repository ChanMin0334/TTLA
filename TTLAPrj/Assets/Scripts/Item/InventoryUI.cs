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
    InventoryManager inventoryManager;
    public InvType invType;

    [SerializeField] private Transform enhancePlace;
    [SerializeField] private Transform equipPlace;

    public void Start()
    {
        inventoryManager = InventoryManager.Instance;
        InventoryUIPrint(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            InventoryUIPrint(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            InventoryUIClear();
        }
    }

    public void InventoryItemAdd(Equipment So)
    {
        inventoryManager.inventory.Add(new InventoryItem(So));
    }

    public void InventoryUIClear()
    {
        //모르겠띠
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        if(enhancePlace.childCount > 0 || equipPlace.childCount > 0)
        {
            Destroy(enhancePlace.GetChild(0).gameObject);
            Destroy(equipPlace.GetChild(0).gameObject);
        }
    }

    public void InventoryUIPrint(GameObject obj)
    {
        Action<GameObject> action = null;
        //인벤토리 내용 출력 = 즉 슬롯 생성, 근데 인벤토리의 부모 하위에 생성해야됨. 그걸 어캐암?
        foreach (InventoryItem item in inventoryManager.inventory)
        {
            GameObject slot = Instantiate(inventoryManager.slotPrefab, transform);
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
            //이미 강화칸에 물건 올려둠
            return;
        }

        slotItem.transform.SetParent(enhancePlace, false);
        slotItem.transform.localPosition = Vector3.zero;

        slotItem.SetEvent(OnReturnEvent);
        Debug.Log("나 강화 인벤에 있음");
    }

    public void OnEquipEvent(GameObject clickSlot)
    {
        SlotItem slotItem = clickSlot.GetComponent<SlotItem>();
        if (slotItem == null) return;

        if (equipPlace.childCount > 0) {
            return;
        }

        //조건 추가가 필요함 아이템 타입 가져와야한다.
        //if (slotItem.Data.itemData.equipmentType == EquipmentType.Weapon)

        slotItem.transform.SetParent(equipPlace, false);
        slotItem.transform.localPosition = Vector3.zero;

        slotItem.SetEvent(OnReturnEvent);
        Debug.Log("나 장착 인벤에 있음");
    }

    public void OnReturnEvent(GameObject clickSlot)
    {
        SlotItem slotItem = clickSlot.GetComponent<SlotItem>();
        if (slotItem == null) return;

        slotItem.transform.SetParent(this.transform, false);

        if (invType == InvType.Equip)
        {
            slotItem.SetEvent(OnEquipEvent);
        }
        else if (invType == InvType.Enhance) {
            slotItem.SetEvent(OnEnhanceEvent);
        }
        
        Debug.Log("돌려보내주셈");
    }


    //UI를 여기서 처리해야한다. InventoryUI
    //Slot을 관리해야한다.
}
