using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;    // ���� ü��
    public float maxHealth; // �ִ� ü��
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
        health = maxHealth;
    }

    public void Init(SpawnData _spawndata)
    {
        anim.runtimeAnimatorController = animCon[_spawndata.spriteType];
        speed = _spawndata.speed;
        maxHealth = _spawndata.maxHealth;
    }
}
