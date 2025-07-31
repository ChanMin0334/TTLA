using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//이 코드는 플레이어 레벨 테스트용 스크립트입니다.
//병합시에 Player 스크립트와 합치는 것을 추천합니다.
public class PlayerLevel_test : MonoBehaviour
{
    // 플레이어 레벨
    public int playerLevel = 1;

    // 클리어 한 스테이지의 갯수가 2가 되면 레벨업
    public int expCount = 0;

    //스테이지가 클리어 시 실앻되는 메서드
    public void StageCleared()
    {
        // 스테이지 클리어 시 경험치 획득
        expCount++;
        // 경험치가 2가 되면 레벨업
        if (expCount >= 2)
        {
            LevelUp();
            expCount = 0; // 경험치 초기화
        }
    }

    public void LevelUp()
    {
        playerLevel++;

        // 레벨업 사운드 재생
        SoundManager.Instance.PlaySFX(SFX_Name.Player_LevelUp, 0.5f);

         // 플레이어한테 선택지 띄워주기

        // 플레이어 체력 10% 회복

        Debug.Log("플레이어 레벨이 " + playerLevel + "로 상승했습니다!");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
