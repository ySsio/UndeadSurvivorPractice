using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // �ּ� �κ��� InputSystem�� ������� �ʰ� ������� �κ�
  
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    /*void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }*/

    void FixedUpdate()
    {
        // 1. ���� �ش�
        //rigid.AddForce(inputVec);

        // 2. �ӵ� ����
        //rigid.velocity = inputVec;

        // 3. ��ġ �̵�
        //rigid.MovePosition(inputVec);

        // Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        

        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // lateupdate : �������� ���� �Ǳ� ���� ����Ǵ� ���� �ֱ� �Լ�
    void LateUpdate()
    {
        // animator�� float �Ķ���͸� �����ϴ� �Լ�
        // ���� 1: �Ķ���� �̸� (string Ÿ��), ���� 2: float ��
        anim.SetFloat("Speed",inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

}
