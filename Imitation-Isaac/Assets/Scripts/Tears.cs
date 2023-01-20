using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��ũ��Ʈ
public class Tears : MonoBehaviour
{
    //���� ���ݷ�, �ӵ�
    protected float str;
    protected float speed;
    
    protected Rigidbody2D tearRigidbody;

    // ���� ���� �� ���� �Լ�
    public virtual void OnEnable()
    {
        // ������Ʈ �ҷ�����
        tearRigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1f);
    }

    // ������ ���� ���� �Լ�
    public virtual void SetTearStats(float startStr, float startSpeed)
    {
        str = startStr;
        speed = startSpeed;
    }

    // ������ �ӵ� ����
    public virtual void SetTearVelocity(Vector2 tearDir)
    {
        // ������ �ӵ��� ���ڷ� ���� ���� ����, ��ӹ��� Ŭ���� ������ speed�� ����
        tearRigidbody.velocity = tearDir * speed;
    }
}
