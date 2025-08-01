using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameStateUI : MonoBehaviour
{
    [Header("GameState UI")] // �г� �ϳ��� �ϰ� ���ڸ� �ٸ��� �ص� �ɵ�
    [SerializeField] GameObject clearPanel;
    [SerializeField] SpriteRenderer[] clearStars;
    [SerializeField] GameObject overPanel;
    [SerializeField] GameObject mainBtn;
    [SerializeField] GameObject exitBtn;

    public IEnumerator GameClear()
    {
        clearPanel.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(clearPanel.transform.DOScale(Vector3.one * 25, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
        seq.AppendInterval(1.5f);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < clearStars.Length; i++)
        {
            clearStars[i].gameObject.SetActive(true);
            clearStars[i].transform.localScale = Vector3.zero;
            clearStars[i].transform.DOScale(Vector3.one * 10, 0.3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.5f);
        }

        SetBtn();
    }

    public IEnumerator GameOver()
    {
        overPanel.SetActive(true);

        Sequence seq = DOTween.Sequence();

        seq.Append(overPanel.transform.DOScale(Vector3.one * 25, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
        seq.AppendInterval(1.5f);

        yield return new WaitForSeconds(0.5f);

        SetBtn();
    }

    void SetBtn()
    {
        Sequence seq = DOTween.Sequence();

        mainBtn.SetActive(true);
        exitBtn.SetActive(true);

        seq.Append(mainBtn.transform.DOScale(new Vector3(400f, 200f), 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
        seq.Append(exitBtn.transform.DOScale(new Vector3(400f, 200f), 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
    }
}
