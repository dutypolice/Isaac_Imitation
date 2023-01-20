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

    //target�� ���� AddForce ����ġ
    float moveForce = 0.5f;
    bool startMoving = false;

    void OnEnable()
    {
        //�ʿ� ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
        // Ÿ���� �÷��̾�� ����
        target = FindObjectOfType<PlayerInput>().gameObject;
        playerLE = target.GetComponent<LivingEntity>();
        flyRigidbody = GetComponent<Rigidbody2D>();
        room = GetComponentInParent<Room>();

        //�濡 �ĸ� �߰��صα�
        room.AddMonster(this);

        //�⺻ ü��, ���ݷ� ����(LivingEntity)
        InitData(20f, 0.5f);
    }

    private void Update()
    {
        // ���� ���� �Լ��� �����Ͽ� startMoving�� true�� �ٲٸ� �̵� �Լ� ����, �� Ÿ���� ����ִ� ������ ��
        if (startMoving && !playerLE.dead)
            MoveToTarget();
    }
    //�÷��̾ ���� �����̱�
    void MoveToTarget()
    {
        //Ÿ���� �����ϸ� Ÿ���� ���� �̵�, �ĸ� �̵��� ������ �ֱ����� AddForce�� �̵� ����
        if (target !=null)
        {
            Vector3 movDir = target.transform.position - transform.position;
            movDir.Normalize();

            flyRigidbody.AddForce(movDir * moveForce);
        }
    }

    //�÷��̾�� ���� �� �÷��̾��� ü�� ���
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!dead && collision.collider.tag == "Player")
        {
            LivingEntity playerLE = collision.gameObject.GetComponent<LivingEntity>();
            
            if (playerLE != null)
                playerLE.OnDamage(1f);
        }
    }

    // �ĸ��� ��� �Լ� �������̵�
    public override void OnDie()
    {
        base.OnDie();
        // ������Ʈ �ı� �� ��� �Ҹ� ���
        audioSource.PlayOneShot(deathClip);

        //�÷��̾�� ���̻� ������ ���� �ʱ� ���� ������Ʈ���� ����
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        room.OnMonsterDead();

        //2�� �� ������Ʈ �ı�
        Destroy(gameObject, 2f);
    }

    // �ĸ��� ���� ���� �Լ�
    public void StartMoveToTarget(bool chase)
    {
        startMoving = chase;
    }
}
