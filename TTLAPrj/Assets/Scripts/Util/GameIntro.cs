using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntro : MonoBehaviour
{
    public GameObject introUI; // 인트로 UI 오브젝트

    void Start()
    {
        StartCoroutine(ShowIntroAndLoadNext());
    }

    IEnumerator ShowIntroAndLoadNext()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlaySFX(SFX_Name.SFX_IntroSound);
        introUI.SetActive(true); // 인트로 UI 활성화
        yield return new WaitForSeconds(0.5f); // 인트로 UI 표시 시간
        introUI.SetActive(false); // 인트로 UI 비활성화
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainScene");
    }
}
