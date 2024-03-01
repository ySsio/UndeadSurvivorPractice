using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


// 지금 얘 생각은 Weapon이 Bullet을 모아놓는 곳임 ㅋㅋ 무기/총알 의미는 아니고
public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed; // 회전 속도

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
                speed = 150; // 시계방향 회전
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
            // Get()으로 생성하면 poolmanager 아래에 생성될텐데, 부모를 player 밑 weapon(this)으로 바꿔야 함.
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

            // 각 무기마다 초기화해줌.
            t_bullet.GetComponent<Bullet>().Init(damage, -1);   // -1 is Infinity Per.

            // 무기를 적절히 배치하여 (각을 균등하게 나눠서) 예쁘게 회전하도록
            t_bullet.Rotate(Vector3.forward * 360 * (float)index / count);
            t_bullet.Translate(Vector3.up * 1.5f); // 로컬 하게 움직이는 함수. (본인기준 이동함수)

        }
    }
}
