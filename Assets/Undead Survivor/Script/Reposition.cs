using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;
    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        // collision의 상대 태그가 "Area"가 아니면 리턴
        if (!collision.CompareTag("Area"))
            return;

        // 플레이어가 x축으로 벗어났는지 y축으로 벗어났는지 확인해서 타일맵을 배치해야 함!

        // 1. 얼마나 벗어났는가
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // 2. 어디로 벗어났는가

        // 여기서는 플레이어 방향을 인풋의 방향으로 측정하긴 했는데, 내 생각에는
        // 위에서 diffX와 diffY에 절댓값을 씌우지 않고 그냥 그대로 받아와서 양수면 플레이어가 오른쪽인거고 음수면 플레이어가 왼쪽인 거니까
        // 그방향으로 계산을 하는게 맞지 않나 싶어.
        // 인풋의 방향으로 하면 좀 희박하긴 하지만 거리를 넘는 순간 인풋을 반대로 줘버리면 의도한 것과 반대쪽으로 작동이 되겠지.

        Vector3 playerDir = GameManager.instance.player.inputVec;
        // input system을 사용해서 normalize된 inputVec을 갖고 있는 경우를 위해 삼항연산자 사용해서 -1/1로 바꿔줌
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;


        // 자기자신의 tag를 검사해서 각각의 조건문으로 들어감
        switch (transform.tag)
        {
            case "Ground":
                // 대각선으로 벗어나는 경우 (diffX==diffY에서 에러를 줄이기 위해 아래와 같이 조건 작성함)
                // 수직/수평방향 모두 처리해줘야함.
                if (Mathf.Abs(diffX - diffY) <= 0.1f)
                {
                    // 타일 하나가 길이 20임. 40을 이동하면 현재 타일을 건너뛰어서 옆에 배치한다는 뜻이겠죵?
                    transform.Translate(Vector3.right * dirX * 40);
                    transform.Translate(Vector3.up * dirY * 40);
                }
                // 수평방향으로 벗어나는 경우
                else if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                // 수직방향으로 벗어나는 경우
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f,3f), Random.Range(-3f, 3f),0f));
                }
                break;
        }
    }
}
