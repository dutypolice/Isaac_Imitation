using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 크리켓의 머리 클래스
public class CricketHead : MonoBehaviour, IItem
{
    // 크리켓의 머리 아이템 효과 함수
    public void Use(GameObject target)
    {
        // 공격력 2배, 게임 오브젝트 파괴
        target.GetComponent<PlayerStat>().StrPlus(2);
        Destroy(gameObject);
    }
}
