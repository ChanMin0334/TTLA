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
    [SerializeField] SkillDataBase skillDataBase;
    [SerializeField] SkillCards[] cards;
    [SerializeField] GameObject levelUpPanel;
    SkillCards selectedCard = null;

    [Header("Option UI")]
    [SerializeField] GameObject soundPanel;

    [Header("UI Component")]
    [SerializeField] Player player;
    [SerializeField] UpgradeUI upgradeUI; //upgrade UI 호출용
    [SerializeField] GameStateUI gamestateUI; //gamestate UI 호출용
    [SerializeField] CharacterInfoUI characterInfoUI; // 호출용
    [SerializeField] InventoryUI inventoryUI;

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
            CallGameClear();

        if (Input.GetKeyDown(KeyCode.Keypad4))
            CallGameOver();            
    }

    #region 레벨업쪽
    public void OnCardSelected(SkillCards clickedCard) // 카드 하이라이트 기능
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
        seq.Append(levelUpPanel.transform.DOScale(Vector3.one * 25, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack));
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
        SoundManager.Instance.PlaySFX(SFX_Name.Player_LevelUp);
        ShowlevelUp(() =>
        {
            ShowCard();
        });
    }

    #endregion


    #region 유틸리티? 옵션, 캐릭터 이미지 변경 수정예정
    public void SoundPanelOn()
    {
        soundPanel.SetActive(true);
    }

    public void SoundPanelOff()
    {
        soundPanel.SetActive(false);
    }
    #endregion

    public void CallGameClear() // 게임클리어시 호출
    {
        StartCoroutine(gamestateUI.GameClear());
        SoundManager.Instance.PlaySFX(SFX_Name.Player_LevelUp);
    }

    public void CallGameOver() // 게임 오버시 호출
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