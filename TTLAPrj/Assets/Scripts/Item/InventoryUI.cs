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
    //아이템을 추가 삭제는 아이템 매니저가 할일
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
        //인벤토리 내용 출력 = 즉 슬롯 생성, 근데 인벤토리의 부모 하위에 생성해야됨. 그걸 어캐암?
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
            //이미 강화칸에 물건 올려둠

            UIManager.Instance.CallUpdateUI();
            return;
        }

        //만들고 꺼두었다가 정보 넘기면서 켜주기
        slotItem.transform.SetParent(enhancePlace, false);
        slotItem.transform.localPosition = Vector3.zero;

        UIManager.Instance.CallUpdateUI();
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

        player.AddStats(slotItem.Data.itemStat); // 테스트
        UIManager.Instance.CallShowCharacterInfo();

        Debug.Log("나 장착 인벤에 있음");
    }

    public void OnReturnEvent(GameObject clickSlot)
    {
        SlotItem slotItem = clickSlot.GetComponent<SlotItem>();
        if (slotItem == null) return;

        slotItem.transform.SetParent(InvSlotPlace, false);

        if (invType == InvType.Equip)
        {
            slotItem.SetEvent(OnEquipEvent);
            player.RemoveStats(slotItem.Data.itemStat); // 테스트
            UIManager.Instance.CallShowCharacterInfo();
        }
        else if (invType == InvType.Enhance) {
            slotItem.SetEvent(OnEnhanceEvent);
        }

        UIManager.Instance.CallUpdateUI();
        Debug.Log("돌려보내주셈");
    }


    //UI를 여기서 처리해야한다. InventoryUI
    //Slot을 관리해야한다.
}
