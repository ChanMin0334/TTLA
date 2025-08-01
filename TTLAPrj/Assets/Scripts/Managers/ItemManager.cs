using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //ItemManager 아이템 관리
    public static ItemManager Instance { get; private set; }

    public InventoryUI equipUI;
    public InventoryUI enhanceUI;
    public GameObject slotPrefab; //슬롯 프리팹
    //실 아이템 객체가 저장된 곳
    public List<InventoryItem> inventory = new List<InventoryItem>();

    //데이터만 가지고 꺼내쓸 수 있는 함수가 필요하다.

    //mvc 패턴

    //초기 아이템 테스트용
    [SerializeField] public Equipment[] slots;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //테스트용 초기 데이터로 아이템 생성
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

    //인벤토리에 있는 함수를 호출한다.
    public void InventoryItemAdd(Equipment So)
    {
        inventory.Add(new InventoryItem(So));
    }

    public void InventoryUIClear()
    {
        //모르겠띠
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

    //강화 슬롯에 넣고 메인메뉴로 갈 시 돌아가기
    //양 측 인벤토리의 아이템 동기화
}
