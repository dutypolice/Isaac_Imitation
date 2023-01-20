using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 스탯 클래스
public class PlayerStat : LivingEntity
{
    //필요 컴포넌트
    AudioSource audioSource;
    PlayerMoving playerMoving;
    SpriteRenderer spriteRenderer;
    public List<GameObject> childrenObjects = new List<GameObject>();

    // 피격 클립
    public AudioClip hurtClip;
    //사망음 클립
    public AudioClip deathClip;
    // 무적 판정 시간
    float unbeatableTime = 1f;
    // 마지막으로 피격 당한 시간
    float lastBeatedTime;
    void Start()
    {
        //필요 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        playerMoving = GetComponent<PlayerMoving>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //판정 시간, 기본 스탯 초기화
        lastBeatedTime = 0f;
        InitData(6f, 10f);    
    }

    // 사망 함수 오버라이드
    public override void OnDie()
    {
        // 사망 오디오 클립 실행
        audioSource.PlayOneShot(deathClip);
        // 플레이어 정지, 움직임 함수 종료
        playerMoving.StopMoving();
        playerMoving.enabled = false;
        // Player의 자식 오브젝트인 Head와 Body 오브젝트를 비활성화 후
        // 비활성화 시켜놨던 PlayerRenderer를 활성화시켜 사망한 모습으로 바꾸기
        childrenObjects[0].SetActive(false);
        childrenObjects[1].SetActive(false);
        spriteRenderer.enabled = true;

        base.OnDie();
    }


    // 피격 함수 오버라이드
    public override void OnDamage(float damage)
    {
        //아직 피격 판정 시간이면 피격X
        if (lastBeatedTime + unbeatableTime > Time.time)
            return;

        // 피격
        base.OnDamage(damage);

        if (!dead)
        {
            // 피격음 실행
            audioSource.PlayOneShot(hurtClip);
            // 마지막으로 피격당한 시간을 현재로 설정
            lastBeatedTime = Time.time;
        }
    }





    // 아이템 사용 처리 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 오브젝트의 태그가 아이템이면 사용처리
        if(collision.tag == "Item")
        {
            IItem item = collision.GetComponent<IItem>();
            if (item != null)
                item.Use(gameObject);
        }
    }

    //플레이어 공격력 추가 함수
    public void StrPlus(int magnifi)
    {
        str *= magnifi;
    }
}
