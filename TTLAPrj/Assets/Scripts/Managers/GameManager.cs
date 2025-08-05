using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }
    public Player player;
    //[HideInInspector]
    public GameObject playerObj;
    // 게임 진입 했는지 여부 (사용 안함)
    public bool isPlaying = false;

    // 일시 정지 여부
    // 레벨업,옵션 열기,게임 종료 시 사용
    public bool isPaused = false;

    public int currentStage = 1; // 현재 레벨

    public Action SaveStage; // 스테이지(휘발성) 저장 액션
    public Action DeleteStage; // 스테이지 끝나면 삭제
    public Action SaveGame; // 게임 저장 액션

    public SaveManager saveManager; // SaveManager 인스턴스
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
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 등록
    }

    // Start는 첫 프레임 업데이트 전에 호출됩니다.
    void Start()
    {
        Init();
    }

    // Update는 매 프레임마다 호출됩니다.
    void Update()
    {
        if (isPaused)
        {
            player.canMove = false; // 일시 정지 상태에서는 플레이어 이동 불가
        }
        else
        {
            if (!isPlaying) player.canMove = true; // 일시 정지 해제 시 플레이어 이동 가능
        }
    }

    private void Init()
    {
        SaveStage = saveManager.SaveStage; // SaveGame 액션에 SaveManager의 SaveGame 메서드 등록
        DeleteStage += saveManager.DeleteStage; // DeleteStage 액션에 SaveManager의 DeleteStage 메서드 등록
        SaveGame += saveManager.SaveGame; // SaveGame 액션에 SaveManager의 SaveGame 메서드 등록
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
