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
        // # Awake() 함수에서 Player.instance.player로 가져오니까 오류남. Start()에서는 되는데, 이게 순서 차이가 있는 듯.
        // GetComponentInParent<>()는 자기자신을 부르고, GetComponentsInParent<>()[0]도 자기 자신
        playerSpriteRenderer = GetComponentsInParent<SpriteRenderer>()[1];  
    }

    private void LateUpdate()
    {
        bool isReverse = playerSpriteRenderer.flipX;

        if (isLeft) // 근접 무기
        {
            transform.localRotation = isReverse ? leftReverseRot : leftRot;
            spriteRenderer.flipY = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 4 : 6;
        }
        else  // 원거리 무기
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriteRenderer.flipX = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 6 : 4;
        }
    }

}
