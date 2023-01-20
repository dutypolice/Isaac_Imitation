using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    // 필요 컴포넌트들
    // 방에 달려있는 문
    public DoorColider[] doors;
    // 방에 소환된 파리 리스트
    List<Fly> flies = new List<Fly>();
    // 방의 카메라 컴포넌트
    public CinemachineVirtualCamera currentRoomCam;


    public bool clear = false;
    // 현재 방의 몬스터 수
    int monstersInRoom = 0;
    // 방의 활성화 여부
    bool active = false;



    void Start()
    {   // 게임 시작 시 방의 몬스터 수가 0 이하이면 모든 방 문 열기
        if (monstersInRoom <= 0)
        {
            clear = true;
            OpenAllDoors(false);
        }
    }



    // 방 내부의 모든 파리의 플레이어 추적, 정지 함수
    public void StartChasingPlayer(bool chase)
    {
        for (int i = 0; i < flies.Count; i++)
            flies[i].StartMoveToTarget(chase);
    }



    // 카메라 이동 함수
    public void OnVirtualCamera()
    {   
        // 스크립트를 가진 방의 가상 카메라를 껐다 켜서 카메라가 이 방을 비추도록 함
        currentRoomCam.enabled = false;
        currentRoomCam.enabled = true;
    }



    // 몬스터 사망 시 처리 함수
    public void OnMonsterDead()
    {
        // 방 내부의 몬스터 수 -1
        monstersInRoom -- ;

        //  만약 모든 몬스터가 사망하면 문 열기
        if (monstersInRoom <= 0)
        {
            clear = true;
            OpenAllDoors(false);
        }
    }



    // 방 안 4개의 문을 여닫는 함수
    void OpenAllDoors(bool isClosed)
    {
        for (int i = 0; i < doors.Length; i++)
            doors[i].OpenOrCloseTheDoor(isClosed);
    }



    //방 활성화 함수
    public void ActiveRoom(bool isActive)
    {
        active = isActive;
        StartChasingPlayer(isActive);
    }



    // 방에 파리 추가 함수
    public void AddMonster(Fly fly)
    {
        // 몬스터 수 +1
        monstersInRoom++;
        //클리어 X상태로 전환
        clear = false;
        // 문을 모두 닫기
        for (int i = 0; i < doors.Length; i++)
            doors[i].OpenOrCloseTheDoor(true);
        // 파리 리스트에 파리 추가
        flies.Add(fly);

        // 방이 활성화된 상태라면 추적 시작
        fly.StartMoveToTarget(active);
    }
}
