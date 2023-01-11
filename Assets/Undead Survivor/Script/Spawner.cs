using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawndata; // level���� ��ȯ �� ������ ��ȯ ���� ���� �޶����Ƿ� �׿� ���� �迭�� �����ؼ� ����

    public int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // �� ���� ������Ʈ�� �ڽĵ��� ��� �迭�� ������, �ٵ� �ڱ� �ڽŵ� ������. �׷��� index=0���� �ڱ��ڽ��� ��
    }

    private void Update()
    {
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawndata.Length-1); // Mathf.FloorToInt : ���� vs Mathf.CeilToInt (�ٴڰ� õ��)
                                                                                                     // spawndata.Length-1�� �ִ� �����̶�� �� �� �����Ƿ� �ִ뷹���� ���Ͽ� ���� ���� level�� ����.
        timer += Time.deltaTime;
        if (timer > spawndata[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(level); // ���� Ȱ��ȭ�� �Ǵ� ���� ������ enemy ���ӿ�����Ʈ�� ��ȯ
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].transform.position; // spawnPoint �迭�� �ִ� �� ������ �߿� �������� ��� ��ġ ����
        enemy.GetComponent<Enemy>().Init(spawndata[level]); // enemy ���ӿ�����Ʈ�� Enemy component�� Init �Լ��� ����. sprite, speed, maxHealth�� health�� �ʱ�ȭ����.
    }
}


// ����ȭ => inspector������ ���̰� ��.
[System.Serializable]
public class SpawnData
{

    public float spawnTime; // enemy ��ȯ����(�ð�)
    public int spriteType; // enemy prefab�� ���� (0�̸� ���� 1�̸� ���̷��� �̷���)
    public int health; // enemy ü��
    public float speed; // enemy speed
}
