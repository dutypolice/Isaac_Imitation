using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ݶ��̴� ���� Ŭ����
public class DoorColider : MonoBehaviour
{
    //�ʿ� ������Ʈ��
    // box�ݶ��̴� = ���� ������Ʈ���� ���� �Ѿ�� ���ϵ��� ���� (Ʈ���� �ݶ��̴��� ���� ���� ������ ���� ��Ȳ���� ���� ������ �Ѿ�� ���� ����)
    BoxCollider2D boxCollider;
    // ��������Ʈ ���� Ŭ����(�ڽ� ������Ʈ�� ����)
    Door childDoor;
    // ���� ��� ���� ���� ��ũ��Ʈ
    Room nextRoomScript;
    Room currentRoomScript;

    // ���� �� ������Ʈ
    public GameObject nextRoom;



    void Awake()
    {
        //������Ʈ ��������
        boxCollider = GetComponent<BoxCollider2D>();
        childDoor = GetComponentInChildren<Door>();
        if(nextRoom!=null)
            nextRoomScript = nextRoom.GetComponent<Room>();
        currentRoomScript = GetComponentInParent<Room>();

        //���� �� ��� �� �ݱ�
        OpenOrCloseTheDoor(true);
    }



    //�� ���ݱ� ó�� �Լ�
    public void OpenOrCloseTheDoor(bool isClosed)
    {
        // ���� ��������Ʈ�� �����ϴ� �ڽ� ������Ʈ�� Ȱ��ȭ ������ ������ �� ���ݱ�
        if(childDoor != null)
        {
            childDoor.LockDoor(isClosed);
            boxCollider.enabled = isClosed;
        }
    }



    //���� ������ �̵� ó�� -  ī�޶� �̵�, �� Ȱ��ȭ, �÷��̾� ���� ������ �̵�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //���� ���� ��Ȱ��ȭ
            currentRoomScript.ActiveRoom(false);
            //���� ī�޶� �̿��� �극�� ī�޶� ���� ������ �̵�, �� Ȱ��ȭ
            nextRoomScript.OnVirtualCamera();
            nextRoomScript.ActiveRoom(true);

            //�÷��̾ ���� ������ �̵�
            PlayerSpawnNextRoom();
            //���� ���� ���͵��� �÷��̾� ���� ����
            nextRoomScript.StartChasingPlayer(true);
        }
    }


    //�÷��̾� ���� �� �̵� �Լ�
    public void PlayerSpawnNextRoom()
    {
        GameObject player = FindObjectOfType<PlayerInput>().gameObject;
        player.transform.position = transform.Find("SpawnPosition").position;
    }
}
