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
        //�𸣰ڶ�
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
        //�κ��丮 ���� ��� = �� ���� ����, �ٵ� �κ��丮�� �θ� ������ �����ؾߵ�. �װ� ��ĳ��?
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
            //�̹� ��ȭĭ�� ���� �÷���
            return;
        }

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

        slotItem.transform.SetParent(this.transform, false);

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
