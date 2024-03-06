using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriteRenderer;

    SpriteRenderer playerSpriteRenderer;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35f);
    Quaternion leftReverseRot = Quaternion.Euler(0, 0, -135f);

    private void Awake()
    {
        // # Awake() �Լ����� Player.instance.player�� �������ϱ� ������. Start()������ �Ǵµ�, �̰� ���� ���̰� �ִ� ��.
        // GetComponentInParent<>()�� �ڱ��ڽ��� �θ���, GetComponentsInParent<>()[0]�� �ڱ� �ڽ�
        playerSpriteRenderer = GetComponentsInParent<SpriteRenderer>()[1];  
    }

    private void LateUpdate()
    {
        bool isReverse = playerSpriteRenderer.flipX;

        if (isLeft) // ���� ����
        {
            transform.localRotation = isReverse ? leftReverseRot : leftRot;
            spriteRenderer.flipY = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 4 : 6;
        }
        else  // ���Ÿ� ����
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriteRenderer.flipX = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 6 : 4;
        }
    }

}
