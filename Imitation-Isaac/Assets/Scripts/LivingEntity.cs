using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    // ���� ���� ������ ������ Ư���� (ü��, ��� ����, ���ݷ�)
    // set ���������ڸ� protected�� ������ ���ΰ� �ڽ� Ŭ���������� ���� ����
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    public float str { get; protected set; }

    // �ʱ� ü��, �������, ���ݷ� �ʱ�ȭ �Լ�
    public virtual void InitData(float startHealth, float startStr)
    {
        health = startHealth;
        dead = false;
        str = startStr;
    }

    // �ǰ� ó�� �Լ�
    public virtual void OnDamage(float damage)
    {
        // ������¸� �Լ� ����
        if (dead)
            return;
        
        //ü�� ���� �� ü���� 0 �����̸� OnDie �Լ� ����
        health -= damage;
        if (health <= 0)
            OnDie();
    }

    // ��� ó�� �Լ�
    public virtual void OnDie()
    {
        // ��� ���� ��ȯ
        dead = true;
    }

}
