using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnInfo
    {
        public GameObject monsterPrefab;
        public int count;
    }

    public SpawnInfo[] spawnList;
    private BoxCollider2D area; // 스폰 구역

    void Awake()
    {
        area = GetComponent<BoxCollider2D>();
        if (area == null)
        {
            Debug.LogError("SpawnArea에 BoxCollider2D가 필요합니다!");
        }
    }

    void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        foreach (var info in spawnList)
        {
            for (int i = 0; i < info.count; i++)
            {
                Vector3 spawnPos = GetRandomPositionInArea();
                // 부모 없이 먼저 생성
                GameObject monster = Instantiate(info.monsterPrefab, spawnPos, Quaternion.identity);
                // 부모로 설정하되, 월드 좌표/스케일 유지
                monster.transform.SetParent(this.transform, true);
            }
        }
    }


    Vector3 GetRandomPositionInArea()
    {
        Bounds bounds = area.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(x, y, 0f);
    }
}
