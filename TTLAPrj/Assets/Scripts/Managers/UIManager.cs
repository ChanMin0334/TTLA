using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static UIManager Instance { get; private set; }

    [Header("Ability관련 UI")]
    //[SerializeField] AbilitySo abilitySO;
    [SerializeField] SkillDataBase skillDataBase;
    [SerializeField] SkillCards[] cards;
    [SerializeField] GameObject levelUpPanel;
    SkillCards selectedCard = null;

    [Header("Option UI")]
    [SerializeField] GameObject soundPanel;

    [Header("GameState UI")] // 패널 하나로 하고 글자만 다르게 해도 될듯
    [SerializeField] GameObject clearPanel;
    [SerializeField] SpriteRenderer[] clearStars;
    [SerializeField] GameObject overPanel;
    [SerializeField] GameObject mainBtn;
    [SerializeField] GameObject exitBtn;

    [Header("Utility UI")]
    [SerializeField] Image character;
    [SerializeField] Sprite[] otherCharacters;
    int characterIndex;

    //임시용
    [SerializeField] Player player;
    
    //[SerializeField] GameObject gameOverPanel;

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

        if (Input.GetKeyDown(KeyCode.Keypad3))
            StartCoroutine(GameClear());

        if(Input.GetKeyDown(KeyCode.Keypad4))
            StartCoroutine(GameOver());
    }

    #region 레벨업쪽 UI
    public void OnCardSelected(SkillCards clickedCard) // 카드 하이라이트 기능a
    {
        if (selectedCard != null && selectedCard != clickedCard) //선택한 카드 존재 && 기존카드 != 선택카드
        {
            selectedCard.DeSelect(); //기존카드 originalColor
        }

        selectedCard = clickedCard;
        selectedCard.Select();

        Debug.Log("선택된 카드: " + selectedCard.GetSkillID());

        Debug.Log($"플레이어의 카드선택전 공격력 : {player.Stats.Atk}");
        Debug.Log($"플레이어의 카드선택전 공격속도 {player.Stats.AtkSpeed}");

        selectedCard.currentSkill.ApplySkill(player);

        foreach (var card in cards)
        {
            card.Showout();
        }

        Debug.Log($"플레이어의 카드선택후 공격력 : {player.Stats.Atk}");
        Debug.Log($"플레이어의 카드선택후 공격속도 {player.Stats.AtkSpeed}");

    }

    void ShowCard()
    {
        if (skillDataBase != null && skillDataBase.skills.Length > 0)
        {
            int maxCount = Mathf.Min(cards.Length, skillDataBase.skills.Length);
            List<int> Noduplication = new List<int>(); //중복 방지 
            for (int i = 0; i < maxCount; i++)
            {
                int rand;
                do
                {
                    rand = Random.Range(0, skillDataBase.skills.Length);
                } while (Noduplication.Contains(rand));
                Noduplication.Add(rand);

                Skill randomSkill = skillDataBase.skills[rand];
                cards[i].SetSkill(randomSkill);
                cards[i].ShowIn();
            }
        }
    }
    void ShowlevelUp(System.Action onComplete)
    {
        levelUpPanel.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(levelUpPanel.transform.DOScale(Vector3.one * 15, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
        seq.AppendInterval(1.5f);
        seq.Append(levelUpPanel.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack));
        seq.OnComplete(() =>
        {
            levelUpPanel.SetActive(false);
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

    #region 유틸리티? 옵션, 캐릭터 이미지 변경
    public void SoundPanelOn()
    {
        soundPanel.SetActive(true);
    }

    public void SoundPanelOff()
    {
        soundPanel.SetActive(false);
    }

    public void ChangeSprite(int index)
    {
        characterIndex += index;

        if (characterIndex < 0)
            characterIndex = otherCharacters.Length - 1;
        else if (characterIndex >= otherCharacters.Length)
            characterIndex = 0;

        character.DOFade(0f, 0.2f).OnComplete(() =>
        {
            character.sprite = otherCharacters[characterIndex];
            character.DOFade(1f, 0.2f);
        });
    }

    public void OnLeftButton() => ChangeSprite(-1);
    public void OnRightButton() => ChangeSprite(1);

    #endregion

    #region 게임오버 / 게임 클리어

    IEnumerator GameClear()
    {
        clearPanel.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(clearPanel.transform.DOScale(Vector3.one * 15, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
        seq.AppendInterval(1.5f);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < clearStars.Length; i++)
        {
            clearStars[i].gameObject.SetActive(true);
            clearStars[i].transform.localScale = Vector3.zero;
            clearStars[i].transform.DOScale(0.06f, 0.3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.5f);
        }

        SetBtn();
    }

    IEnumerator GameOver()
    {
        overPanel.SetActive(true);

        Sequence seq = DOTween.Sequence();

        seq.Append(overPanel.transform.DOScale(Vector3.one * 15, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
        seq.AppendInterval(1.5f);

        yield return new WaitForSeconds(0.5f);

        SetBtn();
    }

    void SetBtn()
    {
        Sequence seq = DOTween.Sequence();

        mainBtn.SetActive(true);
        exitBtn.SetActive(true);

        seq.Append(mainBtn.transform.DOScale(new Vector3(10.5f, 4.5f), 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
        seq.Append(exitBtn.transform.DOScale(new Vector3(10.5f, 4.5f), 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
    }
    #endregion
}


