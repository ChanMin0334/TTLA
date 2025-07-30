using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public int itemID; // 아이템의 고유 ID
    public int quantity; // 아이템의 개수
    public bool isUsable; // 아이템 사용 가능 여부
    public string description; // 아이템 설명

    public abstract void Use();
}
