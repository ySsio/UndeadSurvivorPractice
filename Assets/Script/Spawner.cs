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
        // 시간 카운트..
        curTime -= Time.deltaTime;

        // 시간 카운트 종료
        if (curTime <= 0)
        {
            // 스폰
            Spawn();
            
            // 시간 초기화
            curTime = timer;
        }

    }

    void Spawn()
    {
        // 오브젝트 생성/ 풀에서 꺼내온 후 curObject에 저장
        GameObject curObject = GameManager.instance.poolManager.Get(Random.Range(0,4));

        // 생성/ 꺼내온 오브젝트의 위치를 스폰 포인트 중 하나로 랜덤하게 옮김 (거기서 생성된 것 처럼..)
        curObject.transform.position = spawnPoint[(int)Random.Range(0, spawnPoint.Length - 1)].transform.position;
    }
}
