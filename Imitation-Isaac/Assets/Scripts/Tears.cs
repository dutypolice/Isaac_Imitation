using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 눈물 스크립트
public class Tears : MonoBehaviour
{
    //눈물 공격력, 속도
    protected float str;
    protected float speed;
    
    protected Rigidbody2D tearRigidbody;

    // 눈물 생성 시 실행 함수
    public virtual void OnEnable()
    {
        // 컴포넌트 불러오기
        tearRigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1f);
    }

    // 눈물의 스탯 설정 함수
    public virtual void SetTearStats(float startStr, float startSpeed)
    {
        str = startStr;
        speed = startSpeed;
    }

    // 눈물의 속도 변경
    public virtual void SetTearVelocity(Vector2 tearDir)
    {
        // 눈물의 속도는 인자로 받은 눈물 방향, 상속받은 클래스 내부의 speed로 결정
        tearRigidbody.velocity = tearDir * speed;
    }
}
