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

    public Scanner scanner;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
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

    // ���� ���������� �Ѿ�� ������ ����Ǵ� �����ֱ� �Լ�
    private void LateUpdate()
    {
        // �ִϸ��̼��� inputVec�� ũ�⿡ ���� ���� (run/stand)
        anim.SetFloat("Speed", inputVec.magnitude);

        // ��ǲ x ���� 0���� ������ ��������Ʈ������ x������ ������
        if (inputVec.x !=0)
        {
            spriteRenderer.flipX = inputVec.x < 0;  
        }
    }


    // ## Input System�� �̿��� ���
    private void OnMove(InputValue _inputValue)
    {
        inputVec = _inputValue.Get<Vector2>();  // �̹� normalize�� ����ϱ�� ���������Ƿ� ���� FixedUpdate ������ normalized ��� ���ص� ��.
    }
}
