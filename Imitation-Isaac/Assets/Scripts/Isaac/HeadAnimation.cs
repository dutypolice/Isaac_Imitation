using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//헤드 애니메이션 스크립트
public class HeadAnimation : MonoBehaviour
{
    //애니메이션 파라미터
    //[bool] attack			- 공격(attack)의 컨디션
    //[bool] stopped        - 정지(stop)의 컨디션
    //[float] vhMoving		- 이동 블렌드트리의 수평, 수직 컨디션
    //[float] vhAttack		- 공격 블렌드트리의 수평, 수직 컨디션

    //필요 컴포넌트들
    PlayerInput playerInput;
    Animator headAnimator;

    //공격상황을 나타내는 변수
    bool attack;

    void Start()
    {
        //컴포넌트 가져오기
        playerInput = GetComponentInParent<PlayerInput>();
        headAnimator = GetComponent<Animator>();
        //시작 시에는 공격 상황 X
        attack = false;
    }


    void Update()
    {
        //공격 입력이 감지되면 attack = true, 감지되지 않으면 attack = false 
        if (playerInput.horizontalAttack != 0 || playerInput.verticalAttack != 0)
            attack = true;
        else
            attack = false;

        //헤드 애니메이터의 attack 파라미터를 수정
        headAnimator.SetBool("attack", attack);

        //공격 상황일 때는 공격 시의 헤드 애니메이션 실행
        if (attack)
            AttackParameterSet();

        //공격 상황이 아니면 움직임 시의 헤드 애니메이션 실행
        else
            MovingParameterSet();
    }

    void MovingParameterSet()
    {
        //수평, 수직 입력을 입력 코드에서 가져오기
        float horInput = playerInput.horizontalMoving;
        float verInput = playerInput.verticalMoving;

        //현재 이동 입력이 없다면 정지상태로 전환, 움직임 함수 종료
        if (horInput == 0 && verInput == 0)
        {
            headAnimator.SetBool("stopped", true);
            return;
        }
        //이동 입력이 있다면 정지상태 해제
        headAnimator.SetBool("stopped", false);

        //이동 애니메이션 시작 전 공격 파라미터 0으로 설정
        headAnimator.SetFloat("horizontalAttack", 0f);
        headAnimator.SetFloat("verticalAttack", 0f);
        //수평, 수직 입력에 따라 이동 애니메이션 실행
        headAnimator.SetFloat("horizontalMoving", horInput);
        headAnimator.SetFloat("verticalMoving", 0.9f * verInput);
    }

    void AttackParameterSet()
    {
        //공격 애니메이션 시작 전 이동 파라미터 0으로 설정
        headAnimator.SetFloat("horizontalMoving", 0f);
        headAnimator.SetFloat("verticalMoving", 0f);
        //수평, 수직 공격 입력에 따라 공격 애니메이션 실행
        headAnimator.SetFloat("horizontalAttack", playerInput.horizontalAttack);
        headAnimator.SetFloat("verticalAttack", 0.9f * playerInput.verticalAttack);
    }
}
