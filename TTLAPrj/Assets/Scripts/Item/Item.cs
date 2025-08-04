using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int itemID; // �������� ���� ID
    public bool isUsable; // ������ ��� ���� ����
    public string description; // ������ ����
}