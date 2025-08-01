using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameIntro : MonoBehaviour
{
    public GameObject introText; // 20조 텍스트
    public GameObject introPanel; // 인트로 패널
    public GameObject titleImg; // 타이틀 이미지
    public GameObject titleText; // 타이틀 텍스트
    public float fadeDuration = 10f; // 패널 밝아지는 시간
    public float titleFadeDuration = 5f; // 패널 밝아지는 시간
    public float moveDuration = 1f; // 패널 올라오는 시간
    public float moveDistance = 40f; // 패널이 올라오는 거리 (픽셀 단위)

    private AudioSource audioSource;
    private Image panelImage;
    private RectTransform panelRect;
    private Image titleImage;
    private bool isPanelOn = false; // 패널이 켜져 있는지 여부
    private bool canStart = false; // 인트로 시작 여부

    void Start()
    {
        panelImage = introPanel.GetComponent<Image>();
        panelRect = introPanel.GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        titleImage = titleImg.GetComponent<Image>();
        StartCoroutine(ShowIntroAndLoadNext());
    }

    // 인트로가 끝난 후 유저 입력(키, 마우스 클릭 등)을 받아 다음 씬으로 이동
    void Update()
    {
        if (canStart)
        {
            // 아무 키나 누르거나 마우스 클릭 시
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                canStart = false; // 중복 입력 방지
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

        // 두 코루틴을 동시에 실행
        Coroutine move = StartCoroutine(MovePanel());
        Coroutine fadePanel = StartCoroutine(FadeInPanelRGB());
        Coroutine fadeTitle = StartCoroutine(FadeInTitleImg());

        // 모든 코루틴이 끝날 때까지 대기
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
                isPanelOn = true; // 패널이 켜졌음을 표시
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

        while (!isPanelOn) // 패널이 켜질 때까지 대기
        {
            yield return null;
        }

        canStart = true; // 인트로 시작 가능 상태로 변경

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
        SceneManager.LoadScene("MainScene"); // 다음 씬 로드
    }
}
