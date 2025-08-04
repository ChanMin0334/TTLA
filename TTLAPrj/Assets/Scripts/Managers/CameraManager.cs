using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static CameraManager Instance { get; private set; }

    // 메인 카메라 참조
    public Camera mainCamera;

    // 카메라 타겟(플레이어 등)
    public Transform target;

    // 카메라 이동 속도
    public float followSpeed = 5f;

    // 카메라 오프셋
    public Vector3 offset = new Vector3(0, 5, -10);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        FindAndSetupCamera();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndSetupCamera();
    }

    private void FindAndSetupCamera()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // FHD(1920x1080) 비율로 고정 (16:9)
            mainCamera.aspect = 1920f / 1080f;
        }
    }

    void LateUpdate()
    {
        // 타겟 따라가는 기능 구현만 해둠
        /*if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }*/
    }

    // 카메라 타겟 설정
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
