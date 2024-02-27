using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public Vector2 inputVec;
    private Rigidbody2D rigid;
    [SerializeField]
    private float speed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ## Input Manager을 이용한 방식
        //// Input.GetAxis()를 통해 좌우/상하 입력 받을 수  있음. -> project setting
        //// Input.GetAxis()에는 보간법이 적용되어 키보드에서 손을 떼도 미끄러지는 현상 발생. Input.GetAxisRaw()는 -1/0/1 이렇게 값을 딱딱 끊어주므로 이런 현상 없앨 수 있음.
        //inputVec.x = Input.GetAxisRaw("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");
        
    }

    void FixedUpdate()
    {
        // 움직임 구현 방법 3가지
        // 1. rigid.AddForce()
        //rigid.AddForce(inputVec);

        // 2. rigid.velocity 값 수정
        //rigid.velocity = inputVec;

        // 3. rigid.MovePosition()
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(nextVec + rigid.position);   // transform.position은 Vector3d인데 rigid.position은 Rigidbody2d.position이라 Vector2d네

        // 사용자의 프레임마다 update/fixedupdate 주기가 다르기 때문에 Time.deltaTime/Time.fixedDeltaTime을 곱해주지 않으면 환경에 따라 차이가 발생함.
    }


    // ## Input System을 이용한 방식
    private void OnMove(InputValue _inputValue)
    {
        inputVec = _inputValue.Get<Vector2>();  // 이미 normalize를 사용하기로 세팅했으므로 위에 FixedUpdate 문에서 normalized 사용 안해도 됨.
    }
}
