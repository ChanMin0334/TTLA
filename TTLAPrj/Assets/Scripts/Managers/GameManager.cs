using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }
    public Player player;
    // 게임 진입 했는지 여부
    public bool isPlaying = false;

    // 일시 정지 여부
    // 레벨업,옵션 열기,게임 종료 시 사용
    public bool isPaused = false;

    public int currentStage = 1; // 현재 레벨

    private void Awake()
    {
        // 싱글톤 인스턴스 할당 및 중복 방지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴 방지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start는 첫 프레임 업데이트 전에 호출됩니다.
    void Start()
    {
        Init();
    }

    // Update는 매 프레임마다 호출됩니다.
    void Update()
    {
        if (isPlaying)
        {
            if (isPaused)
            {
                Time.timeScale = 0f; // 일시정지: 시간 멈춤
            }
            else
            {
                Time.timeScale = 1f; // 일시정지 해제: 시간 정상 진행
            }
        }
    }

    private void Init()
    {
         SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 등록
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
