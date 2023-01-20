using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : LivingEntity 
{
    GameObject target;
    Rigidbody2D flyRigidbody;
    AudioSource audioSource;
    LivingEntity playerLE;
    Room room;
    public AudioClip deathClip;

    //target을 향한 AddForce 가중치
    float moveForce = 0.5f;
    bool startMoving = false;

    void OnEnable()
    {
        //필요 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        // 타겟은 플레이어로 설정
        target = FindObjectOfType<PlayerInput>().gameObject;
        playerLE = target.GetComponent<LivingEntity>();
        flyRigidbody = GetComponent<Rigidbody2D>();
        room = GetComponentInParent<Room>();

        //방에 파리 추가해두기
        room.AddMonster(this);

        //기본 체력, 공격력 설정(LivingEntity)
        InitData(20f, 0.5f);
    }

    private void Update()
    {
        // 추적 실행 함수를 실행하여 startMoving을 true로 바꾸면 이동 함수 실행, 단 타겟이 살아있는 상태일 때
        if (startMoving && !playerLE.dead)
            MoveToTarget();
    }
    //플레이어를 향해 움직이기
    void MoveToTarget()
    {
        //타겟이 존재하면 타겟을 향해 이동, 파리 이동에 관성을 주기위해 AddForce로 이동 구현
        if (target !=null)
        {
            Vector3 movDir = target.transform.position - transform.position;
            movDir.Normalize();

            flyRigidbody.AddForce(movDir * moveForce);
        }
    }

    //플레이어와 접촉 시 플레이어의 체력 깎기
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!dead && collision.collider.tag == "Player")
        {
            LivingEntity playerLE = collision.gameObject.GetComponent<LivingEntity>();
            
            if (playerLE != null)
                playerLE.OnDamage(1f);
        }
    }

    // 파리의 사망 함수 오버라이드
    public override void OnDie()
    {
        base.OnDie();
        // 오브젝트 파괴 전 사망 소리 출력
        audioSource.PlayOneShot(deathClip);

        //플레이어에게 더이상 영향을 주지 않기 위해 컴포넌트들을 끄기
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        room.OnMonsterDead();

        //2초 뒤 오브젝트 파괴
        Destroy(gameObject, 2f);
    }

    // 파리의 추적 시작 함수
    public void StartMoveToTarget(bool chase)
    {
        startMoving = chase;
    }
}
