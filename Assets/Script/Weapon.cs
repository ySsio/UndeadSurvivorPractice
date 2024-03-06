using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    float timer;


    private void Update()
    {
        ControlWeapon();

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

        // BroadcastMessage()로 모든 자식들이 ApplyGear을 실행하도록 함.
        // 새로운 무기를 얻어 레벨업 해도 기어 속성이 적용되도록.
        GameManager.instance.player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    private void ControlWeapon()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back, speed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                    
                }
                break;

            default:
                
                break;
        }

    }

    public void Init(ItemData data)
    {
        // Basic Set (게임오브젝트 생성 초기화)
        name = "Weapon " + data.itemId;       // 여기서 name은 게임오브젝트의 이름을 설정하는 것. (this.name)
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;


        // 이게 뭐지
        for (int i = 0; i < GameManager.instance.poolManager.prefabs.Length; i++)
        {
            if(data.projectile == GameManager.instance.poolManager.prefabs[i])
            {
                prefabId = i;
                break;
            }

        }

        // id = Weapon의 종류
        switch (id)
        {
            // id 0 : 근접무기 관리
            case 0:
                speed = 150; // 시계방향 회전
                AllocateBullet();
                break;

            // id 1 : 원거리 무기 관리
            case 1:
                speed = .3f;

                break;

            default:
                break;
        }

        // Hand set
        Hand hand = GameManager.instance.player.hands[(int)data.itemType];  // item 정보 보면 왼손은 근거리 (id 0), 오른손은 원거리 (id 1)
        hand.spriteRenderer.sprite = data.handSprite;
        hand.gameObject.SetActive(true);

        // BroadcastMessage()로 모든 자식들이 ApplyGear을 실행하도록 함.
        // 새로운 무기를 얻어 레벨업 해도 기어 속성이 적용되도록.
        GameManager.instance.player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
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

    private Transform Fire()
    {
        if (!GameManager.instance.player.scanner.nearestTarget)
            return null;

        Vector3 targetPos = GameManager.instance.player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;
        
        // 오브젝트 풀링
        Transform t_bullet = GameManager.instance.poolManager.Get(prefabId).transform;

        // 새로 생성됐으면 상관없는데 재활용이면 다시 위치 초기화 해줘야 함
        t_bullet.position = transform.position;

        // weapon1 아래로 들어오게 
        t_bullet.parent = transform;

        // FromToRotation (FromDir, ToDir) : FromDir의 로컬 축 (x/y/z)를 ToDir 방향으로 옮긴다. FromDir을 축으로 회전하는게 아니라 축의 방향을 바꾸는거임.
        t_bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        // 초기화
        t_bullet.GetComponent<Bullet>().Init(damage, count, dir);

        

        return t_bullet;
    }
}
