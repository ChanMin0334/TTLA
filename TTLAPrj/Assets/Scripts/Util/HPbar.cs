using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Entity Entity;         // 플레이어/몬스터 등 참조 (Inspector에서 할당)
    public Image hpFillImage;     // HP바 이미지 (fillAmount 사용)
    public float maxWidth = 1f; // HP가 100%일 때의 HP바 width (픽셀 단위, Inspector에서 설정)

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
            // width 조정
            Vector2 size = hpRect.sizeDelta;
            size.x = maxWidth * ratio;
            hpRect.sizeDelta = size;
        }
    }
}
