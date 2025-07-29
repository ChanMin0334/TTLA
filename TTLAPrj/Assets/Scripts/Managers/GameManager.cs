using System;
using UnityEngine;

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
        throw new NotImplementedException();
    }

}
