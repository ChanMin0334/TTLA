using UnityEngine.UIElements.Experimental;

[System.Serializable]
public class Stats
{
    public float Atk;
    public float HpMax;
    public float Hp;
    public float AtkSpeed;
    public float Speed;

    public Stats() {}
    public Stats(float atk,  float hp, float atkSpeed, float speed, float hpMax)
    {
        Atk = atk;
        Hp = hp;
        AtkSpeed = atkSpeed;
        Speed = speed;
        HpMax = hpMax;
    }
}
