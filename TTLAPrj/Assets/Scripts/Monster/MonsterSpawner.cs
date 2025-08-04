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

    public void SpawnMonsters()
    {
        foreach (var info in spawnList)
        {
            for (int i = 0; i < info.count; i++)
            {
                Vector3 spawnPos = GetRandomPositionInArea();
                GameObject monster = Instantiate(info.monsterPrefab, spawnPos, Quaternion.identity);
                monster.transform.SetParent(this.transform, true);
            }
        }
    }

    Vector3 GetRandomPositionInArea()
    {
        // transform.position을 중심으로, localScale.x, localScale.y를 영역 크기로 사용
        Vector3 center = transform.position;
        Vector3 size = transform.localScale;

        float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float y = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);

        return new Vector3(x, y, 0f);
    }

#if UNITY_EDITOR
    // 씬 뷰에서 영역을 시각적으로 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, 0.1f));
    }
#endif
}
