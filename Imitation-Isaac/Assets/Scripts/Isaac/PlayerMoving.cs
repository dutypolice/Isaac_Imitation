using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    //����� ������Ʈ
    Rigidbody2D playerRigidbody;
    PlayerInput playerInput;
    //�÷��̾� �̵��ӵ�
    public float speed;
    void Start()
    {
        //�ʿ��� ������Ʈ ��������
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        speed = 8f;
    }

    void FixedUpdate()
    {
        //�̵� �����Լ� ����
        Moving();
    }

    //�÷��̾� �̵� �Լ�
    void Moving()
    {
        // vertical �Է°� horizontal �Է��� ���� ���� ���� �������Ϳ� ���ѵ� ���� �÷��̾� �̵����͸� ����
        Vector2 movingDir = (playerInput.verticalMoving * Vector2.up + playerInput.horizontalMoving * Vector2.right);

        //������ ũ�Ⱑ 1�� �Ѿ�ٸ� ������ ������ �������ͷ� �ٲٱ�
        float vectorScalar = movingDir.magnitude;
        if (vectorScalar > 1)
            movingDir = movingDir.normalized;

        //�÷��̾� �̵� ó��
        playerRigidbody.velocity = movingDir * speed;
    }

    //�÷��̾� �̵� ����
    public void StopMoving()
    {
        playerRigidbody.velocity = Vector2.zero;
    }
}
