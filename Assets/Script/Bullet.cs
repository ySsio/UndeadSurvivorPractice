using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; // ��� �����ϴ��� ( �� ���� ���� ����ϴٰ� ����� ������ .. ���� : ���� (-1) / ���Ÿ� : �� ���� �ְ���)

    public void Init(float _damage, int _per)
    {
        damage = _damage; per = _per;
    }
}
