using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; // 몇번 관통하는지 ( 몇 명의 적을 통과하다가 사라질 것인지 .. 근접 : 무한 (-1) / 원거리 : 뭐 제한 있겠지)

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float _damage, int _per, Vector3 dir = default)
    {
        damage = _damage; per = _per;

        if (per > -1)
        {
            rigid.velocity = dir * 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per <= -1)
            return;

        // 관통 수 감소
        --per;


        // 총알 관통 수 다 소모했으면 오브젝트 끔.
        if (per <= -1)
        {
            // 속도 제거 - 흠.. 꼭 해야됨? init 할 때 조절 하면 되지 않나
            rigid.velocity = Vector3.zero;

            // 총알 끔
            gameObject.SetActive(false);
        }
    }
}
