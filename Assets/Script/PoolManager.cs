using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    // .. 프리펩들 보관할 변수 (배열)
    public GameObject[] prefabs;

    // .. 풀 담당을 하는 리스트의 배열 ㅋ; 
    private List<GameObject>[] pool;

    private void Awake()
    {
        // 배열을 동적할당 함.
        pool = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < pool.Length; i++)
        {
            // 배열의 각 요소에 리스트를 동적할당 함.
            pool[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int objectNumber)
    {
        GameObject objectSelected = null;

        // .. 선택한 풀의 비활성화된 게임 오브젝트를 찾아서 objectSelected에 할당.
        foreach (GameObject item in pool[objectNumber])
        {
            if (!item.activeSelf)                   // # 게임오브젝트 활성화 여부는 activeSelf고, 컴포넌트 활성화 여부는 enabled 이다.
            {
                objectSelected = item;
                objectSelected.SetActive(true);
                break;
            }
                
        }
        
        // .. 풀에서 재사용 가능한, 비활성화된 오브젝트 못 찾았을 경우
        if (!objectSelected)
        {
            // .. 새롭게 게임오브젝트를 생성하여 objectSelected에 할당. 
            // Instantiate() - 새로운 게임오브젝트를 생성함. 이 instantiate을 많이 안 하기 위해 오브젝트 풀링을 하는 것임.
            objectSelected = Instantiate(prefabs[objectNumber], transform);

            // 새로 생성한 게임오브젝트를 풀에 등록함.
            pool[objectNumber].Add(objectSelected);
        }

        return objectSelected;
    }


}
