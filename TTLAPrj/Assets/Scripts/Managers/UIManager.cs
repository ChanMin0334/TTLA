using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static UIManager Instance { get; private set; }

    [Header("Ability���� UI")]
    [SerializeField] AbilitySo abilitySO;
    [SerializeField] AbilityCards[] cards;
    [SerializeField] GameObject levelUpBox;
    AbilityCards selectedCard = null;

    // UI �г� ���� (�ʿ信 ���� �߰�)
    // public GameObject mainMenuPanel;
    // public GameObject pausePanel;
    // public GameObject gameOverPanel;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �Ҵ� �� �ߺ� ����
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

    #region �������� UI
    public void OnCardSelected(AbilityCards clickedCard) // ī�� ���̶���Ʈ ���
    {
        if (selectedCard != null && selectedCard != clickedCard) //������ ī�� ���� && ����ī�� != ����ī��
        {
            selectedCard.DeSelect(); //����ī�� originalColor
        }

        selectedCard = clickedCard;
        selectedCard.Select();

        Debug.Log("���õ� ī��: " + selectedCard.GetAbilityID());

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
            List<int> Noduplication = new List<int>(); //�ߺ� ���� 
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
    void levelUpUI() //ShowLevelUp ������ ShowCard ȣ��
    {
        ShowlevelUp(() =>
        {
            ShowCard();
        });
    }
    #endregion

}


