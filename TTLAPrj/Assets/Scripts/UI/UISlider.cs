using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlider : MonoBehaviour
{
    [SerializeField] float onX; 
    [SerializeField] float offX;

    //Å×½ºÆ®
    [SerializeField] float onY;
    [SerializeField] float offY;

    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        //rect.anchoredPosition = new Vector2(offX, rect.anchoredPosition.y);
        rect.anchoredPosition = new Vector2(offX, offY);
    }

    public void SlideOn(float duration)
    {
        //rect.DOAnchorPosX(onX, duration).SetEase(Ease.Unset);
        rect.DOAnchorPos(new Vector2(onX, onY), duration).SetEase(Ease.Unset);
    }

    public void SlideOff(float duration)
    {
        //rect.DOAnchorPosX(offX, duration).SetEase(Ease.Unset);
        rect.DOAnchorPos(new Vector2(offX, offY), duration).SetEase(Ease.Unset);

    }
}
