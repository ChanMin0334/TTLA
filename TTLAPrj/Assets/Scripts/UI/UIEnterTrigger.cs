using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEnterTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum TriggerType { Enter, Click }

    [Tooltip("Trigger ���")][SerializeField] TriggerType type = TriggerType.Enter;
    [SerializeField] UISlider targetUI; //Silde ���
    [SerializeField] float duration = 0.3f;// �ð�

    public void OnPointerEnter(PointerEventData eventData) // ���콺�� target�� Enter �Ҷ� ȣ��
    {
        if (type != TriggerType.Enter)
            return;

        targetUI.SlideOn(duration);
    }

    public void OnPointerExit(PointerEventData eventData) // ���콺�� target���� Exit�Ҷ� ȣ��
    {
        if (type != TriggerType.Enter)
            return;

        targetUI.SlideOff(duration);
    }

    public void SlideOnBtn() // ��ư��
    {
        if (type != TriggerType.Click)
            return;

        UIManager.Instance.CallInventoryReDraw();
        targetUI.SlideOn(duration);
    }

    public void SlideOffBtn() // ��ư��
    {
        if (type != TriggerType.Click)
            return;

        UIManager.Instance.CallInventoryReDraw();
        targetUI.SlideOff(duration);
    }
}
