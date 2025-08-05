using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static GameManager Instance { get; private set; }
    public Player player;
    //[HideInInspector]
    public GameObject playerObj;
    // ���� ���� �ߴ��� ���� (��� ����)
    public bool isPlaying = false;

    // �Ͻ� ���� ����
    // ������,�ɼ� ����,���� ���� �� ���
    public bool isPaused = false;

    public int currentStage = 1; // ���� ����

    public Action SaveStage; // ��������(�ֹ߼�) ���� �׼�
    public Action DeleteStage; // �������� ������ ����
    public Action SaveGame; // ���� ���� �׼�

    public SaveManager saveManager; // SaveManager �ν��Ͻ�
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
        SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ���
    }

    // Start�� ù ������ ������Ʈ ���� ȣ��˴ϴ�.
    void Start()
    {
        Init();
    }

    // Update�� �� �����Ӹ��� ȣ��˴ϴ�.
    void Update()
    {
        if (isPaused)
        {
            player.canMove = false; // �Ͻ� ���� ���¿����� �÷��̾� �̵� �Ұ�
        }
        else
        {
            if (!isPlaying) player.canMove = true; // �Ͻ� ���� ���� �� �÷��̾� �̵� ����
        }
    }

    private void Init()
    {
        SaveStage = saveManager.SaveStage; // SaveGame �׼ǿ� SaveManager�� SaveGame �޼��� ���
        DeleteStage += saveManager.DeleteStage; // DeleteStage �׼ǿ� SaveManager�� DeleteStage �޼��� ���
        SaveGame += saveManager.SaveGame; // SaveGame �׼ǿ� SaveManager�� SaveGame �޼��� ���
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = FindAnyObjectByType<Player>();
        playerObj = player.gameObject;
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
