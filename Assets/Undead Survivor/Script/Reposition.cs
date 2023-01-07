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
        // collision�� ��� �±װ� "Area"�� �ƴϸ� ����
        if (!collision.CompareTag("Area"))
            return;

        // �÷��̾ x������ ������� y������ ������� Ȯ���ؼ� Ÿ�ϸ��� ��ġ�ؾ� ��!

        // 1. �󸶳� ����°�
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // 2. ���� ����°�

        // ���⼭�� �÷��̾� ������ ��ǲ�� �������� �����ϱ� �ߴµ�, �� ��������
        // ������ diffX�� diffY�� ������ ������ �ʰ� �׳� �״�� �޾ƿͼ� ����� �÷��̾ �������ΰŰ� ������ �÷��̾ ������ �Ŵϱ�
        // �׹������� ����� �ϴ°� ���� �ʳ� �;�.
        // ��ǲ�� �������� �ϸ� �� ����ϱ� ������ �Ÿ��� �Ѵ� ���� ��ǲ�� �ݴ�� ������� �ǵ��� �Ͱ� �ݴ������� �۵��� �ǰ���.

        Vector3 playerDir = GameManager.instance.player.inputVec;
        // input system�� ����ؼ� normalize�� inputVec�� ���� �ִ� ��츦 ���� ���׿����� ����ؼ� -1/1�� �ٲ���
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;


        // �ڱ��ڽ��� tag�� �˻��ؼ� ������ ���ǹ����� ��
        switch (transform.tag)
        {
            case "Ground":
                // �밢������ ����� ��� (diffX==diffY���� ������ ���̱� ���� �Ʒ��� ���� ���� �ۼ���)
                // ����/������� ��� ó���������.
                if (Mathf.Abs(diffX - diffY) <= 0.1f)
                {
                    // Ÿ�� �ϳ��� ���� 20��. 40�� �̵��ϸ� ���� Ÿ���� �ǳʶپ ���� ��ġ�Ѵٴ� ���̰���?
                    transform.Translate(Vector3.right * dirX * 40);
                    transform.Translate(Vector3.up * dirY * 40);
                }
                // ����������� ����� ���
                else if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                // ������������ ����� ���
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
