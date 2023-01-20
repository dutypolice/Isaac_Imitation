using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ���� Ŭ����
public class IsaacTear : Tears
{

    // Ʈ���� �浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ �������� �Ӹ��κп��� �߻� ��, �߻� �� �Ӹ����� Ʈ���� �浹 ����
        // ���������� Ʈ���� �浹 ����
        if (collision.tag != "Player" && collision.tag != "Tear")
        {
            // �浹�� ��밡 ���Ͷ�� ���� ���� ó��
            if (collision.tag == "Monster")
            {
                LivingEntity monsterLE = collision.gameObject.GetComponent<LivingEntity>();

                if (monsterLE != null)
                    monsterLE.OnDamage(str);
            }
            // �÷��̾�, ������ �ƴ� ������Ʈ�� �浹 �� ���� �ı�
            Destroy(gameObject);
        }
    }

    public void addPlayerInertia( Vector2 playerVelocity )
    {
        tearRigidbody.velocity += 0.5f * playerVelocity;
    }
}
