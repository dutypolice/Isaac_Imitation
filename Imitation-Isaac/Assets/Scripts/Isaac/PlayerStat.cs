using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾� ���� Ŭ����
public class PlayerStat : LivingEntity
{
    //�ʿ� ������Ʈ
    AudioSource audioSource;
    PlayerMoving playerMoving;
    SpriteRenderer spriteRenderer;
    public List<GameObject> childrenObjects = new List<GameObject>();

    // �ǰ� Ŭ��
    public AudioClip hurtClip;
    //����� Ŭ��
    public AudioClip deathClip;
    // ���� ���� �ð�
    float unbeatableTime = 1f;
    // ���������� �ǰ� ���� �ð�
    float lastBeatedTime;
    void Start()
    {
        //�ʿ� ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
        playerMoving = GetComponent<PlayerMoving>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //���� �ð�, �⺻ ���� �ʱ�ȭ
        lastBeatedTime = 0f;
        InitData(6f, 10f);    
    }

    // ��� �Լ� �������̵�
    public override void OnDie()
    {
        // ��� ����� Ŭ�� ����
        audioSource.PlayOneShot(deathClip);
        // �÷��̾� ����, ������ �Լ� ����
        playerMoving.StopMoving();
        playerMoving.enabled = false;
        // Player�� �ڽ� ������Ʈ�� Head�� Body ������Ʈ�� ��Ȱ��ȭ ��
        // ��Ȱ��ȭ ���ѳ��� PlayerRenderer�� Ȱ��ȭ���� ����� ������� �ٲٱ�
        childrenObjects[0].SetActive(false);
        childrenObjects[1].SetActive(false);
        spriteRenderer.enabled = true;

        base.OnDie();
    }


    // �ǰ� �Լ� �������̵�
    public override void OnDamage(float damage)
    {
        //���� �ǰ� ���� �ð��̸� �ǰ�X
        if (lastBeatedTime + unbeatableTime > Time.time)
            return;

        // �ǰ�
        base.OnDamage(damage);

        if (!dead)
        {
            // �ǰ��� ����
            audioSource.PlayOneShot(hurtClip);
            // ���������� �ǰݴ��� �ð��� ����� ����
            lastBeatedTime = Time.time;
        }
    }





    // ������ ��� ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹 ������Ʈ�� �±װ� �������̸� ���ó��
        if(collision.tag == "Item")
        {
            IItem item = collision.GetComponent<IItem>();
            if (item != null)
                item.Use(gameObject);
        }
    }

    //�÷��̾� ���ݷ� �߰� �Լ�
    public void StrPlus(int magnifi)
    {
        str *= magnifi;
    }
}
