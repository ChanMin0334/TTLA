using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEnterTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum TriggerType { Enter, Click }

    [Tooltip("Trigger 방식")][SerializeField] TriggerType type = TriggerType.Enter;
    [SerializeField] UISlider targetUI; //Silde 대상
    [SerializeField] float duration = 0.3f;// 시간

    public void OnPointerEnter(PointerEventData eventData) // 마우스가 target에 Enter 할때 호출
    {
        if (type != TriggerType.Enter)
            return;

        targetUI.SlideOn(duration);
    }

    public void OnPointerExit(PointerEventData eventData) // 마우스가 target에서 Exit할때 호출
    {
        if (type != TriggerType.Enter)
            return;

        targetUI.SlideOff(duration);
    }

    public void SlideOnBtn() // 버튼용
    {
        if (type != TriggerType.Click)
            return;

        UIManager.Instance.CallInventoryReDraw();
        targetUI.SlideOn(duration);
    }

    public void SlideOffBtn() // 버튼용
    {
        if (type != TriggerType.Click)
            return;

        UIManager.Instance.CallInventoryReDraw();
        targetUI.SlideOff(duration);
    }
}
