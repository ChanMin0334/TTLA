using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform invSlot;
    [SerializeField] private Transform enhanceSlot;

    [SerializeField] public Equipment[] slots;


    private void Start()
    {
        InventoryItemGenerate();
    }

    private void InventoryItemGenerate()
    {
        foreach (Equipment item in slots)
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
