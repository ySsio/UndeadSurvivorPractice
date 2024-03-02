using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; // ��� �����ϴ��� ( �� ���� ���� ����ϴٰ� ����� ������ .. ���� : ���� (-1) / ���Ÿ� : �� ���� �ְ���)

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

        // ���� �� ����
        --per;


        // �Ѿ� ���� �� �� �Ҹ������� ������Ʈ ��.
        if (per <= -1)
        {
            // �ӵ� ���� - ��.. �� �ؾߵ�? init �� �� ���� �ϸ� ���� �ʳ�
            rigid.velocity = Vector3.zero;

            // �Ѿ� ��
            gameObject.SetActive(false);
        }
    }
}
