using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

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


    //��ȭ ���Կ� �ְ� ���θ޴��� �� �� ���ư���
    //�� �� �κ��丮�� ������ ����ȭ
}
