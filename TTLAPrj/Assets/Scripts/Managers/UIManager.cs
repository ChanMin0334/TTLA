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
    [SerializeField] SkillDataBase skillDataBase;
    [SerializeField] SkillCards[] cards;
    [SerializeField] GameObject levelUpPanel;
    SkillCards selectedCard = null;

    [Header("Option UI")]
    [SerializeField] GameObject soundPanel;

    [Header("UI Component")]
    [SerializeField] Player player;
    [SerializeField] UpgradeUI upgradeUI; //upgrade UI ȣ���
    [SerializeField] GameStateUI gamestateUI; //gamestate UI ȣ���
    [SerializeField] CharacterInfoUI characterInfoUI; // ȣ���
    [SerializeField] InventoryUI inventoryUI;

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
            CallGameClear();

        if (Input.GetKeyDown(KeyCode.Keypad4))
            CallGameOver();            
    }

    #region ��������
    public void OnCardSelected(SkillCards clickedCard) // ī�� ���̶���Ʈ ���
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
        seq.Append(levelUpPanel.transform.DOScale(Vector3.one * 25, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
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
        SoundManager.Instance.PlaySFX(SFX_Name.Player_LevelUp);
        ShowlevelUp(() =>
        {
            ShowCard();
        });
    }

    #endregion


    #region ��ƿ��Ƽ? �ɼ�, ĳ���� �̹��� ���� ��������
    public void SoundPanelOn()
    {
        soundPanel.SetActive(true);
    }

    public void SoundPanelOff()
    {
        soundPanel.SetActive(false);
    }
    #endregion

    public void CallGameClear() // ����Ŭ����� ȣ��
    {
        StartCoroutine(gamestateUI.GameClear());
        SoundManager.Instance.PlaySFX(SFX_Name.Player_LevelUp);
    }

    public void CallGameOver() // ���� ������ ȣ��
    {
        StartCoroutine(gamestateUI.GameOver());
        SoundManager.Instance.PlaySFX(SFX_Name.Player_LevelUp);
    }

    public void CallUpdateUI()
    {
        if(upgradeUI == null)
        {
            Debug.LogWarning("upgradeUI is Null");
            return;
        }

        upgradeUI.UpdateUpgradeUI();
    }

    public void CallUpgradeSuccess()
    {
        if (upgradeUI == null)
        {
            Debug.LogWarning("upgradeUI is Null");
            return;
        }

        upgradeUI.UpgradeSuccess();
    }

    public void CallUpgradeFail()
    {
        if (upgradeUI == null)
        {
            Debug.LogWarning("upgradeUI is Null");
            return;
        }

        upgradeUI.UpgradeFail();
    }
    public void CallShowCharacterInfo()
    {
        characterInfoUI.ShowCharacterInfo();
    }

    public void CallInventoryReDraw()
    {
        inventoryUI.InventoryReDraw();
    }


}