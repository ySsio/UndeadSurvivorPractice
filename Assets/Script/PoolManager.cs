using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    // .. ������� ������ ���� (�迭)
    public GameObject[] prefabs;

    // .. Ǯ ����� �ϴ� ����Ʈ�� �迭 ��; 
    private List<GameObject>[] pool;

    private void Awake()
    {
        // �迭�� �����Ҵ� ��.
        pool = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < pool.Length; i++)
        {
            // �迭�� �� ��ҿ� ����Ʈ�� �����Ҵ� ��.
            pool[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int objectNumber)
    {
        GameObject objectSelected = null;

        // .. ������ Ǯ�� ��Ȱ��ȭ�� ���� ������Ʈ�� ã�Ƽ� objectSelected�� �Ҵ�.
        foreach (GameObject item in pool[objectNumber])
        {
            if (!item.activeSelf)                   // # ���ӿ�����Ʈ Ȱ��ȭ ���δ� activeSelf��, ������Ʈ Ȱ��ȭ ���δ� enabled �̴�.
            {
                objectSelected = item;
                objectSelected.SetActive(true);
                break;
            }
                
        }
        
        // .. Ǯ���� ���� ������, ��Ȱ��ȭ�� ������Ʈ �� ã���� ���
        if (!objectSelected)
        {
            // .. ���Ӱ� ���ӿ�����Ʈ�� �����Ͽ� objectSelected�� �Ҵ�. 
            // Instantiate() - ���ο� ���ӿ�����Ʈ�� ������. �� instantiate�� ���� �� �ϱ� ���� ������Ʈ Ǯ���� �ϴ� ����.
            objectSelected = Instantiate(prefabs[objectNumber], transform);

            // ���� ������ ���ӿ�����Ʈ�� Ǯ�� �����.
            pool[objectNumber].Add(objectSelected);
        }

        return objectSelected;
    }


}
