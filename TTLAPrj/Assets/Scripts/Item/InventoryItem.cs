using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public Equipment itemData;
    public int nowLevel;
    public Stats itemStat;

    public InventoryItem(Equipment data)
    {
        itemData = data;
        itemStat = new Stats(data.bonus.Atk, data.bonus.Hp, data.bonus.AtkSpeed, data.bonus.Speed);
        nowLevel = 0;
    }
}
