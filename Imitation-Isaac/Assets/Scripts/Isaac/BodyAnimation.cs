using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimation : MonoBehaviour
{
    //�ִϸ��̼� �Ķ����
    //[float] vertical      - ������������ ForwardMoving�� BackMoving�� �����
    //[float] horizontal    - ����������� RIghtMoving�� LeftMoving�� �����
    //[bool] stopped        - ����(stop)�� �����

    //�ʿ� ������Ʈ
    PlayerInput playerInput;
    Animator bodyAnimator;
    void Start()
    {
        //������Ʈ ��������
        playerInput = GetComponentInParent<PlayerInput>();
        bodyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovingParameterSet();
    }

    //������ ���� �ٵ� �ִϸ��̼� ����
    void MovingParameterSet()
    {
        //����, ���� �Է��� �Է� �ڵ忡�� ��������
        float horInput = playerInput.horizontalMoving;
        float verInput = playerInput.verticalMoving;
        //���� �Է��� ���ٸ� �������·� ��ȯ, ������ �Լ� ����
        if(horInput ==0 && verInput == 0)
        {
            bodyAnimator.SetBool("stopped", true);
            return;
        }

        //�Է��� �ִٸ� �������� ����
        bodyAnimator.SetBool("stopped", false);
        //����, ���� �Է¿� ���� �ٵ� �ִϸ��̼� ����
        //�̶� ���� �̵� �ִϸ��̼��� ���� �̵� �ִϸ��̼Ǻ��� �켱������ ����ǰ� �ߴ�.
        if (Mathf.Abs(horInput) >= Mathf.Abs(verInput) * 0.9f)
            verInput = 0f;
        else
            horInput = 0f;
        bodyAnimator.SetFloat("horizontal", horInput);
        bodyAnimator.SetFloat("vertical", verInput);
    }
}
