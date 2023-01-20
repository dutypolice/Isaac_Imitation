using UnityEngine;

// 피격 가능한 오브젝트들의 체력감소에 쓰일 인터페이스
public interface IDamagable
{
    void OnDamage(float damage);
}
