using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class BossSpawner : MonoBehaviour
{
    [Header("보스 설정")]
    public GameObject bossPrefab;
    public Transform spawnPoint;

    [Header("연출 관련")]
    public CanvasGroup blackScreen;
    public AudioSource boomSound;

    [Header("카메라 줌 설정")]
    public float zoomSize = 2.5f;
    public float zoomDuration = 0.5f;
    private float originalSize;

    private void OnEnable()
    {
        SpawnMonsters();
    }

    public void SpawnMonsters()
    {
        StartCoroutine(BossIntro());
    }

    IEnumerator BossIntro()
    {
        // Step 1: 화면 어둡게
        yield return FadeInBlack(0.5f);

        // Step 2: 카메라 줌인
        yield return ZoomCamera(zoomSize, zoomDuration);

        // Step 3: 사운드 재생
        boomSound?.Play();
        yield return new WaitForSeconds(1.5f);

        // Step 4: 보스 생성
        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        // 부모로 설정하되, 월드 좌표/스케일 유지
        boss.transform.SetParent(this.transform, true);
        // Step 5: 암전 해제 + 줌 아웃
        yield return FadeOutBlack(0.5f);
        yield return ZoomCamera(originalSize, zoomDuration);

        // Step 6: ?초 후 보스 활성화
        yield return new WaitForSeconds(1.0f);
        boss.GetComponent<MonsterBoss>()?.ActivateBoss();
    }

    IEnumerator FadeInBlack(float duration)
    {
        blackScreen.alpha = 0f;
        blackScreen.gameObject.SetActive(true);
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }
        blackScreen.alpha = 1f;
    }

    IEnumerator FadeOutBlack(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(1f, 0f, t / duration);
            yield return null;
        }
        blackScreen.alpha = 0f;
        blackScreen.gameObject.SetActive(false);
    }

    IEnumerator ZoomCamera(float targetSize, float duration)
    {
        Camera cam = Camera.main;
        if (cam == null || !cam.orthographic)
            yield break;

        if (originalSize == 0f)
            originalSize = cam.orthographicSize;

        float startSize = cam.orthographicSize;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            cam.orthographicSize = Mathf.Lerp(startSize, targetSize, t / duration);
            yield return null;
        }

        cam.orthographicSize = targetSize;
    }
}


