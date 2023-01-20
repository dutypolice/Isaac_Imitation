using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문의 콜라이더 관리 클래스
public class DoorColider : MonoBehaviour
{
    //필요 컴포넌트들
    // box콜라이더 = 게임 오브젝트들이 문을 넘어가지 못하도록 관리 (트리거 콜라이더를 감싸 문이 열리지 않은 상황에서 다음 방으로 넘어가는 것을 방지)
    BoxCollider2D boxCollider;
    // 스프라이트 관리 클래스(자식 오브젝트에 있음)
    Door childDoor;
    // 다음 방과 현재 방의 스크립트
    Room nextRoomScript;
    Room currentRoomScript;

    // 다음 방 오브젝트
    public GameObject nextRoom;



    void Awake()
    {
        //컴포넌트 가져오기
        boxCollider = GetComponent<BoxCollider2D>();
        childDoor = GetComponentInChildren<Door>();
        if(nextRoom!=null)
            nextRoomScript = nextRoom.GetComponent<Room>();
        currentRoomScript = GetComponentInParent<Room>();

        //시작 시 모든 문 닫기
        OpenOrCloseTheDoor(true);
    }



    //문 여닫기 처리 함수
    public void OpenOrCloseTheDoor(bool isClosed)
    {
        // 문의 스프라이트를 관리하는 자식 오브젝트가 활성화 돼있을 때에만 문 여닫기
        if(childDoor != null)
        {
            childDoor.LockDoor(isClosed);
            boxCollider.enabled = isClosed;
        }
    }



    //다음 방으로 이동 처리 -  카메라 이동, 방 활성화, 플레이어 다음 방으로 이동
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //현재 방을 비활성화
            currentRoomScript.ActiveRoom(false);
            //가상 카메라를 이용해 브레인 카메라를 다음 방으로 이동, 방 활성화
            nextRoomScript.OnVirtualCamera();
            nextRoomScript.ActiveRoom(true);

            //플레이어를 다음 방으로 이동
            PlayerSpawnNextRoom();
            //다음 방의 몬스터들이 플레이어 추적 시작
            nextRoomScript.StartChasingPlayer(true);
        }
    }


    //플레이어 다음 방 이동 함수
    public void PlayerSpawnNextRoom()
    {
        GameObject player = FindObjectOfType<PlayerInput>().gameObject;
        player.transform.position = transform.Find("SpawnPosition").position;
    }
}
