using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;    // 현재 체력
    public float maxHealth; // 최대 체력
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    public bool isAlive;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;


    void Awake()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rigid.velocity = Vector2.zero;

        if (!isAlive)
            return;


        Vector2 dirVec = (target.position - rigid.position).normalized;
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
        health = maxHealth;
    }

    public void Init(SpawnData _spawndata)
    {
        anim.runtimeAnimatorController = animCon[_spawndata.spriteType];
        speed = _spawndata.speed;
        maxHealth = _spawndata.maxHealth;
    }
}
