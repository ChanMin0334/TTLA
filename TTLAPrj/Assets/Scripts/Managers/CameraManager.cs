using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static CameraManager Instance { get; private set; }

    // ���� ī�޶� ����
    public Camera mainCamera;

    // ī�޶� Ÿ��(�÷��̾� ��)
    public Transform target;

    // ī�޶� �̵� �ӵ�
    public float followSpeed = 5f;

    // ī�޶� ������
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

        // FHD(1920x1080) ������ ���� (16:9)
        mainCamera.aspect = 1920f / 1080f;

    }


    void LateUpdate()
    {
        // Ÿ���� ������ ����
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
    }

    // ī�޶� Ÿ�� ����
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}