using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    BoxCollider2D coll;

    void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        collision.gameObject.SetActive(false);
    }
}
