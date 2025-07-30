using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEnterTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UISlider targetUI; //Silde 대상
    [SerializeField] float duration = 0.3f;// 시간

    public void OnPointerEnter(PointerEventData eventData) // 마우스가 target에 Enter 할때 호출
    {
        if (targetUI != null)
        {
            targetUI.SlideOn(duration);
        }
        else
            Debug.LogError("targetUI is Missing");
    }

    public void OnPointerExit(PointerEventData eventData) // 마우스가 target에서 Exit할때 호출
    {
        if (targetUI != null)
            targetUI.SlideOff(duration);
    }
}
