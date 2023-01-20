using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��� �ִϸ��̼� ��ũ��Ʈ
public class HeadAnimation : MonoBehaviour
{
    //�ִϸ��̼� �Ķ����
    //[bool] attack			- ����(attack)�� �����
    //[bool] stopped        - ����(stop)�� �����
    //[float] vhMoving		- �̵� ����Ʈ���� ����, ���� �����
    //[float] vhAttack		- ���� ����Ʈ���� ����, ���� �����

    //�ʿ� ������Ʈ��
    PlayerInput playerInput;
    Animator headAnimator;

    //���ݻ�Ȳ�� ��Ÿ���� ����
    bool attack;

    void Start()
    {
        //������Ʈ ��������
        playerInput = GetComponentInParent<PlayerInput>();
        headAnimator = GetComponent<Animator>();
        //���� �ÿ��� ���� ��Ȳ X
        attack = false;
    }


    void Update()
    {
        //���� �Է��� �����Ǹ� attack = true, �������� ������ attack = false 
        if (playerInput.horizontalAttack != 0 || playerInput.verticalAttack != 0)
            attack = true;
        else
            attack = false;

        //��� �ִϸ������� attack �Ķ���͸� ����
        headAnimator.SetBool("attack", attack);

        //���� ��Ȳ�� ���� ���� ���� ��� �ִϸ��̼� ����
        if (attack)
            AttackParameterSet();

        //���� ��Ȳ�� �ƴϸ� ������ ���� ��� �ִϸ��̼� ����
        else
            MovingParameterSet();
    }

    void MovingParameterSet()
    {
        //����, ���� �Է��� �Է� �ڵ忡�� ��������
        float horInput = playerInput.horizontalMoving;
        float verInput = playerInput.verticalMoving;

        //���� �̵� �Է��� ���ٸ� �������·� ��ȯ, ������ �Լ� ����
        if (horInput == 0 && verInput == 0)
        {
            headAnimator.SetBool("stopped", true);
            return;
        }
        //�̵� �Է��� �ִٸ� �������� ����
        headAnimator.SetBool("stopped", false);

        //�̵� �ִϸ��̼� ���� �� ���� �Ķ���� 0���� ����
        headAnimator.SetFloat("horizontalAttack", 0f);
        headAnimator.SetFloat("verticalAttack", 0f);
        //����, ���� �Է¿� ���� �̵� �ִϸ��̼� ����
        headAnimator.SetFloat("horizontalMoving", horInput);
        headAnimator.SetFloat("verticalMoving", 0.9f * verInput);
    }

    void AttackParameterSet()
    {
        //���� �ִϸ��̼� ���� �� �̵� �Ķ���� 0���� ����
        headAnimator.SetFloat("horizontalMoving", 0f);
        headAnimator.SetFloat("verticalMoving", 0f);
        //����, ���� ���� �Է¿� ���� ���� �ִϸ��̼� ����
        headAnimator.SetFloat("horizontalAttack", playerInput.horizontalAttack);
        headAnimator.SetFloat("verticalAttack", 0.9f * playerInput.verticalAttack);
    }
}
