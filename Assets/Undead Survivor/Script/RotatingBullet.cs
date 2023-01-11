using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBullet : MonoBehaviour
{
    int count = 0;
    int bulletLevel = 0;
    public GameObject[] prefabs;
    List<GameObject> rotateList;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            AddBullet();
            Debug.Log(rotateList);
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

        GameObject newBullet = Instantiate(prefabs[bulletLevel], transform);
        rotateList.Add(newBullet);

        newBullet.transform.localPosition += new Vector3(0, 5, 0);
        for (int i=0; i<count; i++)
        {
            rotateList[i].transform.localRotation = Quaternion.Euler(0, 0, -360 / (count + 1));
        }
        
    }
}
