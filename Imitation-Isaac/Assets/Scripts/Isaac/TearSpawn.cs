using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 눈물 생성기 클래스
public class TearSpawn : MonoBehaviour
{
    //필요 컴포넌트들
    Rigidbody2D playerRigidbody;
    PlayerInput playerInput;
    LivingEntity playerLE;
    public GameObject tearPrefab;

    // 연사 속도
    float fireRate;
    // 마지막 발사 시간
    float timeAfterSpawn;
    // 눈물의 진행 방향
    Vector2 tearDir;


    void Start()
    {
        //필요 컴포넌트 불러오기
        playerInput = GetComponentInParent<PlayerInput>();
        playerRigidbody = GetComponentInParent<Rigidbody2D>();
        playerLE = GetComponentInParent<LivingEntity>();

        //초기 값 설정
        fireRate = 0.3f;
        timeAfterSpawn = 0f;
    }


    void Update()
    {
        // 연사가 가능한 시간에 공격 입력이 감지되면
        if (isShooting() && timeAfterSpawn + fireRate < Time.time)
        {
            // 공격 입력에 따라 눈물 방향 설정
            SetTearDir();
            //눈물 생성,  연사 가능 시간 재설정
            TearInstantiate(playerLE.str, 7f);
            timeAfterSpawn = Time.time;
        }
    }


    // 공격 입력 감지 함수
    bool isShooting()
    {
        if (playerInput.verticalAttack != 0 || playerInput.horizontalAttack != 0)
            return true;

        return false;

    }


    // 입력에 따른 눈물 방향 설정 함수
    void SetTearDir()
    {
        // 수평 공격 입력, 수직 공격 입력값을 가져오기, 이때 수평 공격이 우선되도록 수직 공격 입력 값에는 0.9 곱하기
        float horizontalInput = playerInput.horizontalAttack;
        float verticalInput = 0.9f * playerInput.verticalAttack;

        // 공격 입력값에 따라 눈물 방향 설정
        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
            tearDir = horizontalInput > 0 ? Vector2.right : Vector2.left;
        else
            tearDir = verticalInput > 0 ? Vector2.up : Vector2.down;
    }


    //눈물 생성 함수
    void TearInstantiate(float damage, float speed)
    {
        //눈물을 생성, 눈물의 데미지 설정
        GameObject tear = Instantiate(tearPrefab, transform.position, Quaternion.identity);
        IsaacTear tearScript = tear.GetComponent<IsaacTear>();
        tearScript.SetTearStats(damage, speed);

        //눈물의 관성을 위해 아이작의 현재 속도정보 가져오기
        Vector2 playerVelocity = playerRigidbody.velocity;

        //tearScript로 눈물의 속도 설정
        tearScript.SetTearVelocity(tearDir);
        tearScript.addPlayerInertia(playerVelocity);
    }
}
