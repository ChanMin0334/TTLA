public enum EquipmentType
{
    Weapon,
    Armor,
    Shoes,
}

public class Equipment : Item
{
    public Stats appendStat;
    public EquipmentType equipmentType; // ��� ���� (����, ��, �Ź� ��)

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
