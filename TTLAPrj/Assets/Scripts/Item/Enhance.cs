using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public enum EnhanceResult
{
    Success,
    Fail,
    MaxLevel,
}
public class Enhance : MonoBehaviour
{
    [SerializeField] private GameObject enhanceItemSlot;
    Button enhanceButton;

    private void Start()
    {
        enhanceButton = GetComponentInChildren<Button>();
    }
    public void OnEnhanceClick()
    {
        //강화 아이템 확인
        if(enhanceItemSlot.transform.childCount == 0)
        {
            Debug.Log("강화 아이템x");
            return;
        }

        //슬롯 아이템 가져오기
        SlotItem slotItem = enhanceItemSlot.GetComponentInChildren<SlotItem>();
        if (slotItem == null) {
            Debug.Log("slotItem 스크립트 없음");
            return;
        }

        Equipment data = slotItem.Data;

        EnhanceResult result = TryEnhancement(data);

        UpgradeResult(result, data);
    }

    public EnhanceResult TryEnhancement(Equipment item)
    {
        if(item.maxEnhanceLevel <= item.nowLevel) //최대 레벨 일시에
        {
            return EnhanceResult.MaxLevel;
        }

        float successChance = item.chances[item.nowLevel];
    
        if(Random.value < successChance) //강화 성공
        {
            return EnhanceResult.Success;
        }
        else
        {
            return EnhanceResult.Fail;
        }
    }

    public void UpgradeResult(EnhanceResult result, Equipment data) //Enum결과값 처리
    {
        switch (result)
        {
            case EnhanceResult.Success:
                data.nowLevel++;
                Debug.Log("강화 성공");
                break;
            case EnhanceResult.Fail:
                Debug.Log("강화 실패");
                break;
            case EnhanceResult.MaxLevel:
                Debug.Log("최대 레벨");
                break;
        }
    }

    public void UpdateItemStat(Equipment data)
    {
        if (data.bonus == null)
        {
            Debug.Log("itemData.bonus is NULL");
            return;
        }

        data.bonus.Atk = data.appendStat.Atk * (data.nowLevel + 1);
        data.bonus.AtkSpeed = data.appendStat.Atk * (data.nowLevel + 1);
        data.bonus.Hp = data.appendStat.Atk * (data.nowLevel + 1);
        data.bonus.Speed = data.appendStat.Atk * (data.nowLevel + 1);

        Debug.Log("아이템 스탯 업그레이드 반영 성공!");
    }
}
