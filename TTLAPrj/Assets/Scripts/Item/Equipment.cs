public enum EquipmentType
{
    Weapon,
    Armor,
    Shoes,
}

public class Equipment : Item
{
    public Stats appendStat;
    public EquipmentType equipmentType; // 장비 종류 (무기, 방어구, 신발 등)

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
