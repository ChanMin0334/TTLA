using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : ScriptableObject {
    public string   enemyName;    // 적 이름
    public Stat     stat;         // 앞서 만든 Stat
    public float    attackRange;  // 사거리
    public GameObject prefab;     // 소환할 몬스터 프리팹
    public Sprite   icon;         // UI용 아이콘
}
