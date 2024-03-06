using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

        // BroadcastMessage()�� ��� �ڽĵ��� ApplyGear�� �����ϵ��� ��.
        // ���ο� ���⸦ ��� ������ �ص� ��� �Ӽ��� ����ǵ���.
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
        // Basic Set (���ӿ�����Ʈ ���� �ʱ�ȭ)
        name = "Weapon " + data.itemId;       // ���⼭ name�� ���ӿ�����Ʈ�� �̸��� �����ϴ� ��. (this.name)
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;


        // �̰� ����
        for (int i = 0; i < GameManager.instance.poolManager.prefabs.Length; i++)
        {
            if(data.projectile == GameManager.instance.poolManager.prefabs[i])
            {
                prefabId = i;
                break;
            }

        }

        // id = Weapon�� ����
        switch (id)
        {
            // id 0 : �������� ����
            case 0:
                speed = 150; // �ð���� ȸ��
                AllocateBullet();
                break;

            // id 1 : ���Ÿ� ���� ����
            case 1:
                speed = .3f;

                break;

            default:
                break;
        }

        // Hand set
        Hand hand = GameManager.instance.player.hands[(int)data.itemType];  // item ���� ���� �޼��� �ٰŸ� (id 0), �������� ���Ÿ� (id 1)
        hand.spriteRenderer.sprite = data.handSprite;
        hand.gameObject.SetActive(true);

        // BroadcastMessage()�� ��� �ڽĵ��� ApplyGear�� �����ϵ��� ��.
        // ���ο� ���⸦ ��� ������ �ص� ��� �Ӽ��� ����ǵ���.
        GameManager.instance.player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
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

    private Transform Fire()
    {
        if (!GameManager.instance.player.scanner.nearestTarget)
            return null;

        Vector3 targetPos = GameManager.instance.player.scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;
        
        // ������Ʈ Ǯ��
        Transform t_bullet = GameManager.instance.poolManager.Get(prefabId).transform;

        // ���� ���������� ������µ� ��Ȱ���̸� �ٽ� ��ġ �ʱ�ȭ ����� ��
        t_bullet.position = transform.position;

        // weapon1 �Ʒ��� ������ 
        t_bullet.parent = transform;

        // FromToRotation (FromDir, ToDir) : FromDir�� ���� �� (x/y/z)�� ToDir �������� �ű��. FromDir�� ������ ȸ���ϴ°� �ƴ϶� ���� ������ �ٲٴ°���.
        t_bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        // �ʱ�ȭ
        t_bullet.GetComponent<Bullet>().Init(damage, count, dir);

        

        return t_bullet;
    }
}
