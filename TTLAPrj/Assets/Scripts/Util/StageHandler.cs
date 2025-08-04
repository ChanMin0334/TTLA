using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] stages;

    private GameObject portal;
    private GameObject[] spawners; // ���� �����ʸ� �迭�� ����
    private bool isCleared = false; // �������� Ŭ���� ����
    private bool isReward = false; // UIȣ���
    public float stageMoveDistance = 10f; // ���������� �̵��� �Ÿ�
    public float stageMoveDuration = 1f;  // �̵� �ִϸ��̼� �ð�(��)

    private void Awake()
    {
        if (stages.Length == 0)
        {
            Debug.LogError("���������� �������� �ʾҽ��ϴ�!");
        }
    }
    void Start()
    {
        InitializeStages();
        ActivateCurrentStage();
        SpawnMonstersOnMapLoaded(); // �� �ε� �Ϸ� �� ���� ����
    }
    private void Update()
    {
        if (!isCleared && AllSpawnersCleared())
        {
            isCleared = true;

            if (!isReward && isCleared)
            {
                isReward = true;
                UIManager.Instance.levelUpUI();
                GameManager.Instance.isPaused = true;
            }
            //ȣ��
            //����
            SetPortalType(); // ��Ż Ÿ�� ����
        }
    }

    // ��� �������� �ڽ��� 0������ Ȯ��
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
            stage.SetActive(false); // ��� �������� ��Ȱ��ȭ
        }
    }
    void ActivateCurrentStage()
    {
        int currentStageIndex = GameManager.Instance.currentStage - 1; // 0���� �����ϴ� �ε���
        if (currentStageIndex >= 0 && currentStageIndex < stages.Length)
        {
            stages[currentStageIndex].SetActive(true); // ���� �������� Ȱ��ȭ
            portal = stages[currentStageIndex].transform.Find("Portal").gameObject; // ��Ż ������Ʈ ã��

            // ���� �����ʸ� �迭�� �Ҵ�
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
            Debug.LogError("���� �������� �ε����� ������ ������ϴ�!");
        }
    }

    // �� �ε尡 �Ϸ�Ǿ��� �� �����ʿ��� ���͸� ����
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
            GameManager.Instance.SaveStage?.Invoke(); // �������� ���� ȣ��
        }
        else
        {
            Debug.Log("��� ���������� Ŭ�����߽��ϴ�!");
            GameManager.Instance.DeleteStage?.Invoke(); // �������� ���� ȣ��
            // UI�� ������ ��ư
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


        // �ε巴�� �̵�
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
        GameManager.Instance.playerObj.transform.position = new Vector3(0f, -4f, 0); // �÷��̾� ��ġ �̵�
        GameManager.Instance.playerObj.SetActive(true); // �÷��̾� Ȱ��ȭ
        SpawnMonstersOnMapLoaded(); // ���� �������� �� �ε� �� ���� ����
        isCleared = false;
        isReward = false;
    }


    private void SetPortalType()
    {
        var collider = portal.GetComponent<BoxCollider2D>();
        collider.isTrigger = true; // ��Ż Ȱ��ȭ
        portal.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 255f, 0.4f);

        // PortalTriggerHandler�� ������ �߰�
        if (portal.GetComponent<PortalTriggerHandler>() == null)
        {
            portal.AddComponent<PortalTriggerHandler>().stageHandler = this;
        }
    }

    private void ShuffleStagesExceptLast() //Randomness 추가?
    {
        int n = stages.Length - 1; 
        for (int i = 0; i < n; i++)
        {
            int rand = Random.Range(i, n); 
            GameObject temp = stages[i];
            stages[i] = stages[rand];
            stages[rand] = temp;
        }
    }

    // ��Ż Ʈ���� �̺�Ʈ ó���� Ŭ����
    public class PortalTriggerHandler : MonoBehaviour
    {
        [HideInInspector]
        public StageHandler stageHandler;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.playerObj.SetActive(false); // �÷��̾� ��Ȱ��ȭ
                stageHandler.LoadNextStage();
            }
        }
    }
}

