using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; // 몇번 관통하는지 ( 몇 명의 적을 통과하다가 사라질 것인지 .. 근접 : 무한 (-1) / 원거리 : 뭐 제한 있겠지)

    public void Init(float _damage, int _per)
    {
        damage = _damage; per = _per;
    }
}
