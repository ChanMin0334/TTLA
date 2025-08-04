using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //ItemManager 아이템 관리
    public static ItemManager Instance { get; private set; }
    public GameObject slotPrefab; //슬롯 프리팹
    //실 아이템 객체가 저장된 곳
    public List<InventoryItem> inventory = new List<InventoryItem>();

    //호출할 인벤토리의 이벤트들 저장
    public Action OnInvenAction;

    //데이터만 가지고 꺼내쓸 수 있는 함수가 필요하다.

    //mvc 패턴 필요

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

    //추가 제거 테스트
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

    //그냥 새로고침용
    public void InventoryCall()
    {
        OnInvenAction?.Invoke();
    }

    //인벤토리에 있는 함수를 호출한다.
    public void InventoryItemAdd(InventoryItem slotItem)
    {
        inventory.Add(slotItem);
        OnInvenAction?.Invoke();
    }

    //삭제가 필요한가 모르겠네?
    public void InventoryItemRemove(InventoryItem slotItem)
    {
        //삭제 이렇게 하면 안되네 ㅋㅋ
        inventory.Remove(slotItem);
        OnInvenAction?.Invoke();
    }

    //강화 슬롯에 넣고 메인메뉴로 갈 시 돌아가기
    //양 측 인벤토리의 아이템 동기화
}
