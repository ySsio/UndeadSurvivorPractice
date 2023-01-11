using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawndata; // level마다 소환 몹 종류나 소환 간격 등이 달라지므로 그에 대한 배열을 선언해서 저장

    public int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // 이 게임 오브젝트의 자식들을 모두 배열로 가져옴, 근데 자기 자신도 가져옴. 그래서 index=0에는 자기자신이 들어감
    }

    private void Update()
    {
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawndata.Length-1); // Mathf.FloorToInt : 버림 vs Mathf.CeilToInt (바닥과 천장)
                                                                                                     // spawndata.Length-1이 최대 레벨이라고 볼 수 있으므로 최대레벨과 비교하여 작은 값을 level로 설정.
        timer += Time.deltaTime;
        if (timer > spawndata[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(level); // 새로 활성화된 또는 새로 생성된 enemy 게임오브젝트를 반환
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].transform.position; // spawnPoint 배열에 있는 점 포지션 중에 랜덤으로 골라서 위치 지정
        enemy.GetComponent<Enemy>().Init(spawndata[level]); // enemy 게임오브젝트의 Enemy component의 Init 함수를 실행. sprite, speed, maxHealth와 health를 초기화해줌.
    }
}


// 직렬화 => inspector에서도 보이게 됨.
[System.Serializable]
public class SpawnData
{

    public float spawnTime; // enemy 소환간격(시간)
    public int spriteType; // enemy prefab의 종류 (0이면 좀비 1이면 스켈레톤 이런식)
    public int health; // enemy 체력
    public float speed; // enemy speed
}
