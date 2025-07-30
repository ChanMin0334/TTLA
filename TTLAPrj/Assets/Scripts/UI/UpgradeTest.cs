using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeTest : MonoBehaviour
{
    [SerializeField] SpriteRenderer target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
            PlaySuccess();
        if (Input.GetKeyDown(KeyCode.Keypad6))
            PlayFail();
    }
    public void PlaySuccess()
    {
        Sequence seq = DOTween.Sequence();

        target.color = Color.white;

        seq.Append(target.transform.DOShakeScale(0.3f, 0.3f, 10, 90))
            .Join(target.transform.DOScale(1.2f, 0.15f).SetLoops(2, LoopType.Yoyo))
            .Append(target.DOFade(0.5f, 0.2f).SetLoops(2, LoopType.Yoyo));

        seq.OnComplete(() =>
        {
            Debug.Log("강화 연출 끝");
        });
    }

    public void PlayFail()
    {
        Sequence seq = DOTween.Sequence();

        target.color = Color.white;

        seq.Append(target.transform.DOShakeRotation(0.3f, new Vector3(0, 0, 30), 10, 90))
            .Join(target.DOFade(0.3f, 0.2f).SetLoops(2, LoopType.Yoyo))
            .OnComplete(() =>
            {
                Debug.Log("강화 실패");
            });
    }
}
