using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // 주석 부분은 InputSystem을 사용하지 않고 만들었던 부분
  
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
        // 1. 힘을 준다
        //rigid.AddForce(inputVec);

        // 2. 속도 제어
        //rigid.velocity = inputVec;

        // 3. 위치 이동
        //rigid.MovePosition(inputVec);

        // Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        

        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // lateupdate : 프레임이 종료 되기 전에 실행되는 생명 주기 함수
    void LateUpdate()
    {
        // animator의 float 파라미터를 설정하는 함수
        // 변수 1: 파라미터 이름 (string 타입), 변수 2: float 값
        anim.SetFloat("Speed",inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

}
