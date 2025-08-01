using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour
{
    //��밡 ������ -> �ʱ�ȭ�� ���������� ������Ѵ�
    public InventoryItem Data { get; private set; }

    [SerializeField] private Image image;

    public Button clickButton;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        image = GetComponent<Image>();
        clickButton = GetComponent<Button>();
    }

    public void SetItem(InventoryItem equipData, Action<GameObject> onClick)
    {
        this.Data = equipData;

        if (this.Data != null)
        {
            image.sprite = this.Data.itemData.icon;
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }

        SetEvent(onClick);
    }

    public void SetEvent(Action<GameObject> onClick)
    {
        clickButton.onClick.RemoveAllListeners();
        if (clickButton != null)
        {
            clickButton.onClick.AddListener(() => onClick.Invoke(this.gameObject));
        }
    }
}
