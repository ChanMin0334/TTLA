using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    GameObject currntStage; // ���� ��������
    [SerializeField]
    GameObject nextStage; // ���� ��������
    [SerializeField]
    GameObject portal;

    private void Awake()
    {
        if (currntStage == null || nextStage == null || portal == null)
        {
            Debug.LogError("Portal components are not assigned in the inspector.");
        }
    }
}
