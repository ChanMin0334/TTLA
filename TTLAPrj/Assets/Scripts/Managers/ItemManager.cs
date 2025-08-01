using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //ItemManager ������ ����
    public static ItemManager Instance { get; private set; }

    public InventoryUI equipUI;
    public InventoryUI enhanceUI;
    public GameObject slotPrefab; //���� ������
    //�� ������ ��ü�� ����� ��
    public List<InventoryItem> inventory = new List<InventoryItem>();

    //�����͸� ������ ������ �� �ִ� �Լ��� �ʿ��ϴ�.

    //mvc ����

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            enhanceUI.InventoryUIPrint();
            equipUI.InventoryUIPrint();
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            InventoryUIClear();
        }
    }

    //�κ��丮�� �ִ� �Լ��� ȣ���Ѵ�.
    public void InventoryItemAdd(Equipment So)
    {
        inventory.Add(new InventoryItem(So));
    }

    public void InventoryUIClear()
    {
        //�𸣰ڶ�
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        if (enhanceUI.enhancePlace.childCount > 0 || equipUI.equipPlace.childCount > 0)
        {
            Destroy(enhanceUI.enhancePlace.GetChild(0).gameObject);
            Destroy(equipUI.equipPlace.GetChild(0).gameObject);
        }
    }

    //��ȭ ���Կ� �ְ� ���θ޴��� �� �� ���ư���
    //�� �� �κ��丮�� ������ ����ȭ
}
