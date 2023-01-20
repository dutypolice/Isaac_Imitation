using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾� �Է� ��ũ��Ʈ
public class PlayerInput : MonoBehaviour
{
    //������Ƽ�� ����Ͽ� Ŭ���� ���ο����� �Է°��� �����ϵ��� ����
    //������Ƽ ���������ڴ� public���� �ٸ� Ŭ�������� ���� ������ �� ������ ������Ƽ�� set���� �����ڴ� private�̹Ƿ� �ٸ� Ŭ�������� ���� �Ұ�
    public float verticalMoving { get; private set; }
    public float horizontalMoving { get; private set; }
    public float verticalAttack { get; private set; }
    public float horizontalAttack { get; private set; }


    void Start()
    {
        verticalAttack = 0f;
        verticalMoving = 0f;
        horizontalMoving = 0f;
        horizontalAttack = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //wasd �Է� ó��
        verticalMoving = Input.GetAxis("Vertical");
        horizontalMoving = Input.GetAxis("Horizontal");

        //����Ű �Է� ó��
        verticalAttack = Input.GetAxisRaw("Vertical Attack");
        horizontalAttack = Input.GetAxisRaw("Horizontal Attack");
    }
}