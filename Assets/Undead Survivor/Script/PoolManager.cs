using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. ��������� ������ ����
    public GameObject[] prefabs; // �迭�� ����


    // .. Ǯ ����� �ϴ� ����Ʈ�� (�ϳ��� �����鿡 ���� ���ӿ�����Ʈ���� �����ϴ� ����Ʈ)
    List<GameObject>[] pools;  // ����Ʈ�� �迭�� ����

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
        
        // ... ������ Ǯ�� ��Ȱ��ȭ�� ���ӿ�����Ʈ ����
        
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // ... �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // ... �� ã������?
        if (!select)
        {
            // ... ���Ӱ� �����ؼ� select ������ �Ҵ�
            select = Instantiate(prefabs[0], transform);
            // pool���� ������� (pool�� pools array�� ����)
            pools[index].Add(select);
            Debug.Log(pools.Length);
            Debug.Log(pools[index].Count);
        }


        return select;
    }

}
