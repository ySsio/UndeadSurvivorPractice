using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    // status
    public float speed;
    public float health;    // 현재 체력
    public float maxHealth; // 최대 체력
    
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
    
    
    // 물리 1 프레임 시간?
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

        rigid.MovePosition(nextPos);    // 프레임마다 순간이동 하는 이동 방식
          // 속도는 0으로 맞춤.
    }

    private void LateUpdate()
    {
        if (!isAlive)
            return;

        // 타겟과의 위치관계로 스프라이트 뒤집기 설정
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }


 
    // 스크립트가 활성화 될 때 사용되는 함수 ..??
    // 오브젝트 풀링으로 되살아난 경우
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

        // Bullet을 맞으면 그 bullet의 데미지만큼 현재 체력을 감소시킴
        health -= collision.GetComponent<Bullet>().damage;
        
        if (health > 0)
        {
            // .. 살아있는 경우.. 히트 액션 등
            
            anim.SetTrigger("Hit");
            StartCoroutine(KnockBack());
        }
        else
        {
            // .. 죽은 경우 안 맞음
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


    // 사망
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
