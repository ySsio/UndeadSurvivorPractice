using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    // status
    public float speed;
    public float health;    // ���� ü��
    public float maxHealth; // �ִ� ü��
    
    // isAlive
    public bool isAlive;

    // target
    public Rigidbody2D target;

    // Components - Animation,Sprite
    public RuntimeAnimatorController[] animCon;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Components - Physics
    private Rigidbody2D rigid;
    private Collider2D coll;
    
    
    // ���� 1 ������ �ð�?
    WaitForFixedUpdate wait;

    // this to target direction
    Vector2 dirVec;


    void Awake()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!isAlive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        rigid.velocity = Vector2.zero;

        dirVec = (target.position - rigid.position).normalized;
        Vector2 nextPos = rigid.position + dirVec * speed * Time.fixedDeltaTime;

        rigid.MovePosition(nextPos);    // �����Ӹ��� �����̵� �ϴ� �̵� ���
          // �ӵ��� 0���� ����.
    }

    private void LateUpdate()
    {
        if (!isAlive)
            return;

        // Ÿ�ٰ��� ��ġ����� ��������Ʈ ������ ����
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }


 
    // ��ũ��Ʈ�� Ȱ��ȭ �� �� ���Ǵ� �Լ� ..??
    // ������Ʈ Ǯ������ �ǻ�Ƴ� ���
    private void OnEnable()
    {
        isAlive = true;
        coll.enabled = true;
        rigid.simulated = true;
        anim.SetBool("Dead", false);
        spriteRenderer.sortingOrder = 2;
        health = maxHealth;
    }

    public void Init(SpawnData _spawndata)
    {
        anim.runtimeAnimatorController = animCon[_spawndata.spriteType];
        speed = _spawndata.speed;
        maxHealth = _spawndata.maxHealth;
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isAlive)
            return;

        // Bullet�� ������ �� bullet�� ��������ŭ ���� ü���� ���ҽ�Ŵ
        health -= collision.GetComponent<Bullet>().damage;
        
        if (health > 0)
        {
            // .. ����ִ� ���.. ��Ʈ �׼� ��
            
            anim.SetTrigger("Hit");
            StartCoroutine(KnockBack());
        }
        else
        {
            // .. ���� ��� �� ����
            isAlive = false;
            coll.enabled = false;
            rigid.simulated = false;
            anim.SetBool("Dead", true);
            spriteRenderer.sortingOrder = 1;
            GameManager.instance.kills++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        rigid.AddForce(-dirVec * 3, ForceMode2D.Impulse);
    }


    // ���
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
