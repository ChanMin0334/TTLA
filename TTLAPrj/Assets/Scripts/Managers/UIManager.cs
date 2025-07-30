using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static UIManager Instance { get; private set; }

    [SerializeField] AbilitySo abilitySO;
    [SerializeField] AbilityCards[] cards;

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

    private void Start()
    {
        foreach(var card in cards)
        {
            card.SetManager(this);
        }
    }

    public void OnCardSelected(AbilityCards clickedCard)
    {
        if(selectedCard != null && selectedCard != clickedCard)
        {
            selectedCard.DeSelect();
        }

        selectedCard = clickedCard;
        selectedCard.Select();
    }



#if UNITY_EDITOR
    private void Update()
    {
        AbilityCardTest();
    }

    void AbilityCardTest()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(abilitySO != null && abilitySO.abilities.Length > 0)
            {
                int maxCount = Mathf.Min(cards.Length, abilitySO.abilities.Length);
                List<int> usedIndices = new List<int>();
                for(int i = 0; i < maxCount; i++)
                {
                    int rand;
                    do
                    {
                        rand = Random.Range(0, abilitySO.abilities.Length);
                    }while (usedIndices.Contains(rand));
                    usedIndices.Add(rand);

                    Ability randomAbility = abilitySO.abilities[rand];
                    cards[i].SetAbility(randomAbility);
                    cards[i].ShowIn();
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            foreach(var card in cards)
            {
                card.Showout();
            }

            selectedCard = null;
        }
    }
#endif


    // UI 패널 표시
    // public void ShowPanel(GameObject panel)
    // {
    //     panel.SetActive(true);
    // }

    // UI 패널 숨김
    // public void HidePanel(GameObject panel)
    // {
    //     panel.SetActive(false);
    // }

    // 모든 패널 숨기기 예시
    // public void HideAllPanels()
    // {
    //     mainMenuPanel.SetActive(false);
    //     pausePanel.SetActive(false);
    //     gameOverPanel.SetActive(false);
    // }
}
