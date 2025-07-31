using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//�� �ڵ�� �÷��̾� ���� �׽�Ʈ�� ��ũ��Ʈ�Դϴ�.
//���սÿ� Player ��ũ��Ʈ�� ��ġ�� ���� ��õ�մϴ�.
public class PlayerLevel_test : MonoBehaviour
{
    // �÷��̾� ����
    public int playerLevel = 1;

    // Ŭ���� �� ���������� ������ 2�� �Ǹ� ������
    public int expCount = 0;

    //���������� Ŭ���� �� �ǝ�Ǵ� �޼���
    public void StageCleared()
    {
        // �������� Ŭ���� �� ����ġ ȹ��
        expCount++;
        // ����ġ�� 2�� �Ǹ� ������
        if (expCount >= 2)
        {
            LevelUp();
            expCount = 0; // ����ġ �ʱ�ȭ
        }
    }

    public void LevelUp()
    {
        playerLevel++;

        // ������ ���� ���
        SoundManager.Instance.PlaySFX(SFX_Name.Player_LevelUp, 0.5f);

         // �÷��̾����� ������ ����ֱ�

        // �÷��̾� ü�� 10% ȸ��

        Debug.Log("�÷��̾� ������ " + playerLevel + "�� ����߽��ϴ�!");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
