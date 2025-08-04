using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CharacterInfoUI : MonoBehaviour
{
    [Header("Character UI")]
    [SerializeField] Image character;
    [SerializeField] Sprite[] otherCharacters;

    int characterIndex;

    [Header("Character Info")]
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text atkText;
    [SerializeField] TMP_Text atkSpeedText;
    [SerializeField] TMP_Text speedText;

    [Header("Background UI")]
    [SerializeField] Image background;
    [SerializeField] Sprite[] otherBackground;

    int backgroundIndex;

    //테스트
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

    public void ChangeBackground(int index)
    {
        backgroundIndex += index;

        if (backgroundIndex < 0)
            backgroundIndex = otherBackground.Length - 1;
        else if (backgroundIndex >= otherBackground.Length)
            backgroundIndex = 0;

        background.DOFade(0f, 0.2f).OnComplete(() =>
        {
            background.sprite = otherBackground[backgroundIndex];
            background.DOFade(1f, 0.2f);
        });
    }

    public void OnLeftButtonSprite() => ChangeSprite(-1);
    public void OnRightButtonSprite() => ChangeSprite(1);

    public void OnLeftButtonBackground() => ChangeBackground(-1);

    public void OnRightButtonBackground() => ChangeBackground(1);

    public void ShowCharacterInfo()
    {
        hpText.text = $"체력 : {player.Stats.Hp}"; 
        atkText.text = $"공격력 : {player.Stats.Atk}";
        atkSpeedText.text = $"공격속도 : {player.Stats.AtkSpeed}";
        speedText.text = $"이동속도 : {player.Stats.Speed}";
    }
}