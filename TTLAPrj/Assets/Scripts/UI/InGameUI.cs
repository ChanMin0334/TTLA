using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TMP_Text enemyCount;
    [SerializeField] Player player;

    [SerializeField] Image healthBar;
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

        enemyCount.text = $"³²Àº Àû {count}";

        healthBar.fillAmount = player.Stats.Hp / maxHP;
    }
}
