using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��¦ ��������Ʈ ���� Ŭ����
public class Door : MonoBehaviour
{
    //�¿� ��¦ ������Ʈ
    public GameObject[] Doors;

    // ��¦�� ��������Ʈ ���ݴ� �Լ�
    public void LockDoor(bool isClosed)
    {
        for (int i = 0; i < Doors.Length; i++)
            Doors[i].gameObject.SetActive(isClosed);
    }
}
