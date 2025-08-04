using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] stages;

    private GameObject portal;
    private GameObject[] spawners; // 여러 스포너를 배열로 관리
    private bool isCleared = false; // 스테이지 클리어 여부
    private bool isReward = false; // UI호출용
    public float stageMoveDistance = 10f; // 스테이지가 이동할 거리
    public float stageMoveDuration = 1f;  // 이동 애니메이션 시간(초)

    private void Awake()
    {
        if (stages.Length == 0)
        {
            Debug.LogError("스테이지가 설정되지 않았습니다!");
        }
    }
    void Start()
    {
        InitializeStages();
        ActivateCurrentStage();
        SpawnMonstersOnMapLoaded(); // 맵 로드 완료 시 몬스터 스폰
    }
    private void Update()
    {
        if (!isCleared && AllSpawnersCleared())
        {
            isCleared = true;

            if(!isReward && isCleared)
            {
                isReward = true;
                UIManager.Instance.levelUpUI();
                GameManager.Instance.isPaused = true;
            }
            //호출
            //정지
            SetPortalType(); // 포탈 타입 설정
        }
    }

    // 모든 스포너의 자식이 0개인지 확인
    private bool AllSpawnersCleared()
    {
        if (spawners == null || spawners.Length == 0)
            return false;

        foreach (var spawner in spawners)
        {
            if (spawner != null && spawner.transform.childCount > 0)
                return false;
        }
        return true;
    }

    void InitializeStages()
    {
        foreach (GameObject stage in stages)
        {
            stage.SetActive(false); // 모든 스테이지 비활성화
        }
    }
    void ActivateCurrentStage()
    {
        int currentStageIndex = GameManager.Instance.currentStage - 1; // 0부터 시작하는 인덱스
        if (currentStageIndex >= 0 && currentStageIndex < stages.Length)
        {
            stages[currentStageIndex].SetActive(true); // 현재 스테이지 활성화
            portal = stages[currentStageIndex].transform.Find("Portal").gameObject; // 포탈 오브젝트 찾기

            // 여러 스포너를 배열로 할당
            var spawnerList = new List<GameObject>();
            foreach (Transform child in stages[currentStageIndex].transform)
            {
                if (child.name.Contains("SpawnArea"))
                {
                    spawnerList.Add(child.gameObject);
                }
            }
            spawners = spawnerList.ToArray();
        }
        else
        {
            Debug.LogError("현재 스테이지 인덱스가 범위를 벗어났습니다!");
        }
    }

    // 맵 로드가 완료되었을 때 스포너에서 몬스터를 스폰
    public void SpawnMonstersOnMapLoaded()
    {
        if (spawners == null || spawners.Length == 0)
            return;

        foreach (var spawner in spawners)
        {
            if (spawner != null)
            {
                var monsterSpawner = spawner.GetComponent<MonsterSpawner>();
                if (monsterSpawner != null)
                {
                    monsterSpawner.SpawnMonsters();
                }
            }
        }
    }

    public void LoadNextStage()
    {
        int nextStageIndex = GameManager.Instance.currentStage;
        if (nextStageIndex < stages.Length)
        {    
            StartCoroutine(MoveStagesAndLoad(nextStageIndex));
        }
        else
        {
            Debug.Log("모든 스테이지를 클리어했습니다!");
        }
    }

    private IEnumerator MoveStagesAndLoad(int nextStageIndex)
    {
        int currentStageIndex = GameManager.Instance.currentStage - 1;
        GameObject currentStage = (currentStageIndex >= 0 && currentStageIndex < stages.Length) ? stages[currentStageIndex] : null;
        GameObject nextStage = (nextStageIndex >= 0 && nextStageIndex < stages.Length) ? stages[nextStageIndex] : null;

        Vector3 currentStart = currentStage != null ? currentStage.transform.position : Vector3.zero;
        Vector3 nextStart = nextStage != null ? nextStage.transform.position : Vector3.zero;
        Vector3 currentEnd = currentStart + Vector3.down * stageMoveDistance;
        Vector3 nextEnd = nextStart + Vector3.down * stageMoveDistance;

        float elapsed = 0f;
        if (nextStage != null) nextStage.SetActive(true);


        // 부드럽게 이동
        while (elapsed < stageMoveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / stageMoveDuration);

            if (currentStage != null)
                currentStage.transform.position = Vector3.Lerp(currentStart, currentEnd, t);
            if (nextStage != null)
                nextStage.transform.position = Vector3.Lerp(nextStart, nextEnd, t);

            yield return null;
        }
        if (currentStage != null) currentStage.transform.position = currentEnd;
        if (nextStage != null) nextStage.transform.position = nextEnd;

        GameManager.Instance.currentStage++;
        InitializeStages();
        ActivateCurrentStage();
        GameManager.Instance.playerObj.transform.position = new Vector3(0f, -4f, 0); // 플레이어 위치 이동
        GameManager.Instance.playerObj.SetActive(true); // 플레이어 활성화
        SpawnMonstersOnMapLoaded(); // 다음 스테이지 맵 로드 후 몬스터 스폰
        isCleared = false;
        isReward = false;
    }


    private void SetPortalType()
    {
        var collider = portal.GetComponent<BoxCollider2D>();
        collider.isTrigger = true; // 포탈 활성화
        portal.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 255f, 0.4f);

        // PortalTriggerHandler가 없으면 추가
        if (portal.GetComponent<PortalTriggerHandler>() == null)
        {
            portal.AddComponent<PortalTriggerHandler>().stageHandler = this;
        }
    }

    // 포탈 트리거 이벤트 처리용 클래스
    public class PortalTriggerHandler : MonoBehaviour
    {
        [HideInInspector]
        public StageHandler stageHandler;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.playerObj.SetActive(false); // 플레이어 비활성화
                stageHandler.LoadNextStage();
            }
        }
    }
}

