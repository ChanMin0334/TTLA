using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TMP_Text enemyCount;
    [SerializeField] Player player;
    [SerializeField] MonsterBoss boss;

    [SerializeField] Image healthBar;
    [SerializeField] GameObject bossInfo;
    [SerializeField] Image bossHealthBar;
    float maxHP;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        maxHP = player.Stats.Hp;
    }
    private void Update()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();
        int count = monsters.Length;

        boss = FindObjectOfType<MonsterBoss>();

        enemyCount.text = $"³²Àº Àû {count}";

        healthBar.fillAmount = player.Stats.Hp / maxHP;

        if(boss != null)
        {
            bossInfo.SetActive(true);

            bossHealthBar.fillAmount = boss.Stats.Hp / 100f;
        }
    }
}
