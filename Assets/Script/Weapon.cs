using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


// ���� �� ������ Weapon�� Bullet�� ��Ƴ��� ���� ���� ����/�Ѿ� �ǹ̴� �ƴϰ�
public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed; // ȸ�� �ӵ�

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        RotateWeapon();

        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 1);
        }
    }

    public void LevelUp(float _damage, int _count)
    {
        damage = _damage; count += _count;

        if (id == 0)
        {
            AllocateBullet();
        }
    }

    private void RotateWeapon()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back, speed * Time.deltaTime);
                break;

            default:
                break;
        }

        
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150; // �ð���� ȸ��
                AllocateBullet();
                break;

            default:
                break;
        }
    }


    private void AllocateBullet()
    {
        
        for (int index = 0; index < count; ++index)
        {
            // Get()���� �����ϸ� poolmanager �Ʒ��� �������ٵ�, �θ� player �� weapon(this)���� �ٲ�� ��.
            Transform t_bullet;
            if (index < transform.childCount)
            {
                t_bullet = transform.GetChild(index);
            }
            else
            {
                t_bullet = GameManager.instance.poolManager.Get(prefabId).transform;
                t_bullet.parent = transform;
            }

            

            t_bullet.localPosition = Vector3.zero;
            t_bullet.localRotation = Quaternion.identity;

            // �� ���⸶�� �ʱ�ȭ����.
            t_bullet.GetComponent<Bullet>().Init(damage, -1);   // -1 is Infinity Per.

            // ���⸦ ������ ��ġ�Ͽ� (���� �յ��ϰ� ������) ���ڰ� ȸ���ϵ���
            t_bullet.Rotate(Vector3.forward * 360 * (float)index / count);
            t_bullet.Translate(Vector3.up * 1.5f); // ���� �ϰ� �����̴� �Լ�. (���α��� �̵��Լ�)

        }
    }
}
