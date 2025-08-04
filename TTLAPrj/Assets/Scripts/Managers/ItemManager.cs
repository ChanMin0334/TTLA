using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //ItemManager ������ ����
    public static ItemManager Instance { get; private set; }
    public GameObject slotPrefab; //���� ������
    //�� ������ ��ü�� ����� ��
    public List<InventoryItem> inventory = new List<InventoryItem>();

    //ȣ���� �κ��丮�� �̺�Ʈ�� ����
    public Action OnInvenAction;

    //�����͸� ������ ������ �� �ִ� �Լ��� �ʿ��ϴ�.

    //mvc ���� �ʿ�

    //�ʱ� ������ �׽�Ʈ��
    [SerializeField] public Equipment[] slots;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //�׽�Ʈ�� �ʱ� �����ͷ� ������ ����
        foreach (Equipment itemData in slots)
        {
            inventory.Add(new InventoryItem(itemData));
        }
    }

    //�߰� ���� �׽�Ʈ
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            InventoryItemRemove(inventory[0]);
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            InventoryItem testItem = new InventoryItem(slots[0]);
            InventoryItemAdd(testItem);
        }
    }

    //�׳� ���ΰ�ħ��
    public void InventoryCall()
    {
        OnInvenAction?.Invoke();
    }

    //�κ��丮�� �ִ� �Լ��� ȣ���Ѵ�.
    public void InventoryItemAdd(InventoryItem slotItem)
    {
        inventory.Add(slotItem);
        OnInvenAction?.Invoke();
    }

    //������ �ʿ��Ѱ� �𸣰ڳ�?
    public void InventoryItemRemove(InventoryItem slotItem)
    {
        //���� �̷��� �ϸ� �ȵǳ� ����
        inventory.Remove(slotItem);
        OnInvenAction?.Invoke();
    }

    //��ȭ ���Կ� �ְ� ���θ޴��� �� �� ���ư���
    //�� �� �κ��丮�� ������ ����ȭ
}
