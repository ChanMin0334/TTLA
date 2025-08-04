using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static GameManager Instance { get; private set; }
    public Player player;
    // ���� ���� �ߴ��� ����
    public bool isPlaying = false;

    // �Ͻ� ���� ����
    // ������,�ɼ� ����,���� ���� �� ���
    public bool isPaused = false;

    public int currentStage = 1; // ���� ����

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �Ҵ� �� �ߺ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start�� ù ������ ������Ʈ ���� ȣ��˴ϴ�.
    void Start()
    {
        Init();
    }

    // Update�� �� �����Ӹ��� ȣ��˴ϴ�.
    void Update()
    {
        if (isPlaying)
        {
            if (isPaused)
            {
                Time.timeScale = 0f; // �Ͻ�����: �ð� ����
            }
            else
            {
                Time.timeScale = 1f; // �Ͻ����� ����: �ð� ���� ����
            }
        }
    }

    private void Init()
    {
         SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ���
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = FindAnyObjectByType<Player>();
    }

    public void GoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void GoGameScene()
    {
        SceneManager.LoadScene("Level1_Forest");
    }
}
