using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    // 게임 내의 생물이 가지는 특성들 (체력, 사망 여부, 공격력)
    // set 접근지정자를 protected로 설정해 본인과 자식 클래스에서만 수정 가능
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    public float str { get; protected set; }

    // 초기 체력, 사망상태, 공격력 초기화 함수
    public virtual void InitData(float startHealth, float startStr)
    {
        health = startHealth;
        dead = false;
        str = startStr;
    }

    // 피격 처리 함수
    public virtual void OnDamage(float damage)
    {
        // 사망상태면 함수 종료
        if (dead)
            return;
        
        //체력 감소 뒤 체력이 0 이하이면 OnDie 함수 실행
        health -= damage;
        if (health <= 0)
            OnDie();
    }

    // 사망 처리 함수
    public virtual void OnDie()
    {
        // 사망 상태 전환
        dead = true;
    }

}
