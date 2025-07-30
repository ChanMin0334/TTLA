using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        else
        {
            Destroy(gameObject);
        }

        if (mainCamera == null)
            mainCamera = Camera.main;

        // FHD(1920x1080) 비율로 고정 (16:9)
        mainCamera.aspect = 1920f / 1080f;

    }


    void LateUpdate()
    {
        // 타겟이 있으면 따라감
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
    }

    // 카메라 타겟 설정
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}