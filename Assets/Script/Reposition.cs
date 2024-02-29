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

        // �÷��̾� ��ġ �������� Ÿ�ϸ� �̵� ������ ����� ����
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        float diffX = playerPos.x - myPos.x;
        float diffY = playerPos.y - myPos.y;

        Vector3 moveDir = new Vector3(0, 0, 0);

        

        // tile�� �����Ǿ��ִ��� enemy�� �����Ǿ��ִ����� ���� ������ ���� �ٸ�
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
