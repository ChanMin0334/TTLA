using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static UIManager Instance { get; private set; }

    [Header("Ability관련 UI")]
    [SerializeField] AbilitySo abilitySO;
    [SerializeField] AbilityCards[] cards;
    [SerializeField] GameObject levelUpBox;
    AbilityCards selectedCard = null;

    // UI 패널 예시 (필요에 따라 추가)
    // public GameObject mainMenuPanel;
    // public GameObject pausePanel;
    // public GameObject gameOverPanel;

    private void Awake()
    {
        // 싱글톤 인스턴스 할당 및 중복 방지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2))
            levelUpUI();
    }

    #region 레벨업쪽 UI
    public void OnCardSelected(AbilityCards clickedCard) // 카드 하이라이트 기능
    {
        if (selectedCard != null && selectedCard != clickedCard) //선택한 카드 존재 && 기존카드 != 선택카드
        {
            selectedCard.DeSelect(); //기존카드 originalColor
        }

        selectedCard = clickedCard;
        selectedCard.Select();

        Debug.Log("선택된 카드: " + selectedCard.GetAbilityID());

        foreach (var card in cards)
        {
            card.Showout();
        }
    }

    void ShowCard()
    {
        if (abilitySO != null && abilitySO.abilities.Length > 0)
        {
            int maxCount = Mathf.Min(cards.Length, abilitySO.abilities.Length);
            List<int> Noduplication = new List<int>(); //중복 방지 
            for (int i = 0; i < maxCount; i++)
            {
                int rand;
                do
                {
                    rand = Random.Range(0, abilitySO.abilities.Length);
                } while (Noduplication.Contains(rand));
                Noduplication.Add(rand);

                Ability randomAbility = abilitySO.abilities[rand];
                cards[i].SetAbility(randomAbility);
                cards[i].ShowIn();
            }
        }
    }
    void ShowlevelUp(System.Action onComplete)
    {
        levelUpBox.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(levelUpBox.transform.DOScale(Vector3.one * 15, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
        seq.AppendInterval(1.5f);
        seq.Append(levelUpBox.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack));
        seq.OnComplete(() =>
        {
            levelUpBox.SetActive(false);
            onComplete?.Invoke();
        });
    }
    void levelUpUI() //ShowLevelUp 끝난후 ShowCard 호출
    {
        ShowlevelUp(() =>
        {
            ShowCard();
        });
    }
    #endregion

}


