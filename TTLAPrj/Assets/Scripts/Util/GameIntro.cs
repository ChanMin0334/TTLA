using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntro : MonoBehaviour
{
    public GameObject introUI; // ��Ʈ�� UI ������Ʈ

    void Start()
    {
        StartCoroutine(ShowIntroAndLoadNext());
    }

    IEnumerator ShowIntroAndLoadNext()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlaySFX(SFX_Name.SFX_IntroSound);
        introUI.SetActive(true); // ��Ʈ�� UI Ȱ��ȭ
        yield return new WaitForSeconds(0.5f); // ��Ʈ�� UI ǥ�� �ð�
        introUI.SetActive(false); // ��Ʈ�� UI ��Ȱ��ȭ
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainScene");
    }
}
