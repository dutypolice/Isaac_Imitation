using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    // �ʿ� ������Ʈ��
    // �濡 �޷��ִ� ��
    public DoorColider[] doors;
    // �濡 ��ȯ�� �ĸ� ����Ʈ
    List<Fly> flies = new List<Fly>();
    // ���� ī�޶� ������Ʈ
    public CinemachineVirtualCamera currentRoomCam;


    public bool clear = false;
    // ���� ���� ���� ��
    int monstersInRoom = 0;
    // ���� Ȱ��ȭ ����
    bool active = false;



    void Start()
    {   // ���� ���� �� ���� ���� ���� 0 �����̸� ��� �� �� ����
        if (monstersInRoom <= 0)
        {
            clear = true;
            OpenAllDoors(false);
        }
    }



    // �� ������ ��� �ĸ��� �÷��̾� ����, ���� �Լ�
    public void StartChasingPlayer(bool chase)
    {
        for (int i = 0; i < flies.Count; i++)
            flies[i].StartMoveToTarget(chase);
    }



    // ī�޶� �̵� �Լ�
    public void OnVirtualCamera()
    {   
        // ��ũ��Ʈ�� ���� ���� ���� ī�޶� ���� �Ѽ� ī�޶� �� ���� ���ߵ��� ��
        currentRoomCam.enabled = false;
        currentRoomCam.enabled = true;
    }



    // ���� ��� �� ó�� �Լ�
    public void OnMonsterDead()
    {
        // �� ������ ���� �� -1
        monstersInRoom -- ;

        //  ���� ��� ���Ͱ� ����ϸ� �� ����
        if (monstersInRoom <= 0)
        {
            clear = true;
            OpenAllDoors(false);
        }
    }



    // �� �� 4���� ���� ���ݴ� �Լ�
    void OpenAllDoors(bool isClosed)
    {
        for (int i = 0; i < doors.Length; i++)
            doors[i].OpenOrCloseTheDoor(isClosed);
    }



    //�� Ȱ��ȭ �Լ�
    public void ActiveRoom(bool isActive)
    {
        active = isActive;
        StartChasingPlayer(isActive);
    }



    // �濡 �ĸ� �߰� �Լ�
    public void AddMonster(Fly fly)
    {
        // ���� �� +1
        monstersInRoom++;
        //Ŭ���� X���·� ��ȯ
        clear = false;
        // ���� ��� �ݱ�
        for (int i = 0; i < doors.Length; i++)
            doors[i].OpenOrCloseTheDoor(true);
        // �ĸ� ����Ʈ�� �ĸ� �߰�
        flies.Add(fly);

        // ���� Ȱ��ȭ�� ���¶�� ���� ����
        fly.StartMoveToTarget(active);
    }
}
