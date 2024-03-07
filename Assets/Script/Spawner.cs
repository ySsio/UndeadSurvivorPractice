using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level = 0;

    public float timer;
    float curTime;


    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();  // 자신 포함임.
        timer = spawnData[level].spawnTime;
        curTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        // 시간 카운트..
        curTime -= Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 4f), spawnData.Length - 1);

        // 시간 카운트 종료
        if (curTime <= 0)
        {
            // 스폰
            Spawn();
            
            // 시간 초기화
            curTime = timer;
            timer = spawnData[level].spawnTime;
        }

    }

    void Spawn()
    {
        // 오브젝트 생성 / 풀에서 꺼내온 후 curObject에 저장
        GameObject curObject = GameManager.instance.poolManager.Get(0);

        // 생성 / 꺼내온 오브젝트의 위치를 스폰 포인트 중 하나로 랜덤하게 옮김 (거기서 생성된 것 처럼..)
        // 랜덤 1부터 한 이유는 0은 스포너 자신의 위치를 담고 있기 때문임. 1부터 해야 자식(포인트) 위치만 해당됨.
        curObject.transform.position = spawnPoint[(int)UnityEngine.Random.Range(1, spawnPoint.Length - 1)].transform.position;

        // 생성 / 꺼내온 오브젝트를 level에 맞는 스폰데이터에 따라 초기화 해줌 (애니메이션컨트롤러, 스피드, 체력)
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