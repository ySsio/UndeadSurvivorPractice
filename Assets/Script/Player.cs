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
        // ## Input Manager�� �̿��� ���
        //// Input.GetAxis()�� ���� �¿�/���� �Է� ���� ��  ����. -> project setting
        //// Input.GetAxis()���� �������� ����Ǿ� Ű���忡�� ���� ���� �̲������� ���� �߻�. Input.GetAxisRaw()�� -1/0/1 �̷��� ���� ���� �����ֹǷ� �̷� ���� ���� �� ����.
        //inputVec.x = Input.GetAxisRaw("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");
        
    }

    void FixedUpdate()
    {
        // ������ ���� ��� 3����
        // 1. rigid.AddForce()
        //rigid.AddForce(inputVec);

        // 2. rigid.velocity �� ����
        //rigid.velocity = inputVec;

        // 3. rigid.MovePosition()
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(nextVec + rigid.position);   // transform.position�� Vector3d�ε� rigid.position�� Rigidbody2d.position�̶� Vector2d��

        // ������� �����Ӹ��� update/fixedupdate �ֱⰡ �ٸ��� ������ Time.deltaTime/Time.fixedDeltaTime�� �������� ������ ȯ�濡 ���� ���̰� �߻���.
    }


    // ## Input System�� �̿��� ���
    private void OnMove(InputValue _inputValue)
    {
        inputVec = _inputValue.Get<Vector2>();  // �̹� normalize�� ����ϱ�� ���������Ƿ� ���� FixedUpdate ������ normalized ��� ���ص� ��.
    }
}
