using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 입력 스크립트
public class PlayerInput : MonoBehaviour
{
    //프로퍼티를 사용하여 클래스 내부에서만 입력값을 수정하도록 설정
    //프로퍼티 접근지정자는 public으로 다른 클래스에서 값은 가져올 수 있지만 프로퍼티의 set접근 지정자는 private이므로 다른 클래스에서 수정 불가
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
        //wasd 입력 처리
        verticalMoving = Input.GetAxis("Vertical");
        horizontalMoving = Input.GetAxis("Horizontal");

        //방향키 입력 처리
        verticalAttack = Input.GetAxisRaw("Vertical Attack");
        horizontalAttack = Input.GetAxisRaw("Horizontal Attack");
    }
}