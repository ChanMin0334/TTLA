using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform invSlot;
    [SerializeField] private Transform enhanceSlot;

    public List<InventoryItem> inventory = new List<InventoryItem>();
    [SerializeField] public Equipment[] slots;


    private void Start()
    {
        foreach (Equipment itemData in slots)
        {
            inventory.Add(new InventoryItem(itemData));
        }

        InventoryItemGenerate();
    }

    private void InventoryItemGenerate()
    {
        foreach (InventoryItem item in inventory)
        {
            GameObject slot = Instantiate(slotPrefab, invSlot);
            SlotItem slotItem = slot.GetComponent<SlotItem>();

            if (slotItem != null)
            {
                slotItem.SetItem(item, invSlot.gameObject, enhanceSlot.gameObject);
            }
            else
            {
                Debug.Log("slotItem no");
            }
        }
    }
}
