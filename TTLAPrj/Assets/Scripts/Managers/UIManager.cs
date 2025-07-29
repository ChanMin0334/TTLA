using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static UIManager Instance { get; private set; }

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
