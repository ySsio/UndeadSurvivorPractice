using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotatingBullet : MonoBehaviour
{
    int count = 0;
    int bulletLevel = 0;
    public float distance;
    public GameObject[] prefabs;
    List<GameObject> rotateList;

    private void Awake()
    {
        rotateList = new List<GameObject>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            AddBullet();
        }
    }
    private void FixedUpdate()
    {

        if (transform.tag == "LaunchingBullet")
        {

        }
        else if (transform.tag == "RotatingBullet")
        {
            RotateAround();
        }

    }
    public void RotateAround()
    {
        transform.Rotate(0, 0, -180 * Time.fixedDeltaTime, Space.Self);
    }

    public void AddBullet()
    {
        if (count >= 6) return;
        Debug.Log(count);
        GameObject newBullet = Instantiate(prefabs[bulletLevel], transform);
        rotateList.Add(newBullet);
        count++;

        // 회전 bullet을 재배치함.
        BulletReposition(count);
        
    }

    private void BulletReposition(int count)
    {
        for (int i = 0; i < count; i++)
        {
            rotateList[i].transform.localPosition = new Vector3(0, distance, 0);
            rotateList[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            rotateList[i].transform.RotateAround(transform.position, new Vector3(0, 0, 1), -360 * i / count);
        }
    }
}
