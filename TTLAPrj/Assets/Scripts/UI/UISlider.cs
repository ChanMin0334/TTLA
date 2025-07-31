using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlider : MonoBehaviour
{
    [SerializeField] float onX; 
    [SerializeField] float offX; 

    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(offX, rect.anchoredPosition.y);
    }

    public void SlideOn(float duration)
    {
        rect.DOAnchorPosX(onX, duration).SetEase(Ease.Unset);
    }

    public void SlideOff(float duration)
    {
        rect.DOAnchorPosX(offX, duration).SetEase(Ease.Unset);
    }
}
