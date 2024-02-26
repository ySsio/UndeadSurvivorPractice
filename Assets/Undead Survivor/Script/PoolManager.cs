using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. 프리펩들을 보관할 변수
    public GameObject[] prefabs; // 배열로 저장


    // .. 풀 담당을 하는 리스트들 (하나의 프리펩에 대한 게임오브젝트들을 관리하는 리스트)
    List<GameObject>[] pools;  // 리스트도 배열로 저장

    private void Awake()
    {
        pools = new List<GameObject>[prefabs[0].GetComponent<Enemy>().animCon.Length];

        for (int index=0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        
        // ... 선택한 풀의 비활성화된 게임오브젝트 접근
        
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // ... 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // ... 못 찾았으면?
        if (!select)
        {
            // ... 새롭게 생성해서 select 변수에 할당
            select = Instantiate(prefabs[0], transform);
            // pool에도 등록해줌 (pool은 pools array의 원소)
            pools[index].Add(select);
            Debug.Log(pools.Length);
            Debug.Log(pools[index].Count);
        }


        return select;
    }

}
