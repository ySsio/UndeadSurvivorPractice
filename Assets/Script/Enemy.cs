using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;
    public bool isAlive;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        isAlive = true;
        
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
    private void OnEnable()
    {
        
    }
}
