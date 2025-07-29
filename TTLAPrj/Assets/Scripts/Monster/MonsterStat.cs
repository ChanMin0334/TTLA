using UnityEngine;

[System.Serializable]
public class Stat {
    public float Atk      = 10f;   // 공격력
    public float Hp       = 100f;  // 체력
    public float AtkSpeed = 1f;    // 공격 속도(초당 회전수 등)
    public float Speed    = 3f;    // 이동 속도

    // 다른 Stat 데이터를 더하기 해주는 함수
    public void Apply(Stat other) {
        Atk       += other.Atk;
        Hp        += other.Hp;
        AtkSpeed  += other.AtkSpeed;
        Speed     += other.Speed;
    }
}
