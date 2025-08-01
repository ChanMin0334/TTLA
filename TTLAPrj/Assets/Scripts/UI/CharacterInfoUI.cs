using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterInfoUI : MonoBehaviour
{
    [Header("Utility UI")]
    [SerializeField] Image character;
    [SerializeField] Sprite[] otherCharacters;
    int characterIndex;

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
}