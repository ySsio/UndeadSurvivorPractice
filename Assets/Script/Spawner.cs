using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    int level = 0;

    public float timer;
    float curTime;


    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();  // �ڽ� ������.
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
        timer = spawnData[level].spawnTime;
        curTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        // �ð� ī��Ʈ..
        curTime -= Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1);

        // �ð� ī��Ʈ ����
        if (curTime <= 0)
        {
            // ����
            Spawn();
            
            // �ð� �ʱ�ȭ
            curTime = timer;
            timer = spawnData[level].spawnTime;
        }

    }

    void Spawn()
    {
        // ������Ʈ ���� / Ǯ���� ������ �� curObject�� ����
        GameObject curObject = GameManager.instance.poolManager.Get(0);

        // ���� / ������ ������Ʈ�� ��ġ�� ���� ����Ʈ �� �ϳ��� �����ϰ� �ű� (�ű⼭ ������ �� ó��..)
        // ���� 1���� �� ������ 0�� ������ �ڽ��� ��ġ�� ��� �ֱ� ������. 1���� �ؾ� �ڽ�(����Ʈ) ��ġ�� �ش��.
        curObject.transform.position = spawnPoint[(int)UnityEngine.Random.Range(1, spawnPoint.Length - 1)].transform.position;

        // ���� / ������ ������Ʈ�� level�� �´� ���������Ϳ� ���� �ʱ�ȭ ���� (�ִϸ��̼���Ʈ�ѷ�, ���ǵ�, ü��)
        curObject.GetComponent<Enemy>().Init(spawnData[level]);
    }
}


[System.Serializable] // [Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int maxHealth;
    public float speed;
}