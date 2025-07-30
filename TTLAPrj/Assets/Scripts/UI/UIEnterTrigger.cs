using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEnterTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UISlider targetUI; //Silde ���
    [SerializeField] float duration = 0.3f;// �ð�

    public void OnPointerEnter(PointerEventData eventData) // ���콺�� target�� Enter �Ҷ� ȣ��
    {
        if (targetUI != null)
        {
            targetUI.SlideOn(duration);
        }
        else
            Debug.LogError("targetUI is Missing");
    }

    public void OnPointerExit(PointerEventData eventData) // ���콺�� target���� Exit�Ҷ� ȣ��
    {
        if (targetUI != null)
            targetUI.SlideOff(duration);
    }
}
