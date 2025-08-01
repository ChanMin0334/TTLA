using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour
{
    //모노가 빠지면 -> 초기화를 직접적으로 해줘야한다
    private Transform enhancePlace;
    private Transform inventoryPlace;
    public InventoryItem Data { get; private set; }

    [SerializeField] private Image image;
    [SerializeField] private bool IsEnhance;

    public Button clickButton;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        image = GetComponent<Image>();
        clickButton = GetComponent<Button>();
        clickButton.onClick.AddListener(MoveEnhance);
    }

    private void MoveEnhance()
    {
        if(!IsEnhance)
        {
            transform.SetParent(inventoryPlace, false);
            IsEnhance = true;
        }
        else
        {
            if(enhancePlace.childCount > 0)
            {
                return;
            }

            transform.SetParent(enhancePlace, false);
            transform.localPosition = Vector3.zero;
            IsEnhance = false;
        }

        UIManager.Instance.CallUpdateUI();
    }

    public void SetItem(InventoryItem equipData, GameObject inv, GameObject en)
    {
        this.Data = equipData; 
        this.inventoryPlace = inv.transform;
        this.enhancePlace = en.transform;

        if (this.Data != null)
        {
            image.sprite = this.Data.itemData.icon;
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
    }
}
