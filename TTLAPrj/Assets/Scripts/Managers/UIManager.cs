using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static UIManager Instance { get; private set; }

    [Header("Ability���� UI")]
    //[SerializeField] AbilitySo abilitySO;
    [SerializeField] SkillDataBase skillDataBase;
    [SerializeField] SkillCards[] cards;
    [SerializeField] GameObject levelUpPanel;
    SkillCards selectedCard = null;

    [Header("Option UI")]
    [SerializeField] GameObject soundPanel;

    [Header("GameState UI")] // �г� �ϳ��� �ϰ� ���ڸ� �ٸ��� �ص� �ɵ�
    [SerializeField] GameObject clearPanel;
    [SerializeField] SpriteRenderer[] clearStars;
    [SerializeField] GameObject overPanel;
    [SerializeField] GameObject mainBtn;
    [SerializeField] GameObject exitBtn;

    [Header("Utility UI")]
    [SerializeField] Image character;
    [SerializeField] Sprite[] otherCharacters;
    int characterIndex;

    //�ӽÿ�
    [SerializeField] Player player;
    
    //[SerializeField] GameObject gameOverPanel;

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

        if (Input.GetKeyDown(KeyCode.Keypad3))
            StartCoroutine(GameClear());

        if(Input.GetKeyDown(KeyCode.Keypad4))
            StartCoroutine(GameOver());
    }

    #region �������� UI
    public void OnCardSelected(SkillCards clickedCard) // ī�� ���̶���Ʈ ���a
    {
        if (selectedCard != null && selectedCard != clickedCard) //������ ī�� ���� && ����ī�� != ����ī��
        {
            selectedCard.DeSelect(); //����ī�� originalColor
        }

        selectedCard = clickedCard;
        selectedCard.Select();

        Debug.Log("���õ� ī��: " + selectedCard.GetSkillID());

        Debug.Log($"�÷��̾��� ī�弱���� ���ݷ� : {player.Stats.Atk}");
        Debug.Log($"�÷��̾��� ī�弱���� ���ݼӵ� {player.Stats.AtkSpeed}");

        selectedCard.currentSkill.ApplySkill(player);

        foreach (var card in cards)
        {
            card.Showout();
        }

        Debug.Log($"�÷��̾��� ī�弱���� ���ݷ� : {player.Stats.Atk}");
        Debug.Log($"�÷��̾��� ī�弱���� ���ݼӵ� {player.Stats.AtkSpeed}");

    }

    void ShowCard()
    {
        if (skillDataBase != null && skillDataBase.skills.Length > 0)
        {
            int maxCount = Mathf.Min(cards.Length, skillDataBase.skills.Length);
            List<int> Noduplication = new List<int>(); //�ߺ� ���� 
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
    void levelUpUI() //ShowLevelUp ������ ShowCard ȣ��
    {
        ShowlevelUp(() =>
        {
            ShowCard();
        });
    }
    #endregion

    #region ��ƿ��Ƽ? �ɼ�, ĳ���� �̹��� ����
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

    #region ���ӿ��� / ���� Ŭ����

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


