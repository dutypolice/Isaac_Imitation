using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimation : MonoBehaviour
{
    //애니메이션 파라미터
    //[float] vertical      - 수직움직임인 ForwardMoving과 BackMoving의 컨디션
    //[float] horizontal    - 수평움직임인 RIghtMoving과 LeftMoving의 컨디션
    //[bool] stopped        - 정지(stop)의 컨디션

    //필요 컴포넌트
    PlayerInput playerInput;
    Animator bodyAnimator;
    void Start()
    {
        //컴포넌트 가져오기
        playerInput = GetComponentInParent<PlayerInput>();
        bodyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovingParameterSet();
    }

    //움직일 때의 바디 애니메이션 관리
    void MovingParameterSet()
    {
        //수평, 수직 입력을 입력 코드에서 가져오기
        float horInput = playerInput.horizontalMoving;
        float verInput = playerInput.verticalMoving;
        //현재 입력이 없다면 정지상태로 전환, 움직임 함수 종료
        if(horInput ==0 && verInput == 0)
        {
            bodyAnimator.SetBool("stopped", true);
            return;
        }

        //입력이 있다면 정지상태 해제
        bodyAnimator.SetBool("stopped", false);
        //수평, 수직 입력에 따라 바디 애니메이션 실행
        //이때 수평 이동 애니메이션이 수직 이동 애니메이션보다 우선순위로 실행되게 했다.
        if (Mathf.Abs(horInput) >= Mathf.Abs(verInput) * 0.9f)
            verInput = 0f;
        else
            horInput = 0f;
        bodyAnimator.SetFloat("horizontal", horInput);
        bodyAnimator.SetFloat("vertical", verInput);
    }
}
