using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static UIManager Instance { get; private set; }

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

    // UI �г� ǥ��
    // public void ShowPanel(GameObject panel)
    // {
    //     panel.SetActive(true);
    // }

    // UI �г� ����
    // public void HidePanel(GameObject panel)
    // {
    //     panel.SetActive(false);
    // }

    // ��� �г� ����� ����
    // public void HideAllPanels()
    // {
    //     mainMenuPanel.SetActive(false);
    //     pausePanel.SetActive(false);
    //     gameOverPanel.SetActive(false);
    // }
}
