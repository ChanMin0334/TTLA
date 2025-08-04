using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Entity Entity;         // �÷��̾�/���� �� ���� (Inspector���� �Ҵ�)
    public Image hpFillImage;     // HP�� �̹��� (fillAmount ���)
    public float maxWidth = 1f; // HP�� 100%�� ���� HP�� width (�ȼ� ����, Inspector���� ����)

    private RectTransform hpRect;

    void Start()
    {
        if (hpFillImage != null)
            hpRect = hpFillImage.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Entity != null && hpFillImage != null && Entity.Stats != null && hpRect != null)
        {
            float ratio = Mathf.Clamp01(Entity.Stats.Hp / Entity.Stats.HpMax);
            // width ����
            Vector2 size = hpRect.sizeDelta;
            size.x = maxWidth * ratio;
            hpRect.sizeDelta = size;
        }
    }
}
