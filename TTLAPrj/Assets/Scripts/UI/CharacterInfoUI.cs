using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CharacterInfoUI : MonoBehaviour
{
    [Header("Utility UI")]
    [SerializeField] Image character;
    [SerializeField] Sprite[] otherCharacters;
    int characterIndex;

    [Header("Character Info")]
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text atkText;
    [SerializeField] TMP_Text atkSpeedText;
    [SerializeField] TMP_Text speedText;

    //�׽�Ʈ
    [SerializeField] Player player;

    private void OnEnable()
    {
        ShowCharacterInfo();
    }
    public void ChangeSprite(int index)
    {
        characterIndex += index;

        if (characterIndex < 0)
            characterIndex = otherCharacters.Length - 1;
        else if (characterIndex >= otherCharacters.Length)
            characterIndex = 0;

        character.DOFade(0f, 0.2f).OnComplete(() =>
        {
            character.sprite = otherCharacters[characterIndex];
            character.DOFade(1f, 0.2f);
        });
    }

    public void OnLeftButton() => ChangeSprite(-1);
    public void OnRightButton() => ChangeSprite(1);

    public void ShowCharacterInfo()
    {
        hpText.text = $"ü�� : {player.Stats.Hp}"; 
        atkText.text = $"���ݷ� : {player.Stats.Atk}";
        atkSpeedText.text = $"���ݼӵ� : {player.Stats.AtkSpeed}";
        speedText.text = $"�̵��ӵ� : {player.Stats.Speed}";
    }
}