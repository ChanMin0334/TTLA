using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Shoes,
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Item/Data")]
public class Equipment : Item
{
    public Stats appendStat;
    public EquipmentType equipmentType; // ��� ���� (����, ��, �Ź� ��)

    public int nowLevel = 0;
    public int maxEnhanceLevel = 10;
    public Stats bonus;

    public int[] costs;
    [Range(0, 1)] public float[] chances; // �迭 ���� �ÿ�����
}