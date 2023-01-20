using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이작의 눈물 클래스
public class IsaacTear : Tears
{

    // 트리거 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 눈물은 아이작의 머리부분에서 발사 됨, 발사 시 머리와의 트리거 충돌 무시
        // 눈물끼리의 트리거 충돌 무시
        if (collision.tag != "Player" && collision.tag != "Tear")
        {
            // 충돌한 상대가 몬스터라면 몬스터 공격 처리
            if (collision.tag == "Monster")
            {
                LivingEntity monsterLE = collision.gameObject.GetComponent<LivingEntity>();

                if (monsterLE != null)
                    monsterLE.OnDamage(str);
            }
            // 플레이어, 눈물이 아닌 오브젝트와 충돌 시 눈물 파괴
            Destroy(gameObject);
        }
    }

    public void addPlayerInertia( Vector2 playerVelocity )
    {
        tearRigidbody.velocity += 0.5f * playerVelocity;
    }
}
