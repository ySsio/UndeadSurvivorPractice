using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    public float timer;
    float curTime;


    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        curTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        // �ð� ī��Ʈ..
        curTime -= Time.deltaTime;

        // �ð� ī��Ʈ ����
        if (curTime <= 0)
        {
            // ����
            Spawn();
            
            // �ð� �ʱ�ȭ
            curTime = timer;
        }

    }

    void Spawn()
    {
        // ������Ʈ ����/ Ǯ���� ������ �� curObject�� ����
        GameObject curObject = GameManager.instance.poolManager.Get(Random.Range(0,4));

        // ����/ ������ ������Ʈ�� ��ġ�� ���� ����Ʈ �� �ϳ��� �����ϰ� �ű� (�ű⼭ ������ �� ó��..)
        curObject.transform.position = spawnPoint[(int)Random.Range(0, spawnPoint.Length - 1)].transform.position;
    }
}
