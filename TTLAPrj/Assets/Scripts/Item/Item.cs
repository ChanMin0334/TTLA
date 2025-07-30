using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public int itemID; // �������� ���� ID
    public int quantity; // �������� ����
    public bool isUsable; // ������ ��� ���� ����
    public string description; // ������ ����

    public abstract void Use();
}
