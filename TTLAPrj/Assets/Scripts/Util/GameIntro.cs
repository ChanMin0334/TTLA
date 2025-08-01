using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameIntro : MonoBehaviour
{
    public GameObject introText; // 20�� �ؽ�Ʈ
    public GameObject introPanel; // ��Ʈ�� �г�
    public GameObject titleImg; // Ÿ��Ʋ �̹���
    public GameObject titleText; // Ÿ��Ʋ �ؽ�Ʈ
    public float fadeDuration = 10f; // �г� ������� �ð�
    public float titleFadeDuration = 5f; // �г� ������� �ð�
    public float moveDuration = 1f; // �г� �ö���� �ð�
    public float moveDistance = 40f; // �г��� �ö���� �Ÿ� (�ȼ� ����)

    private AudioSource audioSource;
    private Image panelImage;
    private RectTransform panelRect;
    private Image titleImage;
    private bool isPanelOn = false; // �г��� ���� �ִ��� ����
    private bool canStart = false; // ��Ʈ�� ���� ����

    void Start()
    {
        panelImage = introPanel.GetComponent<Image>();
        panelRect = introPanel.GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        titleImage = titleImg.GetComponent<Image>();
        StartCoroutine(ShowIntroAndLoadNext());
    }

    // ��Ʈ�ΰ� ���� �� ���� �Է�(Ű, ���콺 Ŭ�� ��)�� �޾� ���� ������ �̵�
    void Update()
    {
        if (canStart)
        {
            // �ƹ� Ű�� �����ų� ���콺 Ŭ�� ��
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                canStart = false; // �ߺ� �Է� ����
                LoadNextScene();
            }
        }
    }

    IEnumerator ShowIntroAndLoadNext()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlaySFX(SFX_Name.SFX_IntroSound);
        introText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        introText.SetActive(false);
        yield return new WaitForSeconds(1f);

        introPanel.SetActive(true);
        titleImg.SetActive(true);

        // �� �ڷ�ƾ�� ���ÿ� ����
        Coroutine move = StartCoroutine(MovePanel());
        Coroutine fadePanel = StartCoroutine(FadeInPanelRGB());
        Coroutine fadeTitle = StartCoroutine(FadeInTitleImg());

        // ��� �ڷ�ƾ�� ���� ������ ���
        yield return move;
        yield return fadePanel;
        yield return fadeTitle;
    }

    IEnumerator MovePanel()
    {
        if (panelRect == null)
            yield break;

        Vector2 startPos = panelRect.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0, moveDistance);
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            panelRect.anchoredPosition = Vector2.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        panelRect.anchoredPosition = endPos;
    }

    IEnumerator FadeInPanelRGB()
    {
        if (panelImage == null)
            yield break;

        Color color = panelImage.color;
        float elapsed = 0f;
        color.r = 100f / 255f;
        color.g = 100f / 255f;
        color.b = 100f / 255f;
        panelImage.color = color;

        bool audioPlayed = false;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            color.r = Mathf.Lerp(100f / 255f, 1f, smoothT);
            color.g = Mathf.Lerp(100f / 255f, 1f, smoothT);
            color.b = Mathf.Lerp(100f / 255f, 1f, smoothT);

            if (!audioPlayed && (color.r * 255f >= 120f || color.g * 255f >= 120f || color.b * 255f >= 120f))
            {
                isPanelOn = true; // �г��� �������� ǥ��
                audioSource.Play();
                audioPlayed = true;
            }

            panelImage.color = color;
            yield return null;
        }
        color.r = 1f;
        color.g = 1f;
        color.b = 1f;
        panelImage.color = color;
    }

    IEnumerator FadeInTitleImg()
    {
        if (titleImage == null || titleText == null)
            yield break;

        Color imgColor = titleImage.color;
        Text textComp = titleText.GetComponent<Text>();
        Color textColor = textComp.color;

        float elapsed = 0f;
        imgColor.a = 0f;
        textColor.a = 0f;
        titleImage.color = imgColor;
        textComp.color = textColor;

        while (!isPanelOn) // �г��� ���� ������ ���
        {
            yield return null;
        }

        canStart = true; // ��Ʈ�� ���� ���� ���·� ����

        while (elapsed < titleFadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / titleFadeDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            imgColor.a = Mathf.Lerp(0f, 1f, smoothT);
            textColor.a = Mathf.Lerp(0f, 1f, smoothT);
            titleImage.color = imgColor;
            textComp.color = textColor;
            yield return null;
        }
        imgColor.a = 1f;
        textColor.a = 1f;
        titleImage.color = imgColor;
        textComp.color = textColor;
    }


    public void LoadNextScene()
    {
        SceneManager.LoadScene("MainScene"); // ���� �� �ε�
    }
}
