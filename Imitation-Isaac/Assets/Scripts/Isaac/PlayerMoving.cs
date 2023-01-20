using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    //사용할 컴포넌트
    Rigidbody2D playerRigidbody;
    PlayerInput playerInput;
    //플레이어 이동속도
    public float speed;
    void Start()
    {
        //필요한 컴포넌트 가져오기
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        speed = 8f;
    }

    void FixedUpdate()
    {
        //이동 구현함수 실행
        Moving();
    }

    //플레이어 이동 함수
    void Moving()
    {
        // vertical 입력과 horizontal 입력을 각각 수직 수평 단위벡터와 곱한뒤 더해 플레이어 이동벡터를 생성
        Vector2 movingDir = (playerInput.verticalMoving * Vector2.up + playerInput.horizontalMoving * Vector2.right);

        //벡터의 크기가 1을 넘어간다면 방향이 동일한 단위벡터로 바꾸기
        float vectorScalar = movingDir.magnitude;
        if (vectorScalar > 1)
            movingDir = movingDir.normalized;

        //플레이어 이동 처리
        playerRigidbody.velocity = movingDir * speed;
    }

    //플레이어 이동 정지
    public void StopMoving()
    {
        playerRigidbody.velocity = Vector2.zero;
    }
}
