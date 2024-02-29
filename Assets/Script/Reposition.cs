using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ReposDir
{
    Right = 1,
    Left = -1,
    Up = 1,
    Down = -1
}

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    const int TILE_SIZE = 20;
    const int ENEMY_RANGE = 10;
    private int standardDist = 0;


    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        // 플레이어 위치 기준으로 타일맵 이동 방향을 계산할 거임
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        float diffX = playerPos.x - myPos.x;
        float diffY = playerPos.y - myPos.y;

        Vector3 moveDir = new Vector3(0, 0, 0);

        

        // tile에 부착되어있는지 enemy에 부착되어있는지에 따라 움직임 로직 다름
        switch (transform.tag)
        {
            case "Ground":
                standardDist = TILE_SIZE;
                break;

            case "Enemy":
                if (!coll.enabled)
                    break;
                
                standardDist = ENEMY_RANGE;
                moveDir = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
                break;

            default:
                break;

        }

        if (diffX > standardDist)
        {
            moveDir.x = (int)ReposDir.Right     * 2 * standardDist;
        }
        else if (diffX < -standardDist)
        {
            moveDir.x = (int)ReposDir.Left      * 2 * standardDist;
        }

        if (diffY > standardDist)
        {
            moveDir.y = (int)ReposDir.Up        * 2 * standardDist;
        }
        else if (diffY < -standardDist)
        {
            moveDir.y = (int)ReposDir.Down      * 2 * standardDist;
        }

        transform.Translate(moveDir);
    }
}
