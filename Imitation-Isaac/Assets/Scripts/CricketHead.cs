using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ũ������ �Ӹ� Ŭ����
public class CricketHead : MonoBehaviour, IItem
{
    // ũ������ �Ӹ� ������ ȿ�� �Լ�
    public void Use(GameObject target)
    {
        // ���ݷ� 2��, ���� ������Ʈ �ı�
        target.GetComponent<PlayerStat>().StrPlus(2);
        Destroy(gameObject);
    }
}
