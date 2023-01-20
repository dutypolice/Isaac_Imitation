using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ Ŭ����
public class TearSpawn : MonoBehaviour
{
    //�ʿ� ������Ʈ��
    Rigidbody2D playerRigidbody;
    PlayerInput playerInput;
    LivingEntity playerLE;
    public GameObject tearPrefab;

    // ���� �ӵ�
    float fireRate;
    // ������ �߻� �ð�
    float timeAfterSpawn;
    // ������ ���� ����
    Vector2 tearDir;


    void Start()
    {
        //�ʿ� ������Ʈ �ҷ�����
        playerInput = GetComponentInParent<PlayerInput>();
        playerRigidbody = GetComponentInParent<Rigidbody2D>();
        playerLE = GetComponentInParent<LivingEntity>();

        //�ʱ� �� ����
        fireRate = 0.3f;
        timeAfterSpawn = 0f;
    }


    void Update()
    {
        // ���簡 ������ �ð��� ���� �Է��� �����Ǹ�
        if (isShooting() && timeAfterSpawn + fireRate < Time.time)
        {
            // ���� �Է¿� ���� ���� ���� ����
            SetTearDir();
            //���� ����,  ���� ���� �ð� �缳��
            TearInstantiate(playerLE.str, 7f);
            timeAfterSpawn = Time.time;
        }
    }


    // ���� �Է� ���� �Լ�
    bool isShooting()
    {
        if (playerInput.verticalAttack != 0 || playerInput.horizontalAttack != 0)
            return true;

        return false;

    }


    // �Է¿� ���� ���� ���� ���� �Լ�
    void SetTearDir()
    {
        // ���� ���� �Է�, ���� ���� �Է°��� ��������, �̶� ���� ������ �켱�ǵ��� ���� ���� �Է� ������ 0.9 ���ϱ�
        float horizontalInput = playerInput.horizontalAttack;
        float verticalInput = 0.9f * playerInput.verticalAttack;

        // ���� �Է°��� ���� ���� ���� ����
        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
            tearDir = horizontalInput > 0 ? Vector2.right : Vector2.left;
        else
            tearDir = verticalInput > 0 ? Vector2.up : Vector2.down;
    }


    //���� ���� �Լ�
    void TearInstantiate(float damage, float speed)
    {
        //������ ����, ������ ������ ����
        GameObject tear = Instantiate(tearPrefab, transform.position, Quaternion.identity);
        IsaacTear tearScript = tear.GetComponent<IsaacTear>();
        tearScript.SetTearStats(damage, speed);

        //������ ������ ���� �������� ���� �ӵ����� ��������
        Vector2 playerVelocity = playerRigidbody.velocity;

        //tearScript�� ������ �ӵ� ����
        tearScript.SetTearVelocity(tearDir);
        tearScript.addPlayerInertia(playerVelocity);
    }
}
