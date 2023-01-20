using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문짝 스프라이트 관리 클래스
public class Door : MonoBehaviour
{
    //좌우 문짝 오브젝트
    public GameObject[] Doors;

    // 문짝의 스프라이트 여닫는 함수
    public void LockDoor(bool isClosed)
    {
        for (int i = 0; i < Doors.Length; i++)
            Doors[i].gameObject.SetActive(isClosed);
    }
}
